using Interactables.Interobjects;
using InventorySystem.Items.Usables.Scp330;

using Exiled.API.Features;
using Exiled.CustomRoles.API;
using Exiled.CustomRoles.API.Features;
using System;
using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;


#if RUEI
using RueI.API;
using RueI.API.Elements;
#endif

namespace CandyChances
{
    public static class Extensions
    {
#if RUEI
        private static readonly Tag s_scp330HintsTag = new("CandyChances");
#endif

        private static readonly Dictionary<string, Type> s_candyNametoTypes = BuildTypeLookup();

        private static Dictionary<string, Type> BuildTypeLookup()
        {
            Type[] candyTypes = typeof(ICandy).Assembly.GetTypes();
            Dictionary<string, Type> dict = new(candyTypes.Length, StringComparer.Ordinal);

            for (int i = 0; i < candyTypes.Length; i++)
            {
                Type t = candyTypes[i];
                if (t?.Name != null && !dict.ContainsKey(t.Name))
                    dict[t.Name] = t;
            }

            return dict;
        }

        internal static int GetUsageLimit(this Player player)
        {
            Config config = Plugin.Instance.Config;

            if (config.OverrideUseLimitsforCustomRoles)
            {
                foreach (CustomRole role in player.GetCustomRoles())
                {
                    if (config.ModifiedUseLimitsforCustomRoles.TryGetValue(role.Name, out int customUsageLimit))
                        return customUsageLimit;
                }
            }

            if (config.OverrideUseLimitsforRoles)
            {
                if (config.ModifiedUseLimits.TryGetValue(player.Role.Type, out int customUsageLimit))
                    return customUsageLimit;
            }

            return Scp330Interobject.MaxAmountPerLife;
        }

        internal static void Give330BowlUsageHint(this Player player, CandyKindID candy, int usageLimit, int usageCount)
        {
            Config config = Plugin.Instance.Config;
            Translation translation = Plugin.Instance.Translation;

            string hint = null;
            if (config.ShowCandyHint)
            {
#if HALLOWEN
                if (translation.HallowenCandyHints.TryGetValue(candy, out string[] hints))
                    hint = hints.RandomItem();
#else
                if (translation.CandyHints.TryGetValue(candy, out string[] hints))
                    hint = hints.RandomItem();
#endif
            }

            if (config.ShowRemainingUseHint)
            {
                int remaining = usageLimit - usageCount - 1;
                string remainingHint = translation.RemainingUse.Replace("{0}", remaining.ToString());
                hint = hint == null ? remainingHint : string.Concat(hint, "\n", remainingHint);
            }

            if (!string.IsNullOrEmpty(hint))
                player.GiveHint(hint, config.HintPositionRuei, config.HintTime);
        }

        internal static void GiveHint(this Player player, string hint, float position, float duration)
        {
#if RUEI
            RueDisplay display = RueDisplay.Get(player);
            display.Show(s_scp330HintsTag, new BasicElement(position, hint), duration);
#else

            player.ShowHint(hint, duration);
#endif
        }

        public static Type ToCandyType(this string candyType)
        {
            if (string.IsNullOrEmpty(candyType))
                return null;

            s_candyNametoTypes.TryGetValue(candyType, out Type found))
            return found;
        }

        public static void PatchSingleType(this Harmony harmony, Type patchClass)
        {
            PatchClassProcessor processor = new(harmony, patchClass);
            processor.Patch();

            Log.Debug($"Patched {patchClass.FullName}");
        }

        public static T AddEffect<T>(this Player player) where T : Behaviour
        {
            T effect;
            if (player.GameObject.TryGetComponent(out effect))
            {
                effect.enabled = true;
                return effect;
            }

            effect = player.GameObject.AddComponent<T>();
            effect.enabled = true;
            return effect;
        }

        public static void RemoveEffect<T>(this Player player) where T : Behaviour
        {
            if (player.GameObject.TryGetComponent(out T effect))
            {
                effect.enabled = false;
            }
        }
    }
}
