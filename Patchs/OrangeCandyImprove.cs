using System.Collections.Generic;

using Exiled.API.Features;
using HarmonyLib;
using UnityEngine;
using MEC;

using InventorySystem.Items.Usables.Scp330;

using Light = Exiled.API.Features.Toys.Light;

namespace CandyChances.Patchs
{
    
    [HarmonyPatch(typeof(HauntedCandyOrange), nameof(HauntedCandyOrange.ServerApplyEffects))]
    internal static class OrangeCandyImprove
    {
        public static void Postfix(ReferenceHub hub) 
        {
            Timing.RunCoroutine(PlaySunRoutine(hub).CancelWith(hub));
            Log.Debug("[OrangeCandyImprove] Sun light effect started.");
        }

        private static IEnumerator<float> PlaySunRoutine(ReferenceHub hub)
        {
            Config config = Plugin.Instance.Config;

            Player player = Player.Get(hub);

            Light light = Light.Create(position: player.Transform.position, rotation: Vector3.zero, scale: Vector3.one * 2, spawn: true, color: new Color(1f, 0.45f, 0.05f));

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

            yield return Timing.WaitForSeconds(HauntedCandyOrange.ActiveTime);

            while (light.Intensity > 0.5f)
            {
                light.Intensity *= 0.95f;
                yield return Timing.WaitForSeconds(fadeOutSpeed);
            }

            light.Destroy();
        }
    }
}