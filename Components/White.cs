using System;
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

        private bool tagStatusCache;
        private PlayerInfoArea infoAreaChache;
        private const int FrameSize = 480;

        private static readonly int DelayMain = 42;
        private static readonly int DelaySmall = 22; 

        private const float VolumeMain = 1;
        private const float VolumeSmall = 0.6f;
        private const float Feedback = 0.3f;

        private readonly Queue<float[]> qMain = new();
        private readonly Queue<float[]> qSmall = new();
        
        private readonly Stack<float[]> framePool = new();

        private readonly byte[] encodedBuffer = new byte[512];
        private readonly float[] lastFrame = new float[FrameSize];

        private const int FadeIntensity = 240;
        private const int SilentWalkIntensity = 20;

        private const float MinSpeakerDistance = 1;
        private const float MaxSpeakerDistance = 20;

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
                echoSpeaker.MinDistance = MinSpeakerDistance;
                echoSpeaker.MaxDistance = MaxSpeakerDistance;
            }

            decoder = new OpusDecoder();
            encoder = new OpusEncoder(OpusApplicationType.Voip);

            infoAreaChache = Player.InfoArea;
            tagStatusCache = Player.BadgeHidden;

            Player.BadgeHidden = false;
            Player.InfoArea = PlayerInfoArea.PowerStatus;
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

            qMain.Clear();
            qSmall.Clear();
            framePool.Clear();

            Player.InfoArea = infoAreaChache;
            Player.BadgeHidden = tagStatusCache;
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

            float[] dry = RentFrame();
            int decoded = decoder.Decode(ev.VoiceMessage.Data, ev.VoiceMessage.DataLength, dry);

            for (int i = decoded; i < FrameSize; i++)
                dry[i] = 0f;

            qMain.Enqueue(CopyFrame(dry));
            qSmall.Enqueue(CopyFrame(dry));

            if (qMain.Count < DelayMain || qSmall.Count < DelaySmall)
            {
                ReturnFrame(dry);
                yield break;
            }

            float[] echoMain = qMain.Dequeue();
            float[] echoSmall = qSmall.Dequeue();
            float[] output = RentFrame();

            for (int i = 0; i < FrameSize; i++)
            {
                float v = echoMain[i] * VolumeMain + echoSmall[i] * VolumeSmall + lastFrame[i] * Feedback;

                output[i] = v;
                lastFrame[i] = v * 0.97f;
            }

            int encoded = encoder.Encode(output, encodedBuffer, FrameSize);
            AudioMessage msg = new(echoSpeaker.ControllerId, encodedBuffer, encoded);
            NetworkServer.SendToReady(msg, VoiceChatSettings.Channels);

            ReturnFrame(dry);
            ReturnFrame(echoMain);
            ReturnFrame(echoSmall);
            ReturnFrame(output);
        }

        private float[] CopyFrame(float[] f)
        {
            float[] n = RentFrame();
            Array.Copy(f, n, FrameSize);
            return n;
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
