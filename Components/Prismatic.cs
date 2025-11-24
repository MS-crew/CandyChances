using System.Collections.Generic;

using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;

using Hazards;

using MEC;

using Mirror;

using PlayerRoles.PlayableScps.Scp939;

using PlayerStatsSystem;

using UnityEngine;

namespace CandyChances.Components
{
    public class Prismatic : Effect
    {
        protected override float Duration => 3f;
        protected override UpdateMode UpdateMode => UpdateMode.None;

        private const float HealPerTick = 5f;
        private const float HealTickDelay = 1f;

        private const float DeathSaveHealth = 1f;

        private const float TeslaImmunityDuration = 2f;
        private const float DamageImmunityDuration = 0.25f;

        private const float ReducedDamageMultiplier = 0.85f;

        private float lastSaveTime;
        private CoroutineHandle regenHandle;

        public PrismaticCloud OriginCloud { get; set; }

        private bool IsDamageImmune => lastSaveTime + DamageImmunityDuration >= Time.timeSinceLevelLoad;

        private bool IsTeslaImmune => lastSaveTime + TeslaImmunityDuration >= Time.timeSinceLevelLoad;

        protected override void SubscribeEvents()
        {
            base.SubscribeEvents();
            Exiled.Events.Handlers.Player.Hurting += OnHurting;
        }

        protected override void UnSubscribeEvents()
        {
            base.UnSubscribeEvents();
            Exiled.Events.Handlers.Player.Hurting -= OnHurting;
        }

        public override void OnEffectEnabled()
        {
            regenHandle = Timing.RunCoroutine(HealLoop().CancelWith(gameObject));
        }

        public override void OnEffectDisabled()
        {
            Timing.KillCoroutines(regenHandle);
        }

        private IEnumerator<float> HealLoop()
        {
            while (enabled && Player != null && Player.IsAlive)
            {
                Player.Heal(HealPerTick);
                yield return Timing.WaitForSeconds(HealTickDelay);
            }
        }

        private void OnHurting(HurtingEventArgs ev)
        {
            if (ev.Player != Player)
                return;

            float baseDamage = ev.Amount;

            if (IsDamageImmune || (IsTeslaImmune && ev.DamageHandler.Type == DamageType.Tesla))
            {
                ev.Amount = 0;
                ev.IsAllowed = false;
                return;
            }

            if (!enabled)
            {
                ev.Amount *= ReducedDamageMultiplier;
                return;
            }

            if ( ev.DamageHandler.Type is DamageType.Scp207 or DamageType.PocketDimension)
            {
                ev.Amount *= ReducedDamageMultiplier;
                return;
            }

            if (ev.DamageHandler.Base is Scp939DamageHandler dmghdl939 && dmghdl939.Scp939DamageType == Scp939DamageType.LungeTarget)
            {
                ev.Amount *= ReducedDamageMultiplier;
                return;
            }

            float hp = Player.Health;
            float ahp = Player.ArtificialHealth;
            float total = hp + ahp;

            if (hp > baseDamage || total > baseDamage)
            {
                ev.Amount *= ReducedDamageMultiplier;
                return;
            }

            enabled = false;

            OriginCloud?.IgnoredTargets.Add(Player.ReferenceHub);

            lastSaveTime = Time.timeSinceLevelLoad;

            ev.Amount = (total - DeathSaveHealth) / baseDamage; 
        }
    }
}
