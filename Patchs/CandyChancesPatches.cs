using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

using Exiled.API.Features;

using HarmonyLib;

using InventorySystem.Items.Usables.Scp330;

namespace CandyChances.Patchs
{
    [HarmonyPatch]
    internal static class CandyChanceOverridePatch
    {
        private const string spawnChanceProperty = nameof(ICandy.SpawnChanceWeight);

        private static IEnumerable<MethodBase> TargetMethods()
        {
            foreach (string candyType in Plugin.Instance.Config.CandyChances.Keys)
            {
                Type type = candyType.ToCandyType();
                if (type == null)
                {
                    Log.Error($"[Candy] Class not found: {candyType}");
                    continue;
                }

                MethodInfo getter = AccessTools.PropertyGetter(type, spawnChanceProperty);
                if (getter != null)
                {
                    Log.Debug($"[Candy] Overriding: {candyType}.SpawnChanceWeight");
                    yield return getter;
                }
            }
        }

        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, MethodBase orginal)
        {
            string candyName = orginal.DeclaringType.Name;

            if (!Plugin.Instance.Config.CandyChances.TryGetValue(candyName, out float chance))
                return instructions;

            return new CodeInstruction[]
            {
                new (OpCodes.Ldc_R4, chance),
                new (OpCodes.Ret)
            };
        }
    }
}
