using MEC;
using UnityEngine;
using Exiled.API.Enums;
using Exiled.API.Features;
using PlayerRoles.PlayableScps;
using System.Collections.Generic;

using Light = Exiled.API.Features.Toys.Light;

namespace CandyChances.Components
{
    public class OrangeCandy : Effect
    {
        protected override float Duration => 25;

        private Light light;
        private CoroutineHandle handle;

        private static Color lightColor = new(1f, 0.45f, 0.05f);

        private const float range = 20f;

        private const float fadeInMult = 1.2f;
        private const float fadeOutMult = 0.9f;

        private const float fadeInSpeed = 0.05f;
        private const float fadeOutSpeed = 0.05f;

        private const float firstInsentity = 0.5f;
        private const float maxInsentity = 50000f;

        private const float FlashDurationMult = 1f;
        private const float WitnessDurationMult = 1.15f;

        private const float FlashDistance = 4.4f;
        private const float FlashDistanceSqr = FlashDistance * FlashDistance;

        private const LightShadows shadowType = LightShadows.None;

        public override void OnEffectEnabled()
        {
            Player.RemoveEffect<Metal>();
            Player.RemoveEffect<White>();

            handle = Timing.RunCoroutine(SunEffect(Player).CancelWith(gameObject));
        }

        public override void OnEffectDisabled()
        {
            light?.Destroy();
            Timing.KillCoroutines(handle);
        }

        private IEnumerator<float> SunEffect(Player player)
        {
            float totalDuration = Duration;
            float fadeOutTime = 3f;
            float startTime = Time.time;        

            light = Light.Create(position: player.Transform.position, rotation: Vector3.zero, scale: Vector3.one * 2, spawn: true, color: lightColor);

            light.Range = range;
            light.ShadowType = shadowType;
            light.Intensity = firstInsentity;

            light.Transform.SetParent(player.Transform, true);

            while (light != null && light.Intensity < maxInsentity)
            {
                light.Intensity *= fadeInMult;
                yield return Timing.WaitForSeconds(fadeInSpeed);
            }

            float elapsed = Time.time - startTime;
            float timeLeft = totalDuration - elapsed - fadeOutTime;

            yield return Timing.WaitForSeconds(timeLeft);

            float fadeOutStart = Time.time;
            float fadeOutEnd = fadeOutStart + fadeOutTime;

            while (light != null && Time.time < fadeOutEnd)
            {
                float t = 1f - ((Time.time - fadeOutStart) / fadeOutTime);
                light.Intensity = Mathf.Lerp(firstInsentity, maxInsentity, t);

                yield return Timing.WaitForOneFrame;
            }

            light?.Destroy();
        }

        public override void OnEffectUpdate() 
        {
            Vector3 playerPos = Player.Position;
            foreach (Player spectator in Player.List)
            {
                if (Player == spectator || !spectator.IsAlive || !HitboxIdentity.IsEnemy(Player.ReferenceHub, spectator.ReferenceHub))
                    continue;

                if (!VisionInformation.GetVisionInformation(spectator.ReferenceHub, spectator.CameraTransform, playerPos, 0.3f, 60f, true, true, 0, false).IsLooking)
                    continue;

                spectator.EnableEffect(type: EffectType.Flashed, duration: Time.deltaTime * WitnessDurationMult, addDurationIfActive: true);

                float sqrDist = (spectator.Position - playerPos).sqrMagnitude;
                if (sqrDist <= FlashDistanceSqr)
                {
                    spectator.EnableEffect(type: EffectType.Flashed, duration: Time.deltaTime * FlashDurationMult, addDurationIfActive: true);
                }
            }
        }
    }
}
