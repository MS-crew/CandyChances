using System.Collections.Generic;
using System.Reflection.Emit;

using Exiled.API.Enums;
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

            LocalBuilder prismatic = generator.DeclareLocal(typeof(Components.Prismatic));

            List<CodeInstruction> newInstructions = new()
            {
                new(OpCodes.Ldarg_1),
                new(OpCodes.Call, Method(typeof(Player), nameof(Player.Get), [typeof(ReferenceHub)])),
                new(OpCodes.Call, Method(typeof(Extensions), nameof(Extensions.AddEffect)).MakeGenericMethod(typeof(Components.Prismatic))),
                new(OpCodes.Stloc_S, prismatic.LocalIndex),

                new(OpCodes.Ldarg_2),
                new(OpCodes.Brtrue_S, setOrigin),
                new(OpCodes.Ret),

                new CodeInstruction(OpCodes.Ldloc_S, prismatic.LocalIndex).WithLabels(setOrigin),
                new(OpCodes.Ldarg_0),
                new(OpCodes.Callvirt, PropertySetter(typeof(Components.Prismatic), nameof(Components.Prismatic.OriginCloud))),

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

            Vector3 targetPos = hitInfo.point + Vector3.up * HauntedCandyRainbow.CloudHeight;
            GameObject cloud = PrefabHelper.Spawn(PrefabType.PrismaticCloud, targetPos, Quaternion.identity);

            if (!cloud.TryGetComponent<PrismaticCloud>(out PrismaticCloud prismaticCluod))
                return false;

            prismaticCluod.SynchronizedPosition = new RelativePosition(targetPos);
            NetworkServer.Spawn(cloud);

            player.AddEffect<Components.Prismatic>().OriginCloud = cloud.GetComponent<PrismaticCloud>();
            return false;
        }
    }
}
