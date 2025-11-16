using System.Collections.Generic;

using Exiled.API.Features;
using InventorySystem.Items.Usables.Scp330;

using MEC;

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

            handle = Timing.RunCoroutine(SunEffect(Player));
        }
        public override void OnEffectDisabled()
        {
            light?.Destroy();
            Timing.KillCoroutines(handle);
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

        public override void OnEffectUpdate() { }
    }
}
