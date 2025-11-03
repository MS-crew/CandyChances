using Exiled.Events.EventArgs.Scp330;
using Interactables.Interobjects;

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