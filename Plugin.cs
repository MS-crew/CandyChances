using System;
using HarmonyLib;
using Exiled.API.Features;
using Scp330 = Exiled.Events.Handlers.Scp330;

namespace CandyChances
{
    public class Plugin : Plugin<Config, Translation>
    {
        private Harmony harmony;

        public static EventHandlers eventHandlers;

        public override string Author => "ZurnaSever";

        public override string Name => "CandyChances";

        public override string Prefix => "CandyChances";

        public static Plugin Instance { get; private set; }

        public override Version Version { get; } = new Version(2, 0, 1);

        public override Version RequiredExiledVersion { get; } = new Version(9, 0, 0);

        public override void OnEnabled()
        {
            Instance = this;
            eventHandlers = new EventHandlers();

            Scp330.InteractingScp330 += eventHandlers.OnInteractingScp330;

            harmony = new Harmony("CandyChances" + DateTime.Now.Ticks);
            harmony.PatchAll();

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Scp330.InteractingScp330 -= eventHandlers.OnInteractingScp330;

            harmony.UnpatchAll(harmonyID: "CandyChances");

            eventHandlers = null;
            Instance = null;
            base.OnDisabled();
        }
    }
}
