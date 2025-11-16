using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Scp330;

using Interactables.Interobjects;

using InventorySystem.Items.Usables.Scp330;

using MEC;

namespace CandyChances
{
    internal class EventHandlers
    {
        internal void OnRoundStarted()
        {
            Config config = Plugin.Instance.Config;

            if (config.OverrideCandyTakeCooldown)
                Scp330Interobject.TakeCooldown = config.ModifiedCandyTakeCooldown;

            if (config.OverrideGlobalUseLimit)
                Scp330Interobject.MaxAmountPerLife = config.ModifiedGlobalUseLimit;
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

        internal void OnEatenScp330(EatenScp330EventArgs ev)
        {
            if (ev.Candy.Kind == CandyKindID.Orange && Plugin.Instance.Config.OrangeCandySettings.AddLight)
            {
                ev.Player.SendFakeEffectTo(Player.List, EffectType.OrangeCandy, 1);
                Timing.RunCoroutine(Methods.SunEffect(ev.Player).CancelWith(ev.Player.GameObject));
                Log.Debug("[EventHandlers:EatenScp330] Sun light effect started.");
                return;
            }
        }
    }
}