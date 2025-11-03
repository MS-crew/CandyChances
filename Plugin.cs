using System;
using HarmonyLib;
using Exiled.API.Features;
using Scp330 = Exiled.Events.Handlers.Scp330;
using Server = Exiled.Events.Handlers.Server;

namespace CandyChances
{
    public class Plugin : Plugin<Config, Translation>
    {
        private Harmony harmony;

        private EventHandlers eventHandlers;

        public override string Author => "ZurnaSever";

        public override string Name => "Candy Chances";

        public override string Prefix => "CandyChances";

        public static Plugin Instance { get; private set; }

        public override Version Version { get; } = new Version(2, 3, 0);

        public override Version RequiredExiledVersion { get; } = new Version(9, 10, 0);

        public override void OnEnabled()
        {
            Instance = this;
            eventHandlers = new EventHandlers();

            Server.RoundStarted += eventHandlers.OnRoundStarted;
            Scp330.InteractingScp330 += eventHandlers.OnInteractingScp330;

            harmony = new Harmony(Prefix + DateTime.Now.Ticks);
            harmony.PatchAll();

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Server.RoundStarted -= eventHandlers.OnRoundStarted;
            Scp330.InteractingScp330 -= eventHandlers.OnInteractingScp330;

            harmony.UnpatchAll(harmony.Id);
            harmony = null;
            eventHandlers = null;
            Instance = null;

            base.OnDisabled();
        }
    }
}
