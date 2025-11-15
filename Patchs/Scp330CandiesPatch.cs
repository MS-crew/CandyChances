using System;
using System.Collections.Generic;
using System.Reflection.Emit;

using Exiled.API.Features;

using HarmonyLib;

using InventorySystem.Items.Usables.Scp330;

namespace CandyChances.Patchs
{
    [HarmonyPatch(typeof(Scp330Candies), nameof(Scp330Candies.Candies), MethodType.Getter)]
    internal static class Scp330CandiesPatch
    {
        private static ICandy[] cachedCandies;

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> _)
        {
            cachedCandies = BuildCandyList();

            return new[]
            {
                new (OpCodes.Ldsfld, AccessTools.Field(typeof(Scp330CandiesPatch), nameof(Scp330CandiesPatch.cachedCandies))),
                new CodeInstruction(OpCodes.Ret)
            };
        }

        private static ICandy[] BuildCandyList()
        {
            List<ICandy> candies = new();

            foreach (string candyName in Plugin.Instance.Config.ModifiedBowlCandys)
            {
                Type candyType = candyName.ToCandyType();
                if (candyType == null)
                {
                    Log.Error($"Candy class not found: {candyName}");
                    continue;
                }

                if (Activator.CreateInstance(candyType) is ICandy candy)
                    candies.Add(candy);
            }

            return candies.ToArray();
        }

    }
}
