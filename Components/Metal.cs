using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Roles;
using Exiled.Events.EventArgs.Player;

using PlayerRoles.FirstPersonControl;
using PlayerStatsSystem;

using UnityEngine;

namespace CandyChances.Components
{
    public class Metal : Effect
    {
        protected override float Duration => 30;

        private float damageMultiplier = 0.2f;

        private Vector3 normalGravity, fallGravity;

        private FpcGravityController prevController;

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

        private void OnHurting(HurtingEventArgs ev)
        {
            if (ev.Player != Player)
                return;

            if (ev.IsInstantKill)
                return;

            if (ev.DamageHandler.Type != DamageType.Falldown)
            {
                ev.Amount *= damageMultiplier;
                return;
            }

            if (ev.Amount < 35f)
            {
                ev.Amount = 0;
                return;
            }

            OnServerProcessFall(ev.Amount);
            ev.Amount *= 0.05f;
        }

        private void OnServerProcessFall(float damage)
        {
            float damage2 = 80f + damage * 0.4f;
            foreach (Player player2 in Player.List)
            {
                if (!(player2 == Player) && HitboxIdentity.IsDamageable(Player.ReferenceHub, player2.ReferenceHub))
                {
                    Vector3 delta = player2.Position - Player.Position;
                    float sqrDist = delta.sqrMagnitude;

                    if (player2.IsAlive && sqrDist <= 16f)
                    {
                        player2.Hurt(new GrayCandyDamageHandler(Player.ReferenceHub, damage2));
                        Player.ShowHitMarker();
                    }
                }
            }
        }

        public override void OnEffectUpdate()
        {
            UpdateGravity();
        }

        private void UpdateGravity()
        {
            if (prevController == null)
                return;

            Vector3 vector = (prevController.Motor.Velocity.y < 0f) ? fallGravity : normalGravity;
            if (prevController.Gravity == vector)
                return;

            prevController.Gravity = vector;
        }

        public override void OnEffectEnabled()
        {
            Player.RemoveEffect<White>();
            Player.RemoveEffect<OrangeCandy>();

            //şarki oynat

            if (Player.Role is not FpcRole fpc)
            {
                prevController = null;
                return;
            }


            prevController = fpc.FirstPersonController.FpcModule.Motor.GravityController;
            normalGravity = this.prevController.Gravity;
            fallGravity = this.normalGravity * 4f;
            Player.EnableEffect(EffectType.Slowness, duration: Duration, intensity: 50);
        }

        public override void OnEffectDisabled()
        {
            // şarki durdur

            if (prevController != null)
            {
                prevController.Gravity = normalGravity;
                prevController = null;
            }
        }
    }
}
