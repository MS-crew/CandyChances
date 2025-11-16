using System;
using System.Collections.Generic;
using System.Linq;
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

        public static IEnumerable<MethodBase> TargetMethods()
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

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, MethodBase orginal)
        {
            string candyName = orginal.DeclaringType.Name;

            if (!Plugin.Instance.Config.CandyChances.TryGetValue(candyName, out float chance))
                return instructions;
                var methodBase = firstInstruction.operand as MethodBase;
                className = methodBase?.DeclaringType?.Name;
            }


            if (className == null || !Plugin.Instance.Config.CandyChances.TryGetValue(className, out float chance))
                chance = 1f;

            return new[]
            {
                new CodeInstruction(OpCodes.Ldc_R4, chance),
                new CodeInstruction(OpCodes.Ret)
            };
        }
    }
}
