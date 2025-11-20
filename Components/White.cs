using Exiled.API.Enums;
using CustomPlayerEffects;
using Exiled.Events.EventArgs.Player;
using PlayerRoles.FirstPersonControl.Thirdperson.Subcontrollers.Wearables;

namespace CandyChances.Components
{
    public class White : Effect
    {
        protected override float Duration => 25;

        private const int FadeIntensity = 240;
        private const int SilentWalkIntensity = 20;
        private const WearableElements BlockedWearables = WearableElements.Armor | WearableElements.Scp1344Goggles | WearableElements.Scp268Hat;

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

            if (ev.Item != null) ev.Player.DropItem(ev.Item);
        }
        public override void OnEffectEnabled()
        {
            Player.RemoveEffect<Metal>();
            Player.RemoveEffect<OrangeCandy>();

            WhiteCandy.FlickerLights(Player.Position);

            wearableCache = WearableSync.GetFlags(Player.ReferenceHub);
            Player.ReferenceHub.DisableWearables(BlockedWearables);

            Player.EnableEffect(EffectType.Ghostly);
            Player.EnableEffect(EffectType.Fade, intensity: FadeIntensity);
            Player.EnableEffect(EffectType.SilentWalk, intensity: SilentWalkIntensity);
        }

        public override void OnEffectDisabled()
        {
            Player.DisableEffect(EffectType.Fade);
            Player.DisableEffect(EffectType.Ghostly);
            Player.DisableEffect(EffectType.SilentWalk);

            Player.ReferenceHub.EnableWearables(wearableCache);
        }

        public override void OnEffectUpdate() { }
    }
}
