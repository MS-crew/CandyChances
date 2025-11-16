using CustomPlayerEffects;

using Exiled.API.Enums;
using Exiled.Events.EventArgs.Player;

using PlayerRoles.FirstPersonControl.Thirdperson.Subcontrollers.Wearables;

namespace CandyChances.Components
{
    public class White : Effect
    {
        protected override float Duration => 25;

        private WearableElements wearableCache;

        protected override void SubscribeEvents()
        {
            base.SubscribeEvents();
            Exiled.Events.Handlers.Player.ChangingItem += OnChangingItem;
            Exiled.Events.Handlers.Player.SearchingPickup += OnSearchingPickup;
        }

        protected override void UnsubscribeEvents()
        {
            base.UnsubscribeEvents();
            Exiled.Events.Handlers.Player.ChangingItem -= OnChangingItem;
            Exiled.Events.Handlers.Player.SearchingPickup -= OnSearchingPickup;
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
            ev.Player.DropItem(ev.Item);
        }

        public override void OnEffectDisabled()
        {
            Player.DisableEffect(EffectType.Fade);
            Player.DisableEffect(EffectType.Ghostly);
            Player.DisableEffect(EffectType.SilentWalk);
            Player.ReferenceHub.EnableWearables(wearableCache);
        }

        public override void OnEffectEnabled()
        {
            Player.RemoveEffect<Metal>();
            Player.RemoveEffect<OrangeCandy>();

            WhiteCandy.FlickerLights(Player.Position);

            wearableCache = WearableSync.GetFlags(Player.ReferenceHub);
            Player.ReferenceHub.DisableWearables((WearableElements)7);

            Player.EnableEffect(EffectType.Ghostly);
            Player.EnableEffect(EffectType.Fade, intensity: 240);
            Player.EnableEffect(EffectType.SilentWalk, intensity: 20);
        }

        public override void OnEffectUpdate() { }
    }
}
