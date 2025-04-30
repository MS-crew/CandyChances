using System.ComponentModel;
using Exiled.API.Interfaces;
using System.Collections.Generic;
using InventorySystem.Items.Usables.Scp330;

namespace CandyChances
{
    public class Translation : ITranslation
    {
        [Description("Random hint messages shown when player's hands get severed")]
        public string[] HandsSeveredHints { get; set; } =
        {
            "<color=red>⚠ CRITICAL INJURY: Hands severed!</color>",
            "<color=red>Medical Alert: You need prosthetics!</color>",
            "<color=red>Whoops! There go your pizza-eating days!</color>",
            "<color=red>Pro tip: Don't play with cursed objects!</color>",
            "<color=red>Your hand privilege has been revoked!</color>",
            "<color=red>Luke... I am your... oh wait wrong reference</color>",
            "<color=red>Edward Scissorhands called - wants his look back</color>",
            "<color=#FF4500>Psst... I've got candy in my van!</color>",
            "<color=red>Don't scream, it's just a prank bro!</color>",
            "<color=red>+10 points for dramatic injury!</color>",
            "<color=red>FREE HUGS! (Disclaimer: I can't actually hug back)</color>",
            "<color=red>Achievement Unlocked: Handless Wonder</color>",
            "<color=red>Your hands were tasty. Thanks for the snack!</color>",
            "<color=red>Your insurance doesn't cover this</color>"
        };

        [Description("Random hints shown when taking candy from SCP-330")]
        public Dictionary<CandyKindID, string[]> GetCandyHints { get; set; } = new()
        {
            [CandyKindID.Red] =
            [
                "<color=red>Your wounds magically vanish! (Who needs doctors?)</color>",
                "<color=red>Tastes like liquid bandages!</color>",
                "<color=red>+10 HP! Your mom would be proud</color>",
                "<color=red>Your body: 1 - Death: 0</color>",
                "<color=red>Warning: May cause immortality cravings</color>"
            ],

            [CandyKindID.Yellow] =
            [
                "<color=yellow>Your legs now powered by NASCAR!</color>",
                "<color=yellow>Zoomies activated! *vrrrrooom*</color>",
                "<color=yellow>Stamina? You've got INFINITE!</color>",
                "<color=yellow>Warning: May cause sonic booms</color>",
                "<color=yellow>You could outrun your regrets now!</color>"
            ],

            [CandyKindID.Green] =
            [
                "<color=green>You're now 10% more unkillable!</color>",
                "<color=green>Congrats! You've achieved Basic Bitch Armor™</color>",
                "<color=green>Your stamina bar called - it's horny</color>",
                "<color=green>Warning: May cause delusions of tankiness</color>",
                "<color=green>You feel... adequately protected?</color>"
            ],

            [CandyKindID.Blue] =
            [
                "<color=blue>You've become the Nokia 3310 of humans</color>",
                "<color=blue>Plot armor activated!</color>",
                "<color=blue>Warning: You might start bullet-dodging</color>",
                "<color=blue>You: 1 - Physics: 0</color>",
                "<color=blue>Your new superpower: Existing harder</color>"
            ],

            [CandyKindID.Pink] =
    
            [
                "<color=#FF69B4>3... 2... 1... YOU go boom!</color>",
                "<color=#FF69B4>Making an explosive entrance!</color>",
                "<color=#FF69B4>Pro tip: Hug someone quickly!</color>",
                "<color=#FF69B4>You are become death, destroyer of social circles</color>",
                "<color=#FF69B4>Warning: Not a team-building exercise</color>"
            ],

            [CandyKindID.Rainbow] =
            [
                "<color=red>L</color><color=orange>I</color><color=yellow>F</color><color=green>E</color> <color=blue>H</color><color=purple>A</color><color=#FF69B4>X</color>",
                "<color=white>You feel... adequately OP!</color>",
                "<color=#FF0000>M</color><color=#00FF00>O</color><color=#0000FF>D</color><color=#FF00FF>E</color><color=#FFFF00>:</color> <color=white>Buffed</color>",
                "<color=white>50 ways to slightly improve your day</color>",
                "<color=#9400D3>You're winning! (At what? Don't ask)</color>"
            ],

            [CandyKindID.Purple] =
            [
                "<color=#800080>Damage sponge mode: ACTIVATED</color>",
                "<color=#800080>You're basically Wolverine's cousin now</color>",
                "<color=#800080>Warning: May cause tank envy</color>",
                "<color=#800080>Your new motto: 'Hit me harder, daddy!'</color>",
                "<color=#800080>You've achieved C H O N K status</color>"
            ]
        };
    }
}
