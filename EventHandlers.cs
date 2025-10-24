using System.Collections.Generic;
using System.Collections.ObjectModel;

using Exiled.CustomRoles.API;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Scp330;

using Interactables.Interobjects;

using InventorySystem.Items.Usables.Scp330;

using MapGeneration.Holidays;

namespace CandyChances
{
    public class EventHandlers
    {
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
            Translation translation = Plugin.Instance.Translation;

            ev.ShouldPlaySound = config.ShouldPlayTakeSound;

            #region CustomUseLimits
            int usageLimit = Scp330Interobject.MaxAmountPerLife;

            if (config.OverrideUseLimitsforCustomRoles)
            {
                ReadOnlyCollection<CustomRole> customRoles = ev.Player.GetCustomRoles();
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
                if (config.ModifiedUseLimits.TryGetValue(ev.Player.Role.Type, out int customUsageLimit))
                    usageLimit = customUsageLimit;
            }

            ev.ShouldSever = ev.UsageCount >= usageLimit;
            #endregion

            #region Hints logics
            if (ev.ShouldSever)
            {
                if (config.ShowHandsSeveredHint)
                    ev.Player.ShowHint(translation.HandsSeveredHints.RandomItem(), config.HintTime);

                return;
            }


            string hint = null;
            if (config.ShowCandyHint)
            {
                Dictionary<CandyKindID, string[]> hintDictionary = HolidayUtils.IsHolidayActive(HolidayType.Halloween) ?
                    translation.HallowenCandyHints : 
                    translation.CandyHints;

                if (hintDictionary.TryGetValue(ev.Candy, out var hints))
                    hint = hints.RandomItem();
            }

            if (config.ShowRemainingUseHint)
            {
                int remaining = usageLimit - ev.UsageCount - 1;
                string remainingHint = translation.RemainingUse.Replace("{0}", remaining.ToString());
                hint = hint == null ? remainingHint : string.Concat(hint, "\n", remainingHint);
            }

            if (hint != null)
                ev.Player.ShowHint(hint, config.HintTime);
            #endregion
        }
    }
}
