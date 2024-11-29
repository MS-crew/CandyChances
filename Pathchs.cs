using HarmonyLib;
using InventorySystem.Items.Usables.Scp330;

namespace CandyChances
{
    [HarmonyPatch(typeof(CandyBlue), "get_SpawnChanceWeight")]
    public static class CandyBluePatch
    {
        public static void Postfix(ref float __result)
        {
            if (Plugin.Instance.Config.CandyChances.TryGetValue(CandyKindID.Blue, out float chance))
            {
                __result = chance;
            }
        }
    }

    [HarmonyPatch(typeof(CandyGreen), "get_SpawnChanceWeight")]
    public static class CandyGreemPatch
    {
        public static void Postfix(ref float __result)
        {
            if (Plugin.Instance.Config.CandyChances.TryGetValue(CandyKindID.Green, out float chance))
            {
                __result = chance;
            }
        }
    }

    [HarmonyPatch(typeof(CandyPink), "get_SpawnChanceWeight")]
    public static class CandyPinkPatch
    {
        public static void Postfix(ref float __result)
        {
            if (Plugin.Instance.Config.CandyChances.TryGetValue(CandyKindID.Pink, out float chance))
            {
                __result = chance;
            }
        }
    }

    [HarmonyPatch(typeof(CandyPurple), "get_SpawnChanceWeight")]
    public static class CandyPurplePatch
    {
        public static void Postfix(ref float __result)
        {
            if (Plugin.Instance.Config.CandyChances.TryGetValue(CandyKindID.Purple, out float chance))
            {
                __result = chance;
            }
        }
    }

    [HarmonyPatch(typeof(CandyRainbow), "get_SpawnChanceWeight")]
    public static class CandyRainbowPatch
    {
        public static void Postfix(ref float __result)
        {
            if (Plugin.Instance.Config.CandyChances.TryGetValue(CandyKindID.Rainbow, out float chance))
            {
                __result = chance;
            }
        }
    }

    [HarmonyPatch(typeof(CandyRed), "get_SpawnChanceWeight")]
    public static class CandyRedPatch
    {
        public static void Postfix(ref float __result)
        {
            if (Plugin.Instance.Config.CandyChances.TryGetValue(CandyKindID.Red, out float chance))
            {
                __result = chance;
            }
        }
    }

    [HarmonyPatch(typeof(CandyYellow), "get_SpawnChanceWeight")]
    public static class CandyYellowPumper
    {
        public static void Postfix(ref float __result)
        {
            if (Plugin.Instance.Config.CandyChances.TryGetValue(CandyKindID.Yellow, out float chance))
            {
                __result = chance;
            }
        }
    }
}
