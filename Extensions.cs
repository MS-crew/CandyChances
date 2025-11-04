using Interactables.Interobjects;
using InventorySystem.Items.Usables.Scp330;

using Exiled.API.Features;
using Exiled.CustomRoles.API;
using Exiled.CustomRoles.API.Features;

#if RUEI
using RueI.API;
using RueI.API.Elements;
#endif

namespace CandyChances
{
    internal static class Extensions
    {
#if RUEI
        private static readonly Tag s_scp330HintsTag = new("CandyChances");
#endif

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
    }
}
