using System.Collections.Generic;

using AdminToys;
using PlayerRoles;
using PlayerStatsSystem;
using UnityEngine;

using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Toys;
using Exiled.Events.EventArgs.Player;

using DG.Tweening;
using Mirror;

using PlayerHandlers = Exiled.Events.Handlers.Player;

namespace CandyChances.Components
{
    public class Spicy : Effect
    {
        protected override float Duration => 10f;
        protected override UpdateMode UpdateMode => UpdateMode.LateUpdate;

        private Transform fireRoot;

        private const float TweenTime = 0.25f;
        private readonly List<Primitive> spheres = [];
        private readonly List<Sequence> sequences = [];
        private static Color startColor = new(5.34f, 2.3f, 0);
        private readonly HashSet<uint> alreadyDamagedEntities = [];
        private static readonly FireSphereTarget[] Targets =
        [
            new(0, 0, 2.3f,           2.8f,2.9f,2.4f,       5.34f,0.0443f,0f),
            new(0f,0f, 2.855f,         1f,1f,1f,            5.34f,0.0443f,0f),
            new(-0.571f,0.657f, 3.1f,  1f,1f,2.5f,          5.34f,0.0443f,0f),
            new(0f,0f, 4.4f,           3f,1f,2.4f,          5.34f,0.0443f,0f),
            new(0f,0f, 2.887f,         1f,1f,1f,            5.34f,0.0443f,0f),
            new(0f,0f, 2.895f,         3f,1f,2.2636f,       5.34f,0.0443f,0f),
            new(0.29f,0f, 2f,      2.1919f,1.7938f,1.5741f, 5.34f,0.0443f,0f),
            new(0f,0.831f, 4f,         1f,2.1f,3.3817f,     5.34f,0.0443f,0f),
            new(0.593f,-0.543f, 3.722f,1f,2f,2.4208f,       5.34f,0.0443f,0f)
        ];


        public override void OnEffectEnabled()
        {
            SpawnSpheres();
            CreateDOTweenSequence();
        }

        public override void OnEffectDisabled()
        {
            KillSequence();
            DestroySpheres();
            alreadyDamagedEntities.Clear();
        }

        protected override void SubscribeEvents()
        {
            base.SubscribeEvents();
            PlayerHandlers.ChangingItem += OnChangingItem;
            PlayerHandlers.SearchingPickup += OnSearchingPickup;
        }

        protected override void UnSubscribeEvents()
        {
            base.UnSubscribeEvents();
            PlayerHandlers.ChangingItem -= OnChangingItem;
            PlayerHandlers.SearchingPickup -= OnSearchingPickup;
        }
        private void OnSearchingPickup(SearchingPickupEventArgs ev)
        {
            if (ev.Player != Player)
                return;

            ev.IsAllowed = false;
        }

        private void OnChangingItem(ChangingItemEventArgs ev)
        {
            if (ev.Player != Player)
                return;

            ev.IsAllowed = false;
        }

        private void SpawnSpheres()
        {
            Primitive prim = Primitive.Create(primitiveType: PrimitiveType.Cube, spawn: false);
            prim.Flags = PrimitiveFlags.None;

            fireRoot = prim.Transform;

            fireRoot.SetParent(Player.Transform, false);
            fireRoot.localPosition = Vector3.up * 0.5f;

            NetworkServer.Spawn(prim.GameObject);

            for (int i = 0; i < Targets.Length; i++)
            {
                Primitive p = Primitive.Create(PrimitiveType.Sphere, spawn: false);

                p.Color = startColor;
                p.Flags = PrimitiveFlags.Visible;
                p.AdminToyBase.MovementSmoothing = 60;
                p.Transform.SetParent(fireRoot, false);
                p.Transform.localPosition = Vector3.zero;

                spheres.Add(p);

                NetworkServer.Spawn(p.GameObject);
            }

            prim.Scale *= 0.5f;
        }

        private void CreateDOTweenSequence()
        {
            KillSequence();

            for (int i = 0; i < spheres.Count; i++)
            {
                Primitive toy = spheres[i];
                FireSphereTarget t = Targets[i];

                Sequence seq = DOTween.Sequence().
                    Join(toy.Transform.DOLocalMove(t.Pos, TweenTime).SetEase(Ease.OutQuad)).
                    Join(toy.Transform.DOScale(t.Scale, TweenTime).SetEase(Ease.OutQuad)).
                    Join(DOTween.To(() => toy.Color, c => toy.Color = c, t.Color, TweenTime)).
                    SetLoops(-1, LoopType.Restart);

                sequences.Add(seq);
            }
        }

        private void KillSequence()
        {
            foreach (Sequence s in sequences)
                s?.Kill();

            sequences.Clear();
        }

        private void DestroySpheres()
        {
            NetworkServer.Destroy(fireRoot?.gameObject);

            spheres.Clear();
        }

        protected override void OnEffectUpdate()
        {
            Vector3 pos = Player.Position;
            Vector3 forward = Player.CameraTransform.forward;

            bool hitmarker = false;

            foreach (Player target in Player.List)
            {
                if (target == Player)
                    continue;

                Vector3 delta = target.Position - pos;
                float sqrDist = delta.sqrMagnitude;

                if (sqrDist > 25f)
                    continue;

                if (!HitboxIdentity.IsDamageable(Player.ReferenceHub, target.ReferenceHub))
                    continue;

                float dist = Mathf.Sqrt(sqrDist);
                Vector3 dir = delta / dist;

                bool omniHit = dist < 0.7f;

                float dot = Vector3.Dot(dir, forward);
                bool coneHit = dot >= 0.75f;

                if (!omniHit && !coneHit)
                    continue;

                if (Physics.Raycast(pos, dir, dist, PlayerRolesUtils.AttackMask))
                    continue;

                if (!alreadyDamagedEntities.Add(target.NetId))
                    continue;

                target.EnableEffect(EffectType.Burned, 60f);

                float damage = 15f;
                float hume = target.HumeShield;
                damage = (damage * 3f < hume) ? damage * 3f : damage + hume / 3f;

                ExplosionDamageHandler handler = new(Player.Footprint, Vector3.zero, damage, 100, ExplosionType.Custom);
                target.Hurt(handler);

                if (Hitmarker.CheckHitmarkerPerms(handler, target.ReferenceHub))
                    hitmarker = true;
            }

            if (hitmarker)
                Player.ShowHitMarker();

            alreadyDamagedEntities.Clear();
        }

        private struct FireSphereTarget(float px, float py, float pz, float sx, float sy, float sz, float r, float g, float b)
        {
            public Vector3 Pos = new(px, py, pz);
            public Vector3 Scale = new(sx, sy, sz);
            public Color Color = new(r, g, b, 0.25f);
        }

    }
}
