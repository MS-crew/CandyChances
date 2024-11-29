using Exiled.API.Interfaces;
using InventorySystem.Items.Usables.Scp330;
using System.Collections.Generic;

namespace CandyChances
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        public Dictionary<CandyKindID, float> CandyChances { get; set; } = new()
        {
            {CandyKindID.Blue,15f},
            {CandyKindID.Green,15f},
            {CandyKindID.Pink,10f},
            {CandyKindID.Purple,15f},
            {CandyKindID.Rainbow,15f},
            {CandyKindID.Red,15f},
            {CandyKindID.Yellow,15f},
        };
    }
}
