using CandyChances.Components;

using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Scp330;

using Interactables.Interobjects;

using InventorySystem.Items.Usables.Scp330;

using MEC;

using UnityEngine;
using UnityEngine.Rendering;

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
            if (!Plugin.Instance.Config.TryReplicateHalloweenCandys)
                return;

            if (ev.Candy.Kind == CandyKindID.Orange)
            {
                ev.Player.AddEffect<OrangeCandy>();
            }

            else if (ev.Candy.Kind == CandyKindID.Gray )
            {
                ev.Player.AddEffect<Metal>();
            }

            else if (ev.Candy.Kind == CandyKindID.White)
            {
                ev.Player.AddEffect<White>();
            }

            else if (ev.Candy is HauntedCandyPurple)
            {
                ev.Player.EnableEffect(EffectType.Slowness, duration: HauntedCandyPurple.EffectDuration, intensity: 10);
            }

            else if (ev.Candy is HauntedCandyGreen)
            {
                ev.Player.AddEffect<SugarHigh>();
            }
        }
    }
}