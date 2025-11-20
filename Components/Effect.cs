using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;

using MEC;

using UnityEngine;

using PlayerEvents = Exiled.Events.Handlers.Player;

namespace CandyChances.Components
{
    public abstract class Effect : MonoBehaviour
    {
        public Player Player { get; private set; }
        protected abstract float Duration { get; }

        private CoroutineHandle disableHandler;

        protected virtual void Awake()
        {
            this.Player = Player.Get(gameObject);
            Log.Debug($"[CandyChances] Effect {GetType().Name} has been added to player {Player.Nickname}.");
        }

        protected virtual void OnEnable()
        {
            SubscribeEvents();
            OnEffectEnabled();

            disableHandler = Timing.CallDelayed(Duration, () => enabled = false);
            Log.Debug($"[CandyChances] Effect {GetType().Name} has been enabled for player {Player.Nickname} for {Duration} seconds.");
        }

        protected virtual void SubscribeEvents()
        {
            PlayerEvents.ChangingRole += OnChangingRole;
        }

        protected virtual void UnsubscribeEvents()
        {
            PlayerEvents.ChangingRole -= OnChangingRole;
        }

        private void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (ev.Player != Player)
                return;

            enabled = false;
        }

        protected virtual void OnDisable()
        {
            UnsubscribeEvents();
            Timing.KillCoroutines(disableHandler);
            OnEffectDisabled();
            Log.Debug($"[CandyChances] Effect {GetType().Name} has been disabled for player {Player.Nickname}.");
        }

        protected virtual void Update()
        {
            if (!enabled)
                return;

            this.OnEffectUpdate();
            Log.Debug($"[CandyChances] Effect {GetType().Name} is updating for player {Player.Nickname}.");
        }

        public abstract void OnEffectUpdate();

        public abstract void OnEffectDisabled();

        public abstract void OnEffectEnabled();
    }
}