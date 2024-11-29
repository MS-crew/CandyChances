using Exiled.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandyChances
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        public float BlueCandyChance { get; set; } = 15f;
        public float GreenCandyChange { get; set; } = 15f;
        public float PinkCandyChance { get; set; } = 10f;
        public float PurpleCandyChance { get; set; } = 15f;
        public float RainbowCandyChance { get; set; } = 15f;
        public float RedCandyChance { get; set; } = 15f;
        public float YellowCandyChance { get; set; } = 15f;
    }
}
