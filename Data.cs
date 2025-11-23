using System;
using System.Collections.Generic;

using CandyChances.Components;

using Exiled.API.Enums;
using Exiled.API.Features;

using InventorySystem.Items.Usables.Scp330;

namespace CandyChances
{
    internal class Data
    {
        private const int HauntedCandySlownessInsentity = 10;

        internal static IReadOnlyDictionary<string, Type> CandyNametoTypes { get; } = CandyTypeLookup();

        private static Dictionary<string, Type> CandyTypeLookup()
        {
            Type iCandyType = typeof(ICandy);
            Type[] candyTypes = iCandyType.Assembly.GetTypes();

            Dictionary<string, Type> dict = new(21, StringComparer.Ordinal);

            for (int i = 0; i < candyTypes.Length; i++)
            {
                Type t = candyTypes[i];
                if (t?.Name != null && t.IsClass && iCandyType.IsAssignableFrom(t) && !dict.ContainsKey(t.Name))
                {
                    dict[t.Name] = t;
                    Log.Debug($"Chached candy name '{t.Name}' to type '{t.FullName}'");
                }
            }

            return dict;
        }

        internal static IReadOnlyDictionary<Type, Action<Player>> CandyEffects { get; } = new Dictionary<Type, Action<Player>>(7)
        {  
            { typeof(HauntedCandyRed),  p => p.AddEffect<Spicy>() },
            { typeof(HauntedCandyGray),  p => p.AddEffect<Metal>() },
            { typeof(HauntedCandyWhite), p => p.AddEffect<White>() },
            { typeof(HauntedCandyGreen), p => p.AddEffect<SugarHigh>() },
            { typeof(HauntedCandyYellow),p => p.AddEffect<SugarRush>() },
            { typeof(HauntedCandyOrange),p => p.AddEffect<OrangeCandy>() },
            { typeof(HauntedCandyPurple),p => p.EnableEffect(EffectType.Slowness, HauntedCandyPurple.EffectDuration, HauntedCandySlownessInsentity) },
        };
    }
}
