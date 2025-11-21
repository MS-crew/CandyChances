using System.Collections.Generic;

using CustomPlayerEffects;

using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Toys;
using Exiled.Events.EventArgs.Player;

using InventorySystem.Items;

using Mirror;

using PlayerRoles.FirstPersonControl.Thirdperson.Subcontrollers.Wearables;

using VoiceChat;
using VoiceChat.Codec;
using VoiceChat.Codec.Enums;
using VoiceChat.Networking;

using PlayerHandlers = Exiled.Events.Handlers.Player;

namespace CandyChances.Components
{
    public class White : Effect
    {
        protected override float Duration => 25;

        private OpusDecoder decoder;
        private OpusEncoder encoder;
        private Speaker echoSpeaker;

        private string customInfoCache;
        private PlayerInfoArea infoAreaChache;

        private readonly Stack<float[]> framePool = new();
        private readonly Queue<float[]> echoQueue = new();
        private readonly byte[] encodedBuffer = new byte[512];
        
        private const int DelayFrames = 10;
        private const float EchoVolume = 0.70f;

        private const int FadeIntensity = 240;
        private const int SilentWalkIntensity = 20;

        private const WearableElements BlockedWearables = WearableElements.Armor | WearableElements.Scp1344Goggles;

        public override void OnEffectEnabled()
        {
            Player.RemoveEffect<Metal>();
            Player.RemoveEffect<OrangeCandy>();

            WhiteCandy.FlickerLights(Player.Position);
            Player.ReferenceHub.DisableWearables(BlockedWearables);

            Player.EnableEffect(EffectType.Ghostly);
            Player.EnableEffect(EffectType.Fade, intensity: FadeIntensity);
            Player.EnableEffect(EffectType.SilentWalk, intensity: SilentWalkIntensity);

            if (echoSpeaker == null) 
            {
                echoSpeaker = Speaker.Create(Player.Transform, true);
                echoSpeaker.ControllerId = (byte)Player.Id;
                echoSpeaker.IsSpatial = true;
            }

            decoder = new OpusDecoder();
            encoder = new OpusEncoder(OpusApplicationType.Voip);

            customInfoCache = Player.CustomInfo;
            infoAreaChache = Player.InfoArea;

            Player.CustomInfo = "\u200B";//NOT WORK IDK WHY
            Player.InfoArea = PlayerInfoArea.CustomInfo;
        }

        public override void OnEffectDisabled()
        {
            Player.DisableEffect(EffectType.Fade);
            Player.DisableEffect(EffectType.Ghostly);
            Player.DisableEffect(EffectType.SilentWalk);

            Player.ReferenceHub.EnableWearables(ActiveWearables());

            echoSpeaker?.Destroy();
            echoSpeaker = null;

            decoder?.Dispose();
            encoder?.Dispose();
            decoder = null;
            encoder = null;

            echoQueue.Clear();
            framePool.Clear();

            Player.CustomInfo = customInfoCache;
            Player.InfoArea = infoAreaChache;
        }

        protected override void SubscribeEvents()
        {
            base.SubscribeEvents();
            PlayerHandlers.ChangingItem += OnChangingItem;
            PlayerHandlers.VoiceChatting += OnVoiceChatting;
            PlayerHandlers.SearchingPickup += OnSearchingPickup;
        }

        protected override void UnsubscribeEvents()
        {
            base.UnsubscribeEvents();
            PlayerHandlers.ChangingItem -= OnChangingItem; 
            PlayerHandlers.VoiceChatting -= OnVoiceChatting;
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

            if (ev.Item != null) ev.Player.DropItem(ev.Item);
        }

        private IEnumerator<float> OnVoiceChatting(VoiceChattingEventArgs ev)
        {
            if (ev.Player != Player)
                yield break;

            float[] frame = RentFrame();
            int decoded = decoder.Decode(ev.VoiceMessage.Data, ev.VoiceMessage.DataLength, frame);

            for (int i = decoded; i < 480; i++)
                frame[i] = 0f;

            for (int i = 0; i < 480; i++)
                frame[i] *= EchoVolume;

            echoQueue.Enqueue(frame);

            if (echoQueue.Count < DelayFrames)
                yield break;

            float[] delayed = echoQueue.Dequeue();

            int encoded = encoder.Encode(delayed, encodedBuffer, 480);
            AudioMessage msg = new(echoSpeaker.ControllerId, encodedBuffer, encoded);
            NetworkServer.SendToReady(msg, VoiceChatSettings.Channels);

            ReturnFrame(delayed);
        }

        private WearableElements ActiveWearables()
        {
            WearableElements wearables = WearableElements.None;

            foreach (ItemBase item in Player.Inventory.UserInventory.Items.Values)
            {
                if (item.ItemTypeId.IsArmor())
                    wearables |= WearableElements.Armor;

                if (item.ItemTypeId == ItemType.SCP1344)
                    wearables |= WearableElements.Scp1344Goggles;
            }

            return wearables;
        }
        
        public override void OnEffectUpdate() { }
        private void ReturnFrame(float[] f) => framePool.Push(f);
        private float[] RentFrame() => framePool.Count > 0 ? framePool.Pop() : new float[480];
    }
}
