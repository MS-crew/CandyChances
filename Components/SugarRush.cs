using Exiled.API.Features;
using Exiled.API.Features.Roles;

using InventorySystem.Items.Usables.Scp330;

using PlayerRoles.FirstPersonControl;

using UnityEngine;

namespace CandyChances.Components
{
    public class SugarRush : Effect
    {
        protected override float Duration => HauntedCandyYellow.SugarRushDuration;
        protected override UpdateMode UpdateMode => UpdateMode.FixedUpdate;

        private FpcMotor motor;
        private float sprintSpeed;

        private const float SpeedBoost = 2.2f;

        public override void OnEffectEnabled()
        {
            if (motor != null)
                return;

            if (Player.Role is not FpcRole fpc)
                return;

            sprintSpeed = fpc.SprintingSpeed;
            motor = fpc.FirstPersonController.FpcModule.Motor;
        }

        public override void OnEffectDisabled() 
        { 
            motor = null;
        }

        protected override void OnEffectUpdate()
        {
            if (motor == null)
                return;

            Vector3 pos = motor.Position;
            Vector3 dir = motor.CachedTransform.forward;

            dir.y = 0f;
            dir.Normalize();

            Vector3 target = pos + dir * (sprintSpeed * SpeedBoost) * Time.deltaTime;
            
            if ((target - pos).sqrMagnitude > 0.05f)
            {
                Player.Position = target; 
            }
        }
    }
}
