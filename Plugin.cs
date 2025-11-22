using System;

using CandyChances.Patchs;

using Exiled.API.Features;

using HarmonyLib;

using Scp330 = Exiled.Events.Handlers.Scp330;
using Server = Exiled.Events.Handlers.Server;

namespace CandyChances
{
    public class Plugin : Plugin<Config, Translation>
    {
        private Harmony harmony;

        private EventHandlers eventHandlers;

        internal static Plugin Instance { get; private set; }

        public override string Author => "ZurnaSever";

        public override string Name => "Candy Chances";

        public override string Prefix => "CandyChances";

        public override Version Version { get; } = new Version(3, 2, 1);

        public override Version RequiredExiledVersion { get; } = new Version(9, 10, 0);

        public override void OnEnabled()
        {
            Instance = this;
            eventHandlers = new EventHandlers();

            Server.RoundStarted += eventHandlers.OnRoundStarted;

            Scp330.EatenScp330 += eventHandlers.OnEatenScp330;
            Scp330.InteractingScp330 += eventHandlers.OnInteractingScp330;

            harmony = new Harmony(Prefix + DateTime.Now.Ticks);
            DoDynamicPatchs(harmony);

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Server.RoundStarted -= eventHandlers.OnRoundStarted;

            Scp330.EatenScp330 -= eventHandlers.OnEatenScp330;
            Scp330.InteractingScp330 -= eventHandlers.OnInteractingScp330;

            harmony.UnpatchAll(harmony.Id);
            harmony = null;
            eventHandlers = null;
            Instance = null;

            base.OnDisabled();
        }

        private void DoDynamicPatchs(Harmony harmony)
        {
            harmony.PatchSingleType(typeof(CandyChanceOverridePatch));

            if (Config.OverrideBowlCandys)
            {
                harmony.PatchSingleType(typeof(Scp330CandiesPatch));

                if (Config.TryReplicateHalloweenCandys)
                {
                    harmony.PatchSingleType(typeof(HauntedRainbow));
                    harmony.PatchSingleType(typeof(PismaticCloudPatch));
                }
            }
        }
    }
}
