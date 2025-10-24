using HarmonyLib;
using InventorySystem.Items.Usables.Scp330;

namespace CandyChances
{
    [HarmonyPatch(typeof(CandyBlue), nameof(CandyBlue.SpawnChanceWeight), MethodType.Getter)]
    public static class CandyBluePatch
    {
        public static void Postfix(ref float __result)
        {
            if (Plugin.Instance.Config.CandyChances.TryGetValue(CandyKindID.Blue, out float chance))
                __result = chance;
        }
    }

    [HarmonyPatch(typeof(CandyGreen), nameof(CandyGreen.SpawnChanceWeight) , MethodType.Getter)]
    public static class CandyGreenPatch
    {
        public static void Postfix(ref float __result)
        {
            if (Plugin.Instance.Config.CandyChances.TryGetValue(CandyKindID.Green, out float chance))
                __result = chance;
        }
    }

    [HarmonyPatch(typeof(CandyPink), nameof(CandyPink.SpawnChanceWeight), MethodType.Getter)]
    public static class CandyPinkPatch
    {
        public static void Postfix(ref float __result)
        {
            if (Plugin.Instance.Config.CandyChances.TryGetValue(CandyKindID.Pink, out float chance))
                __result = chance;
        }
    }

    [HarmonyPatch(typeof(CandyPurple), nameof(CandyPurple.SpawnChanceWeight), MethodType.Getter)]
    public static class CandyPurplePatch
    {
        public static void Postfix(ref float __result)
        {
            if (Plugin.Instance.Config.CandyChances.TryGetValue(CandyKindID.Purple, out float chance))
                __result = chance;
        }
    }

    [HarmonyPatch(typeof(CandyRainbow), nameof(CandyRainbow.SpawnChanceWeight), MethodType.Getter)]
    public static class CandyRainbowPatch
    {
        public static void Postfix(ref float __result)
        {
            if (Plugin.Instance.Config.CandyChances.TryGetValue(CandyKindID.Rainbow, out float chance))
                __result = chance;
        }
    }

    [HarmonyPatch(typeof(CandyRed), nameof(CandyRed.SpawnChanceWeight), MethodType.Getter)]
    public static class CandyRedPatch
    {
        public static void Postfix(ref float __result)
        {
            if (Plugin.Instance.Config.CandyChances.TryGetValue(CandyKindID.Red, out float chance))
                __result = chance;
        }
    }

    [HarmonyPatch(typeof(CandyYellow), nameof(CandyYellow.SpawnChanceWeight), MethodType.Getter)]
    public static class CandyYellowPatch
    {
        public static void Postfix(ref float __result)
        {
            if (Plugin.Instance.Config.CandyChances.TryGetValue(CandyKindID.Yellow, out float chance))
                __result = chance;
        }
    }
}
