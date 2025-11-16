using System.Collections.Generic;

using CustomPlayerEffects;

using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Roles;

using InventorySystem.Items.Usables.Scp330;

using MEC;

using PlayerRoles.FirstPersonControl;

using PlayerRoles.PlayableScps;

using Unity.Collections.LowLevel.Unsafe;

using UnityEngine;

using Light = Exiled.API.Features.Toys.Light;

namespace CandyChances.Components
{
    public class OrangeCandy : Effect
    {
        private Light light;
        private CoroutineHandle handle;

        protected override float Duration => 25;

        public override void OnEffectEnabled()
        {
            Player.RemoveEffect<Metal>();
            Player.RemoveEffect<White>();

            Player.EnableEffect(EffectType.SoundtrackMute);
            handle = Timing.RunCoroutine(SunEffect(Player));
        }

        public override void OnEffectDisabled()
        {
            light?.Destroy();
            Timing.KillCoroutines(handle);
            Player.DisableEffect(EffectType.SoundtrackMute);
        }

        private IEnumerator<float> SunEffect(Player player)
        {
            Config config = Plugin.Instance.Config;

            light = Light.Create(position: player.Transform.position, rotation: Vector3.zero, scale: Vector3.one * 2, spawn: true, color: new Color(1f, 0.45f, 0.05f));

            light.Range = config.OrangeCandySettings.Range;
            light.Intensity = 1f;
            light.ShadowType = LightShadows.None;
            light.Transform.SetParent(player.Transform, true);

            yield return Timing.WaitForOneFrame;

            float fadeInSpeed = 0.05f;
            float fadeOutSpeed = 0.05f;
            float targetIntensity = config.OrangeCandySettings.MaxInsentity;

            while (light.Intensity <= targetIntensity)
            {
                light.Intensity *= 1.09f;
                yield return Timing.WaitForSeconds(fadeInSpeed);
            }

            yield return Timing.WaitForSeconds(Duration);

            while (light.Intensity > 0.5f)
            {
                light.Intensity *= 0.95f;
                yield return Timing.WaitForSeconds(fadeOutSpeed);
            }

            light.Destroy();
        }

        public override void OnEffectUpdate() 
        {
            Vector3 position = Player.Position;
            foreach (Player spectator in Player.List)
            {
                if (Player == spectator || !HitboxIdentity.IsEnemy(Player.ReferenceHub, spectator.ReferenceHub))
                    continue;

                if (!spectator.IsAlive || !VisionInformation.GetVisionInformation(spectator.ReferenceHub, spectator.CameraTransform, position, 0.3f, 60f, true, true, 0, false).IsLooking)
                    continue;

                spectator.EnableEffect(EffectType.OrangeWitness, duration: Time.deltaTime * 1.15f, true);
                if (Vector3.Distance(spectator.Position, position) <= 4.4f)
                {
                    spectator.EnableEffect(EffectType.Flashed, duration: Time.deltaTime * 1.0333333f, true);
                }
            }
        }
    }
}
