#if HALLOWEN
using HarmonyLib;
using InventorySystem.Items.Usables.Scp330;

namespace CandyChances.Patchs
{
    [HarmonyPatch(typeof(HauntedCandyBlack), nameof(HauntedCandyBlack.SpawnChanceWeight), MethodType.Getter)]
    public static class HauntedCandyBlackPatch
    {
        public static void Postfix(ref float __result)
        {
            if (Plugin.Instance.Config.HallowenCandyChances.TryGetValue(CandyKindID.Black, out float chance))
                __result = chance;
        }
    }

    [HarmonyPatch(typeof(HauntedCandyBlue), nameof(HauntedCandyBlue.SpawnChanceWeight), MethodType.Getter)]
    public static class HauntedCandyBluePatch
    {
        public static void Postfix(ref float __result)
        {
            if (Plugin.Instance.Config.HallowenCandyChances.TryGetValue(CandyKindID.Blue, out float chance))
                __result = chance;
        }
    }

    [HarmonyPatch(typeof(HauntedCandyBrown), nameof(HauntedCandyBrown.SpawnChanceWeight), MethodType.Getter)]
    public static class HauntedCandyBrownPatch
    {
        public static void Postfix(ref float __result)
        {
            if (Plugin.Instance.Config.HallowenCandyChances.TryGetValue(CandyKindID.Brown, out float chance))
                __result = chance;
        }
    }

    [HarmonyPatch(typeof(HauntedCandyEvil), nameof(HauntedCandyEvil.SpawnChanceWeight), MethodType.Getter)]
    public static class HauntedCandyEvilPatch
    {
        public static void Postfix(ref float __result)
        {
            if (Plugin.Instance.Config.HallowenCandyChances.TryGetValue(CandyKindID.Evil, out float chance))
                __result = chance;
        }
    }

    [HarmonyPatch(typeof(HauntedCandyGray), nameof(HauntedCandyGray.SpawnChanceWeight), MethodType.Getter)]
    public static class HauntedCandyGrayPatch
    {
        public static void Postfix(ref float __result)
        {
            if (Plugin.Instance.Config.HallowenCandyChances.TryGetValue(CandyKindID.Gray, out float chance))
                __result = chance;
        }
    }

    [HarmonyPatch(typeof(HauntedCandyGreen), nameof(HauntedCandyGreen.SpawnChanceWeight), MethodType.Getter)]
    public static class HauntedCandyGreenPatch
    {
        public static void Postfix(ref float __result)
        {
            if (Plugin.Instance.Config.HallowenCandyChances.TryGetValue(CandyKindID.Green, out float chance))
                __result = chance;
        }
    }

    [HarmonyPatch(typeof(HauntedCandyOrange), nameof(HauntedCandyOrange.SpawnChanceWeight), MethodType.Getter)]
    public static class HauntedCandyOrangePatch
    {
        public static void Postfix(ref float __result)
        {
            if (Plugin.Instance.Config.HallowenCandyChances.TryGetValue(CandyKindID.Orange, out float chance))
                __result = chance;
        }
    }

    [HarmonyPatch(typeof(HauntedCandyPink), nameof(HauntedCandyPink.SpawnChanceWeight), MethodType.Getter)]
    public static class HauntedCandyPinkPatch
    {
        public static void Postfix(ref float __result)
        {
            if (Plugin.Instance.Config.HallowenCandyChances.TryGetValue(CandyKindID.Pink, out float chance))
                __result = chance;
        }
    }

    [HarmonyPatch(typeof(HauntedCandyPurple), nameof(HauntedCandyPurple.SpawnChanceWeight), MethodType.Getter)]
    public static class HauntedCandyPurplePatch
    {
        public static void Postfix(ref float __result)
        {
            if (Plugin.Instance.Config.HallowenCandyChances.TryGetValue(CandyKindID.Purple, out float chance))
                __result = chance;
        }
    }

    [HarmonyPatch(typeof(HauntedCandyRainbow), nameof(HauntedCandyRainbow.SpawnChanceWeight), MethodType.Getter)]
    public static class HauntedCandyRainbowPatch
    {
        public static void Postfix(ref float __result)
        {
            if (Plugin.Instance.Config.HallowenCandyChances.TryGetValue(CandyKindID.Rainbow, out float chance))
                __result = chance;
        }
    }

    [HarmonyPatch(typeof(HauntedCandyRed), nameof(HauntedCandyRed.SpawnChanceWeight), MethodType.Getter)]
    public static class HauntedCandyRedPatch
    {
        public static void Postfix(ref float __result)
        {
            if (Plugin.Instance.Config.HallowenCandyChances.TryGetValue(CandyKindID.Red, out float chance))
                __result = chance;
        }
    }

    [HarmonyPatch(typeof(HauntedCandyWhite), nameof(HauntedCandyWhite.SpawnChanceWeight), MethodType.Getter)]
    public static class HauntedCandyWhitePatch
    {
        public static void Postfix(ref float __result)
        {
            if (Plugin.Instance.Config.HallowenCandyChances.TryGetValue(CandyKindID.White, out float chance))
                __result = chance;
        }
    }

    [HarmonyPatch(typeof(HauntedCandyYellow), nameof(HauntedCandyYellow.SpawnChanceWeight), MethodType.Getter)]
    public static class HauntedCandyYellowPatch
    {
        public static void Postfix(ref float __result)
        {
            if (Plugin.Instance.Config.HallowenCandyChances.TryGetValue(CandyKindID.Yellow, out float chance))
                __result = chance;
        }
    }
}
#endif