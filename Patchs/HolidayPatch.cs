using System.Collections.Generic;
using System.Reflection.Emit;

using HarmonyLib;

using InventorySystem;

using MapGeneration.Holidays;

using Mirror;

namespace CandyChances.Patchs
{
    [HarmonyPatch(typeof(HolidayUtils), nameof(HolidayUtils.GetActiveHoliday))]
    internal static class HolidayPatch
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> _)
        {
            return new[]
            {
                new (OpCodes.Call, AccessTools.PropertyGetter(typeof(Plugin), nameof(Plugin.Instance))),
                new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Plugin), nameof(Plugin.Config))),
                new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Config), nameof(Config.ModifiedServerHolidayMode))),
                new CodeInstruction(OpCodes.Ret)
            };
        }
    }
}
