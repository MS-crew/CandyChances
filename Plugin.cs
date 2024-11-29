using System;
using HarmonyLib;
using Exiled.API.Features;

namespace CandyChances
{
    public class Plugin : Plugin<Config>
    {
        public static Plugin Instance { get; private set; }

        public override string Author => "ZurnaSever";

        public override string Name => "CandyChances";

        public override string Prefix => "CandyChances";

        public override Version RequiredExiledVersion { get; } = new Version(9, 0, 0);

        public override Version Version { get; } = new Version(1, 0, 0);
        private Harmony harmony;
        public override void OnEnabled()
        {
            Instance = this;
            harmony = new Harmony("CandyChances");
            harmony.PatchAll();
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            Instance = null;
            harmony.UnpatchAll(harmonyID: "CandyChances");
            base.OnDisabled();
        }
    }
}
