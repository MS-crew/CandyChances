using System.Collections.Generic;

using Exiled.API.Enums;
using Exiled.Events.EventArgs.Player;

using MEC;

using UnityEngine;

namespace CandyChances.Components
{
    public class Prismatic : Effect
    {
        protected override float Duration => 3f;

        private const float HealPerTick = 5f;
        private const float DeathSaveHealth = 1f;
        private const float TeslaImmunityTime = 2f;
        private const float DamageImmunityTime = 0.25f;

        private float lastSaveTime;

        private CoroutineHandle regenHandle;

        public object OriginCloud { get; set; }

        private bool IsHitImmunityActive => lastSaveTime + DamageImmunityTime >= Time.timeSinceLevelLoad;
        private bool IsTeslaImmunityActive => lastSaveTime + TeslaImmunityTime >= Time.timeSinceLevelLoad; 

        protected override void SubscribeEvents()
        {
            base.SubscribeEvents();
            Exiled.Events.Handlers.Player.Hurting += OnHurting;
        }

        protected override void UnsubscribeEvents()
        {
            base.UnsubscribeEvents();
            Exiled.Events.Handlers.Player.Hurting -= OnHurting;
        }

        public override void OnEffectEnabled()
        {
            regenHandle = Timing.RunCoroutine(HealLoop());
        }

        public override void OnEffectDisabled()
        {
            Timing.KillCoroutines(regenHandle);
        }

        private IEnumerator<float> HealLoop()
        {
            while (Player != null && Player.IsAlive && enabled)
            {
                Player.Heal(HealPerTick);
                yield return Timing.WaitForSeconds(1f);
            }
        }


        private void OnHurting(HurtingEventArgs ev)
        {
            if (ev.Player != Player)
                return;

            if (IsHitImmunityActive)
            {
                ev.Amount = 0;
                return;
            }

            if (IsTeslaImmunityActive && ev.DamageHandler.Type == DamageType.Tesla)
            {
                ev.Amount = 0;
                return;
            }

            if (!enabled)
            {
                ev.Amount *= 0.85f;
                return;
            }

            float hp = Player.Health;
            float ahp = Player.ArtificialHealth;
            float total = hp + ahp;

            if (hp > ev.Amount || total > ev.Amount)
            {
                ev.Amount *= 0.85f;
                return;
            }

            enabled = false;

            if (OriginCloud is Hazards.PrismaticCloud cloud && cloud.IgnoredTargets != null)
                cloud.IgnoredTargets.Add(Player.ReferenceHub);

            lastSaveTime = Time.timeSinceLevelLoad;
            ev.Amount = Mathf.Max(total - DeathSaveHealth, 0f);

            Player.Health = DeathSaveHealth;
        }

        public override void OnEffectUpdate() { }
    }
}
