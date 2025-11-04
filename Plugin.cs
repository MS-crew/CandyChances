using System;

using Exiled.API.Features;
using HarmonyLib;

using Scp330 = Exiled.Events.Handlers.Scp330;
using Server = Exiled.Events.Handlers.Server;

namespace CandyChances
{
    public class Plugin : Plugin<Config, Translation>
    {
        private Harmony _harmony;

        private EventHandlers _eventHandlers;

        internal static Plugin Instance { get; private set; }

        public override string Author => "ZurnaSever";

        public override string Name => "Candy Chances";

        public override string Prefix => "CandyChances";

        public override Version Version { get; } = new Version(2, 3, 0);

        public override Version RequiredExiledVersion { get; } = new Version(9, 10, 0);

        public override void OnEnabled()
        {
            Instance = this;
            _eventHandlers = new EventHandlers();

            Server.RoundStarted += _eventHandlers.OnRoundStarted;
            Scp330.InteractingScp330 += _eventHandlers.OnInteractingScp330;

            _harmony = new Harmony(Prefix + DateTime.Now.Ticks);
            _harmony.PatchAll();

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Server.RoundStarted -= _eventHandlers.OnRoundStarted;
            Scp330.InteractingScp330 -= _eventHandlers.OnInteractingScp330;

            _harmony.UnpatchAll(_harmony.Id);
            _harmony = null;
            _eventHandlers = null;
            Instance = null;

            base.OnDisabled();
        }
    }
}
