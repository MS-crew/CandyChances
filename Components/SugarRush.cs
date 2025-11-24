using InventorySystem.Items.Usables.Scp330;

using Exiled.API.Features;
using Exiled.API.Features.Roles;

using UnityEngine;

namespace CandyChances.Components
{
    public class SugarRush : Effect
    {
        protected override float Duration => HauntedCandyYellow.SugarRushDuration;
        protected override UpdateMode UpdateMode => UpdateMode.FixedUpdate;

        private float sprintMult;
        private const float SpeedBoost = 2.2f;
        private const float PlayerRadius = 0.3f;
        private static readonly int WorldMask =LayerMask.GetMask("Default");

        public override void OnEffectEnabled()
        {
            if (Player.Role is not FpcRole fpc)
                return;

            sprintMult = fpc.SprintingSpeed * SpeedBoost;
        }

        public override void OnEffectDisabled() { }
        protected override void OnEffectUpdate()
        {
            Vector3 pos = Player.Position;
            Vector3 forward = Player.Transform.forward;

            float distance = sprintMult * Time.deltaTime;
            

            if (Physics.SphereCast(pos, PlayerRadius, forward, out RaycastHit hit, distance + 0.01f, WorldMask, QueryTriggerInteraction.Ignore))
            {
                if (hit.distance > 0)
                {
                    float safeDistance = hit.distance - 0.01f;
                    if (safeDistance > 0)
                    {
                        Player.Position = pos + forward * safeDistance;
                    }
                }
                return;
            }

            Player.Position = pos + forward * distance;
        }
    }
}
