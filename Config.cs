using PlayerRoles;
using System.ComponentModel;
using Exiled.API.Interfaces;
using System.Collections.Generic;
using InventorySystem.Items.Usables.Scp330;


namespace CandyChances
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;


        [Description("Global Scp330 Bowl settings.")]
        public bool OverrideCandyTakeCooldown { get; set; }
        public float ModifiedCandyTakeCooldown { get; set; } = 2;

        public bool OverrideGlobalUseLimit { get; set; }
        public int ModifiedGlobalUseLimit { get; set; } = 3;


        [Description("Modified candy spawn chances in SCP-330 bowl. (Note: Default candy chance weights is 1, the values ​​of all candies are added together and a random value is selected)")]
        public Dictionary<CandyKindID, float> CandyChances { get; set; } = new()
        {
            {CandyKindID.Blue,   15f},
            {CandyKindID.Green,  15f},
            {CandyKindID.Pink,   10f},
            {CandyKindID.Purple, 15f},
            {CandyKindID.Rainbow,15f},
            {CandyKindID.Red,    15f},
            {CandyKindID.Yellow, 15f},
        };


        [Description("Hint duration and visibility options.")]
        public float HintTime { get; set; } = 3;
        public bool ShowCandyHint { get; set; } = true;
        public bool ShowRemainingUseHint { get; set; } = true;
        public bool ShowHandsSeveredHint { get; set; } = true;
        public bool ShouldPlayTakeSound { get; set; } = true;


        [Description("Role based SCP-330 Bowl use limits.")]
        public bool OverrideUseLimitsforRoles { get; set; }
        public Dictionary<RoleTypeId, int> ModifiedUseLimits { get; set; } = new()
        {
            { RoleTypeId.Tutorial, 99 },
        };

        public bool OverrideUseLimitsforCustomRoles { get; set; }
        public Dictionary<string, int> ModifiedUseLimitsforCustomRoles { get; set; } = new()
        {
            { "Example Role Name", 5 },
        };
    }
}
