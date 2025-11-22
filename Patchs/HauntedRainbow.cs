using System.Collections.Generic;
using System.Reflection.Emit;

using CandyChances.Components;

using Exiled.API.Features;

using HarmonyLib;

using Hazards;

using InventorySystem.Items.Usables.Scp330;

using Mirror;

using RelativePositioning;

using UnityEngine;

using static HarmonyLib.AccessTools;

namespace CandyChances.Patchs
{
    [HarmonyPatch(typeof(PrismaticCloud), nameof(PrismaticCloud.ServerEnableEffect))]
    internal static class PismaticCloudPatch
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> _, ILGenerator generator)
        {
            Label setOrigin = generator.DefineLabel();

            LocalBuilder prismatic = generator.DeclareLocal(typeof(Prismatic));

            List<CodeInstruction> newInstructions = new()
            {
                new(OpCodes.Ldarg_1),
                new(OpCodes.Call, Method(typeof(Player), nameof(Player.Get), [typeof(ReferenceHub)])),
                new(OpCodes.Call, Method(typeof(Extensions), nameof(Extensions.AddEffect)).MakeGenericMethod(typeof(Prismatic))),
                new(OpCodes.Stloc_S, prismatic.LocalIndex),

                new(OpCodes.Ldarg_2),
                new(OpCodes.Brtrue_S, setOrigin),
                new(OpCodes.Ret),

                new CodeInstruction(OpCodes.Ldloc_S, prismatic.LocalIndex).WithLabels(setOrigin),
                new(OpCodes.Ldarg_0),
                new(OpCodes.Callvirt, PropertySetter(typeof(Prismatic), nameof(Prismatic.OriginCloud))),

                new(OpCodes.Ret),
            };

            return newInstructions;
        }
    }

    [HarmonyPatch(typeof(HauntedCandyRainbow), nameof(HauntedCandyRainbow.ServerApplyEffects))]
    internal static class HauntedRainbow
    { 
        private static bool Prefix(HauntedCandyRainbow __instance, ReferenceHub hub)
        {
            Player player = Player.Get(hub);
            if (!Physics.Raycast(player.Position, Vector3.down, out RaycastHit hitInfo, HauntedCandyRainbow.RayMaxDistance, HauntedCandyRainbow.Layer))
                return false;

            PrismaticCloud prismaticCloud = Object.Instantiate(__instance.Cloud);
            Vector3 targetPos = hitInfo.point + Vector3.up * HauntedCandyRainbow.CloudHeight;

            prismaticCloud.SynchronizedPosition = new RelativePosition(targetPos);

            NetworkServer.Spawn(prismaticCloud.gameObject);

            player.AddEffect<Prismatic>().OriginCloud = prismaticCloud;
            return false;
        }
    }
}
