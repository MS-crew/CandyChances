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

            int usageLimit = ev.Player.GetUsageLimit();
            ev.ShouldSever = ev.UsageCount >= usageLimit;

            if (ev.ShouldSever)
            {
                if (config.ShowHandsSeveredHint)
                    ev.Player.GiveHint(Plugin.Instance.Translation.HandsSeveredHints.RandomItem(), config.HintPositionRuei, config.HintTime);

                return;
            }

            ev.Player.GiveCandyHint(ev.Candy, usageLimit, ev.UsageCount);
        }
    }
}