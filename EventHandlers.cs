using Interactables.Interobjects;

using Exiled.Events.EventArgs.Scp330;

namespace CandyChances
{
    internal class EventHandlers
    {
        internal void OnRoundStarted()
        {
            Config config = Plugin.Instance.Config;

            if (config.OverrideGlobalUseLimit)
                Scp330Interobject.MaxAmountPerLife = config.ModifiedGlobalUseLimit;

            if (config.OverrideCandyTakeCooldown)
                Scp330Interobject.TakeCooldown = config.ModifiedCandyTakeCooldown;
        }

        internal void OnInteractingScp330(InteractingScp330EventArgs ev)
        {
            if (!ev.IsAllowed)
                return;

            Config config = Plugin.Instance.Config;

            int usageLimit = ev.Player.GetUsageLimit();
            bool reachedLimit = ev.UsageCount >= usageLimit;

            ev.ShouldSever = reachedLimit;
            ev.ShouldPlaySound = config.ShouldPlayTakeSound;

            if (reachedLimit)
            {
                if (config.ShowHandsSeveredHint)
                    ev.Player.GiveHint(Plugin.Instance.Translation.HandsSeveredHints.RandomItem(), config.HintPositionRuei, config.HintTime);

                return;
            }     

            if (config.ShowCandyHint || config.ShowRemainingUseHint)
                ev.Player.Give330BowlUsageHint(ev.Candy, usageLimit, ev.UsageCount);
        }
    }
}