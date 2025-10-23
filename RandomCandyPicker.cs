using UnityEngine;
using CandyChances.Patchs;
using InventorySystem.Items.Usables.Scp330;

namespace CandyChances
{
    public static class RandomCandyPicker
    {
        public static void ApplyRandomEffect(ReferenceHub hub)
        {
            int random = Random.Range(0, 7);

            switch (random)
            {
                case 0: ReversePatches.Yellow(new CandyYellow(), hub); 
                    break;

                case 1: ReversePatches.Blue(new CandyBlue(), hub); 
                    break;

                case 2: ReversePatches.Green(new CandyGreen(), hub); 
                    break;

                case 3: ReversePatches.Red(new CandyRed(), hub); 
                    break;

                case 4: ReversePatches.Purple(new CandyPurple(), hub); 
                    break;

                case 5: ReversePatches.Pink(new CandyPink(), hub); 
                    break;

                case 6: ReversePatches.Rainbow(new CandyRainbow(), hub); 
                    break;
            }
        }
    }
}
