
using System.Collections.ObjectModel;

using Exiled.API.Features;
using Exiled.CustomRoles.API;
using Exiled.CustomRoles.API.Features;

using Interactables.Interobjects;

using InventorySystem.Items.Usables.Scp330;

#if RUEI
using RueI.API;
using RueI.API.Elements;
#endif

namespace CandyChances
{
    internal static class Extensions
    {
        #if RUEI
        private static readonly Tag Scp330HintsTag = new("CandyChances");
        #endif
        internal static int GetUsageLimit(this Player player)
        {
            Config config = Plugin.Instance.Config;

            int usageLimit = Scp330Interobject.MaxAmountPerLife;

            if (config.OverrideUseLimitsforCustomRoles)
            {
                ReadOnlyCollection<CustomRole> customRoles = player.GetCustomRoles();
                if (customRoles != null)
                {
                    foreach (CustomRole role in customRoles)
                    {
                        if (config.ModifiedUseLimitsforCustomRoles.TryGetValue(role.Name, out int customUsageLimit))
                        {
                            usageLimit = customUsageLimit;
                            break;
                        }
                    }
                }
            }

            if (usageLimit == Scp330Interobject.MaxAmountPerLife && config.OverrideUseLimitsforRoles)
            {
                if (config.ModifiedUseLimits.TryGetValue(player.Role.Type, out int customUsageLimit))
                    usageLimit = customUsageLimit;
            }

            return usageLimit;
        }

        internal static void GiveCandyHint(this Player player, CandyKindID candy, int usageLimit, int usageCount)
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

            if (hint != null)
                player.GiveHint(hint, config.HintPositionRuei, config.HintTime);
        }

        internal static void GiveHint(this Player player, string hint, float position, float duration)
        {
#if RUEI
            RueDisplay display = RueDisplay.Get(player);
            display.Show(Scp330HintsTag, new BasicElement(position, hint), duration);
#else

            player.ShowHint(hint, duration);
#endif
        }
    }
}
