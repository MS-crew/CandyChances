using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Interfaces;

using InventorySystem.Items.Usables.Scp330;

using MapGeneration.Holidays;

using PlayerRoles;

namespace CandyChances
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;


        [Description("Global Scp330 Bowl settings.")]
        public bool OverrideCandyTakeCooldown { get; set; } = false;
        public float ModifiedCandyTakeCooldown { get; set; } = 2;

        public bool OverrideGlobalUseLimit { get; set; } = false;
        public int ModifiedGlobalUseLimit { get; set; } = 3;


        [Description("Modified candy types in SCP-330 bowl.")]
        public bool OverrideBowlCandys { get; set; } = false;

        [Description("List of candy types that will appear in SCP-330 bowl.(All Types  )")]
        public List<string> ModifiedBowlCandys { get; set; } = new()
        {
            "CandyBlue",
            "CandyPink",
            "CandyYellow",
            "HauntedCandyBlack",
            "HauntedCandyBrown",
            "HauntedCandyGray",
            "HauntedCandyGreen",
            "HauntedCandyOrange",
            "HauntedCandyPurple",
            "HauntedCandyRainbow",
            "HauntedCandyRed",
            "HauntedCandyWhite"
        };

        [Description("Modifie candy spawn chances.")]
        public bool OverrideCandyChances { get; set; } = false;

        [Description("Modified candy spawn chances in SCP-330 bowl.")]
        public Dictionary<string, float> CandyChances { get; set; } = new()
        {
            {"CandyBlue",             1},
            {"CandyGreen",            1},
            {"CandyPink",          0.2f},
            {"CandyPurple",           1},
            {"CandyRainbow",          1},
            {"CandyRed",              1},
            {"CandyYellow",           1},
            {"HauntedCandyBlack",     1},
            {"HauntedCandyWhite",     1},
            {"HauntedCandyBrown",     1},
            {"HauntedCandyGray",      1},
            {"HauntedCandyGreen",     1},
            {"HauntedCandyOrange",    1},
            {"HauntedCandyPurple",    1},
            {"HauntedCandyRainbow",   1},
            {"HauntedCandyRed",       1},
        };


        [Description("Hint duration and visibility options.")]
        public float HintTime { get; set; } = 3;
        public float HintPositionRuei { get; set; } = 300;
        public bool ShowCandyHint { get; set; } = true;
        public bool ShowRemainingUseHint { get; set; } = true;
        public bool ShowHandsSeveredHint { get; set; } = true;
        public bool ShouldPlayTakeSound { get; set; } = true;


        [Description("Role based SCP-330 Bowl use limits.")]
        public bool OverrideUseLimitsforRoles { get; set; }
        public Dictionary<RoleTypeId, int> ModifiedUseLimits { get; set; } = new()
        {
            { RoleTypeId.Filmmaker, 99 },
        };

        public bool OverrideUseLimitsforCustomRoles { get; set; }
        public Dictionary<string, int> ModifiedUseLimitsforCustomRoles { get; set; } = new()
        {
            { "Example Role Name", 5 },
        };


        [Description("Hallowen Candys improvement settings.")]
        public OrangeCandyImprove OrangeCandySettings { get; set; } = new OrangeCandyImprove();

        public GrayCandyImprove GrayCandySettings { get; set; } = new GrayCandyImprove();

        public WhiteCandyImprove WhiteCandySettings { get; set; } = new WhiteCandyImprove();

        [Description("List of candy types names.")]
        public string[] CandyNames { get; set; } = [.. typeof(ICandy).Assembly.GetTypes().Where(t => typeof(ICandy).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract).Select(t => t.Name)];

        public class OrangeCandyImprove
        {
            public bool AddLight { get; set; } = true;
            public float Range { get; set; } = 20f;
            public float MaxInsentity { get; set; } = 30000f;


            public bool AddEffects { get; set; } = true;
            public List<Effect> Effects { get; set; } =
            [
                new Effect(EffectType.Slowness, intensity:50, duration: 30f),
            ];
        }

        public class GrayCandyImprove
        {
            public bool AddEffects { get; set; } = true;

            public List<Effect> Effects { get; set; } =
            [
                new Effect(EffectType.Slowness, intensity:50, duration: 30f),
            ];
        }

        public class WhiteCandyImprove
        {
            public bool AddEffects { get; set; } = true;

            public List<Effect> Effects { get; set; } =
            [
                new Effect(EffectType.Ghostly, duration: 25f),
                new Effect(EffectType.SilentWalk, duration: 25f),
                new Effect(EffectType.Fade, intensity: 240, duration: 25f),
            ];
        }
    }
}
