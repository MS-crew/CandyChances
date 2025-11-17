using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using Exiled.API.Interfaces;

using InventorySystem.Items.Usables.Scp330;

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
            "CandyRed",
            "CandyYellow",
            "CandyRainbow",
            "HauntedCandyGray",
            "HauntedCandyWhite",
            "HauntedCandyBlack",
            "HauntedCandyBrown",
            "HauntedCandyGreen",
            "HauntedCandyOrange",
            "HauntedCandyPurple",
            "HauntedCandyRainbow",
        };

        [Description("Modifie candy spawn chances.")]
        public bool OverrideCandyChances { get; set; } = true;

        [Description("Modified candy spawn chances in SCP-330 bowl.")]
        public Dictionary<string, float> CandyChances { get; set; } = new()
        {
            {"CandyBlue",             1},
            {"CandyPink",          0.2f},
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
            {"HauntedCandyRainbow",    1},
        };


        [Description("Try replicate Hallowen candy behaviors.")]
        public bool TryReplicateHalloweenCandys { get; set; } = true;


        [Description("Hint duration and visibility options.")]
        public float HintTime { get; set; } = 3;
        public float HintPositionRuei { get; set; } = 300;
        public bool ShowCandyHint { get; set; } = true;
        public bool ShowRemainingUseHint { get; set; } = true;
        public bool ShowHandsSeveredHint { get; set; } = true;
        public bool ShouldPlayTakeSound { get; set; } = true;


        [Description("Role based SCP-330 Bowl use limits.")]
        public bool OverrideUseLimitsforRoles { get; set; } = false;
        public Dictionary<RoleTypeId, int> ModifiedUseLimits { get; set; } = new()
        {
            { RoleTypeId.Filmmaker, 99 },
        };

        public bool OverrideUseLimitsforCustomRoles { get; set; } = false;
        public Dictionary<string, int> ModifiedUseLimitsforCustomRoles { get; set; } = new()
        {
            { "Example Role Name", 5 },
        };


        [Description("List of candy types names.")]
        public string[] CandyNames { get; set; } = [.. typeof(ICandy).Assembly.GetTypes().Where(t => typeof(ICandy).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract).Select(t => t.Name)];
    }
}
