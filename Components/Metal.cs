using UnityEngine;
using Exiled.API.Enums;
using PlayerStatsSystem;
using Exiled.API.Features;
using Exiled.API.Features.Roles;
using PlayerRoles.FirstPersonControl;
using Exiled.Events.EventArgs.Player;

namespace CandyChances.Components
{
    public class Metal : Effect
    {
        protected override float Duration => 30;
        protected override UpdateMode UpdateMode => UpdateMode.FixedUpdate;

        private const float DefaultDamageMultiplier = 0.2f;
        private const float MaxFallDamageMultiplier = 0.05f;
        private const float FallDamageIgnoreThreshold = 35f;

        private const float FallBaseDamage = 80f;
        private const float FallScaleMultiplier = 0.4f;

        private const float FallRadius = 4f;
        private const float FallRadiusSqr = FallRadius * FallRadius;

        private const int SlowEffectIntensity = 50;
        private const float FallGravityMultiplier = 4f;
        
        private Vector3 normalGravity, fallGravity;
        private FpcGravityController prevController;

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
            Player.RemoveEffect<White>();
            Player.RemoveEffect<OrangeCandy>();
            Player.EnableEffect(EffectType.Slowness, intensity: SlowEffectIntensity);

            //şarki oynat

            if (Player.Role is not FpcRole fpc)
            {
                prevController = null;
                return;
            }

            prevController = fpc.FirstPersonController.FpcModule.Motor.GravityController;
            normalGravity = prevController.Gravity;
            fallGravity = normalGravity * FallGravityMultiplier;
        }

        public override void OnEffectDisabled()
        {
            // şarki durdur
            Player.DisableEffect(EffectType.Slowness);

            if (prevController != null)
            {
                prevController.Gravity = normalGravity;
                prevController = null;
            }
        }

        protected override void OnEffectUpdate()
        {
            if (prevController == null)
                return;

            Vector3 vector = (prevController.Motor.Velocity.y < 0f) ? fallGravity : normalGravity;
            if (prevController.Gravity == vector)
                return;

            prevController.Gravity = vector;
        }

        private void OnHurting(HurtingEventArgs ev)
        {
            if (ev.Player != Player)
                return;

            if (ev.IsInstantKill)
                return;

            if (ev.DamageHandler.Type != DamageType.Falldown)
            {
                ev.Amount *= DefaultDamageMultiplier;
                return;
            }

            if (ev.Amount < FallDamageIgnoreThreshold)
            {
                ev.Amount = 0;
                return;
            }

            OnServerProcessFall(ev.Amount);
            ev.Amount *= MaxFallDamageMultiplier;
        }

        private void OnServerProcessFall(float damage)
        {
            Vector3 pos = Player.Position;
            float damage2 = FallBaseDamage + damage * FallScaleMultiplier;
            GrayCandyDamageHandler damageHandler = new(Player.ReferenceHub, damage2);

            foreach (Player player2 in Player.List)
            {
                if (player2 == Player)
                    continue;

                if (!player2.IsAlive)
                    continue;

                if (!HitboxIdentity.IsDamageable(Player.ReferenceHub, player2.ReferenceHub))
                    continue;

                Vector3 delta = player2.Position - pos;
                float sqr = delta.sqrMagnitude;

                if (sqr > FallRadiusSqr)
                    continue;

                player2.Hurt(damageHandler);
                Player.ShowHitMarker();
            }
        }
    }
}
