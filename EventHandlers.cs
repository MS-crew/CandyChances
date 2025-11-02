using System.Collections.Generic;
using System.Collections.ObjectModel;

using Exiled.API.Features;
using Exiled.CustomRoles.API;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Scp330;

using Interactables.Interobjects;

using InventorySystem.Items.Usables.Scp330;


#if RUEI
using RueI.API;
using RueI.API.Elements;
#endif

namespace CandyChances
{
    public class EventHandlers
    {
        #if RUEI
        private static readonly Tag Scp330HintsTag = new("CandyChances");
        #endif

        public void OnRoundStarted()
        {
            Config config = Plugin.Instance.Config;

            if (config.OverrideGlobalUseLimit)
                Scp330Interobject.MaxAmountPerLife = config.ModifiedGlobalUseLimit;

            if (config.OverrideCandyTakeCooldown)
                Scp330Interobject.TakeCooldown = config.ModifiedCandyTakeCooldown;
        }

        public void OnInteractingScp330(InteractingScp330EventArgs ev)
        {
            if (!ev.IsAllowed)
                return;

            Config config = Plugin.Instance.Config;

            ev.ShouldPlaySound = config.ShouldPlayTakeSound;

            int usageLimit = GetUsageLimit(ev.Player);
            ev.ShouldSever = ev.UsageCount >= usageLimit;

            if (ev.ShouldSever)
            {
                if (config.ShowHandsSeveredHint)
                    GiveHint(ev.Player, Plugin.Instance.Translation.HandsSeveredHints.RandomItem(), config.HintPositionRuei, config.HintTime);

                return;
            }

            GiveCandyHint(ev.Player, ev.Candy, usageLimit, ev.UsageCount);
        }

        private int GetUsageLimit(Player player)
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

        private void GiveCandyHint(Player player, CandyKindID candy, int usageLimit, int usageCount)
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
                GiveHint(player, hint, config.HintPositionRuei, config.HintTime);
        }

        private void GiveHint(Player player, string hint, float position, float duration)
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