using HarmonyLib;
using InventorySystem.Items.Usables.Scp330;

namespace CandyChances
{
    [HarmonyPatch(typeof(CandyBlue), "get_SpawnChanceWeight")]
    public static class CandyBluePatch
    {
        public static void Postfix(ref float __result)
        {
            __result = Plugin.Instance.Config.BlueCandyChance;
        }
    }

    [HarmonyPatch(typeof(CandyGreen), "get_SpawnChanceWeight")]
    public static class CandyGreemPatch
    {
        public static void Postfix(ref float __result)
        {
            __result = Plugin.Instance.Config.GreenCandyChange;
        }
    }

    [HarmonyPatch(typeof(CandyPink), "get_SpawnChanceWeight")]
    public static class CandyPinkPatch
    {
        public static void Postfix(ref float __result)
        {
            __result = Plugin.Instance.Config.PinkCandyChance;
        }
    }

    [HarmonyPatch(typeof(CandyPurple), "get_SpawnChanceWeight")]
    public static class CandyPurplePatch
    {
        public static void Postfix(ref float __result)
        {
            __result = Plugin.Instance.Config.PurpleCandyChance;
        }
    }

    [HarmonyPatch(typeof(CandyRainbow), "get_SpawnChanceWeight")]
    public static class CandyRainbowPatch
    {
        public static void Postfix(ref float __result)
        {
            __result = Plugin.Instance.Config.RainbowCandyChance;
        }
    }

    [HarmonyPatch(typeof(CandyRed), "get_SpawnChanceWeight")]
    public static class CandyRedPatch
    {
        public static void Postfix(ref float __result)
        {
            __result = Plugin.Instance.Config.RedCandyChance;
        }
    }

    [HarmonyPatch(typeof(CandyYellow), "get_SpawnChanceWeight")]
    public static class CandyYellowPumper
    {
        public static void Postfix(ref float __result)
        {
            __result = Plugin.Instance.Config.YellowCandyChance;
        }
    }
}
