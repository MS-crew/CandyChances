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
        protected abstract UpdateMode UpdateMode { get; }


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
            SubscribeUpdate();

            disableHandler = Timing.CallDelayed(Duration, () => enabled = false);
            Log.Debug($"[CandyChances] Effect {GetType().Name} has been enabled for player {Player.Nickname} for {Duration} seconds.");
        }
        protected virtual void OnDisable()
        {
            UnSubscribeUpdate();
            UnSubscribeEvents();
            Timing.KillCoroutines(disableHandler);
            OnEffectDisabled();
            
            Log.Debug($"[CandyChances] Effect {GetType().Name} has been disabled for player {Player.Nickname}.");
        }

        protected virtual void SubscribeUpdate()
        {
            switch (UpdateMode)
            {
                case UpdateMode.Update:
                    StaticUnityMethods.OnUpdate += OnEffectUpdate;
                    break;

                case UpdateMode.LateUpdate:
                    StaticUnityMethods.OnLateUpdate += OnEffectUpdate;
                    break;

                case UpdateMode.FixedUpdate:
                    StaticUnityMethods.OnFixedUpdate += OnEffectUpdate;
                    break;

                case UpdateMode.None:
                    break;
            }
        }

        protected virtual void UnSubscribeUpdate()
        {
            switch (UpdateMode)
            {
                case UpdateMode.Update:
                    StaticUnityMethods.OnUpdate -= OnEffectUpdate;
                    break;

                case UpdateMode.LateUpdate:
                    StaticUnityMethods.OnLateUpdate -= OnEffectUpdate;
                    break;

                case UpdateMode.FixedUpdate:
                    StaticUnityMethods.OnFixedUpdate -= OnEffectUpdate;
                    break;

                case UpdateMode.None:
                    break;
            }
        }

        protected virtual void SubscribeEvents()
        {
            PlayerEvents.ChangingRole += OnChangingRole;
        }

        protected virtual void UnSubscribeEvents()
        {
            PlayerEvents.ChangingRole -= OnChangingRole;
        }

        private void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (ev.Player != Player)
                return;

            enabled = false;
        }

        public abstract void OnEffectEnabled();

        public abstract void OnEffectDisabled();

        protected virtual void OnEffectUpdate() { }
    }

    public enum UpdateMode
    {
        None,
        Update,
        LateUpdate,
        FixedUpdate,
    }
}