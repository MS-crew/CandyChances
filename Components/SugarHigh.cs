using System.Collections.Generic;

using MEC;

using UnityEngine;

namespace CandyChances.Components
{
    public class SugarHigh : Effect
    {
        protected override float Duration => 25f;
        protected override UpdateMode UpdateMode => UpdateMode.None;

        private const float ExtraHealth = 200f;
        private const float DisableDuration = 10f;

        private const float RegenPerTick = 6f;
        private const float RegenTickDelay = 1f;
        private const float MinHealtforRegen = 100f;

        private float cachedMaxHealth;
        private CoroutineHandle regenHandle;

        public override void OnEffectEnabled()
        {
            cachedMaxHealth = Player.MaxHealth;

            Player.MaxHealth += ExtraHealth;
            Player.Heal(Player.MaxHealth);

            regenHandle = Timing.RunCoroutine(RegenLoop().CancelWith(gameObject));
        }

        public override void OnEffectDisabled()
        {
            Timing.KillCoroutines(regenHandle);
            Timing.RunCoroutine(ReduceHealthGradually().CancelWith(gameObject));
        }

        private IEnumerator<float> RegenLoop()
        {
            while (Player != null && Player.IsAlive)
            {
                if (Player.Health >= MinHealtforRegen)
                    Player.Heal(RegenPerTick);

                yield return Timing.WaitForSeconds(RegenTickDelay);
            }
        }

        private IEnumerator<float> ReduceHealthGradually()
        {
            if (!Player.IsAlive)
                yield break;

            float disableTimer = 0f;
            float amountPerSecond = ExtraHealth / DisableDuration;

            while (disableTimer < DisableDuration)
            {
                if (Player == null || !Player.IsAlive)
                    yield break;

                float reduceAmount = amountPerSecond * Time.deltaTime;

                Player.MaxHealth = Mathf.Max(Player.MaxHealth - reduceAmount, cachedMaxHealth);
                if (Player.Health > Player.MaxHealth)
                    Player.Health = Player.MaxHealth;

                disableTimer += Time.deltaTime;
                yield return Timing.WaitForOneFrame;
            }
        }
    }
}
