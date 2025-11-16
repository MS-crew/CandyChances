using CustomPlayerEffects;

using Exiled.API.Enums;
using Exiled.Events.EventArgs.Player;

using PlayerRoles.FirstPersonControl.Thirdperson.Subcontrollers.Wearables;

namespace CandyChances.Components
{
    public class White : Effect
    {
        protected override float Duration => 25;

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
            Player.ReferenceHub.EnableWearables((WearableElements)7);
        }

        public override void OnEffectEnabled()
        {
            Player.RemoveEffect<Metal>();
            Player.RemoveEffect<OrangeCandy>();

            WhiteCandy.FlickerLights(Player.Position);
            Player.ReferenceHub.DisableWearables((WearableElements)7);
            Player.EnableEffect(EffectType.Ghostly, duration: Duration);
            Player.EnableEffect(EffectType.SilentWalk, intensity:20, duration: Duration);
            Player.EnableEffect(EffectType.Fade, intensity: 240, duration: Duration);
        }

        public override void OnEffectUpdate() { }
    }
}
