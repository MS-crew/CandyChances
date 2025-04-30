using PlayerRoles;
using Exiled.API.Interfaces;
using System.Collections.Generic;
using InventorySystem.Items.Usables.Scp330;

namespace CandyChances
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        public bool ShouldPlayTakeSound { get; set; } = true;
        public bool HandsSeveredHint { get; set; } = true;
        public float HandsSeveredHintTime { get; set; } = 3;
        public float CandyHintTime { get; set; } = 3;
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
        public Dictionary<RoleTypeId, int> ModifiedUseLimits { get; set; } = new()
        {
            {RoleTypeId.ClassD,     3},
            {RoleTypeId.NtfCaptain, 5},
        };
        public Dictionary<string, int> ModifiedUseLimitsforCustomRoles { get; set; } = new()
        {
            {"Candy Monster", 99},
            {"Chaos Bomber",  50},
        };
    }
}
