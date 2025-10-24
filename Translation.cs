﻿using System.ComponentModel;
using Exiled.API.Interfaces;
using System.Collections.Generic;
using InventorySystem.Items.Usables.Scp330;

namespace CandyChances
{
    public class Translation : ITranslation
    {
        [Description("Message showing remaining uses of SCP-330. {0} is remaining count")]
        public string RemainingUse { get; set; } = "<color=green>Remaining uses: {0}</color>";

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

        [Description("Random hints shown when taking candy from SCP-330.")]
        public Dictionary<CandyKindID, string[]> CandyHints { get; set; } = new()
        {
            [CandyKindID.Red] =
            [
                "<color=red>You got Red Candy</color>",
                "<color=red>Your wounds magically vanish! (Who needs doctors?)</color>",
                "<color=red>Tastes like liquid bandages!</color>",
                "<color=red>+10 HP! Your mom would be proud</color>",
                "<color=red>Your body: 1 - Death: 0</color>",
                "<color=red>Warning: May cause immortality cravings</color>"
            ],

            [CandyKindID.Yellow] =
            [
                "<color=yellow>You got Yellow Candy</color>",
                "<color=yellow>Your legs now powered by NASCAR!</color>",
                "<color=yellow>Zoomies activated! *vrrrrooom*</color>",
                "<color=yellow>Stamina? You've got INFINITE!</color>",
                "<color=yellow>Warning: May cause sonic booms</color>",
                "<color=yellow>You could outrun your regrets now!</color>"
            ],

            [CandyKindID.Green] =
            [
                "<color=green>You got Green Candy</color>",
                "<color=green>You're now 10% more unkillable!</color>",
                "<color=green>Congrats! You've achieved Basic Bitch Armor™</color>",
                "<color=green>Your stamina bar called - it's horny</color>",
                "<color=green>Warning: May cause delusions of tankiness</color>",
                "<color=green>You feel... adequately protected?</color>"
            ],

            [CandyKindID.Blue] =
            [
                "<color=blue>You got Blue Candy</color>",
                "<color=blue>You've become the Nokia 3310 of humans</color>",
                "<color=blue>Plot armor activated!</color>",
                "<color=blue>Warning: You might start bullet-dodging</color>",
                "<color=blue>You: 1 - Physics: 0</color>",
                "<color=blue>Your new superpower: Existing harder</color>"
            ],

            [CandyKindID.Pink] =
            [
                "<color=#FF69B4>You got Pink Candy</color>",
                "<color=#FF69B4>3... 2... 1... YOU go boom!</color>",
                "<color=#FF69B4>Making an explosive entrance!</color>",
                "<color=#FF69B4>Pro tip: Hug someone quickly!</color>",
                "<color=#FF69B4>You are become death, destroyer of social circles</color>",
                "<color=#FF69B4>Warning: Not a team-building exercise</color>"
            ],

            [CandyKindID.Rainbow] =
            [
                "<color=#9400D3>You got Rainbow Candy</color>",
                "<color=red>L</color><color=orange>I</color><color=yellow>F</color><color=green>E</color> <color=blue>H</color><color=purple>A</color><color=#FF69B4>X</color>",
                "<color=white>You feel... adequately OP!</color>",
                "<color=#FF0000>M</color><color=#00FF00>O</color><color=#0000FF>D</color><color=#FF00FF>E</color><color=#FFFF00>:</color> <color=white>Buffed</color>",
                "<color=white>50 ways to slightly improve your day</color>",
                "<color=#9400D3>You're winning! (At what? Don't ask)</color>"
            ],

            [CandyKindID.Purple] =
            [
                "<color=#800080>You got Purple Candy</color>",
                "<color=#800080>Damage sponge mode: ACTIVATED</color>",
                "<color=#800080>You're basically Wolverine's cousin now</color>",
                "<color=#800080>Warning: May cause tank envy</color>",
                "<color=#800080>Your new motto: 'Hit me harder, daddy!'</color>",
                "<color=#800080>You've achieved C H O N K status</color>"
            ]
        };

        [Description("Random hints shown when taking HALLOWEEN candies from SCP-330.")]
        public Dictionary<CandyKindID, string[]> HallowenCandyHints { get; set; } = new()
        {
            [CandyKindID.Black] =
            [
                "<color=black>You got Black Candy</color>",
                "<color=gray>The void gazes back at you...</color>",
                "<color=#222>Darkness creeps into your veins.</color>",
                "<color=#111>Your vision fades. Your senses twist.</color>",
                "<color=#000>You feel… nothing.</color>",
                "<color=gray>You hear whispers from beyond.</color>"
            ],

            [CandyKindID.Brown] =
            [
                "<color=#8B4513>You got Brown Candy</color>",
                "<color=#A0522D>Tastes like pure dirt and regret.</color>",
                "<color=#A0522D>Your stomach gurgles ominously.</color>",
                "<color=#8B4513>You feel heavier… like your insides turned to mud.</color>",
                "<color=#8B4513>Not chocolate. Definitely not chocolate.</color>"
            ],

            [CandyKindID.Evil] =
            [
                "<color=#8B0000>You got Evil Candy</color>",
                "<color=red>Your body convulses with unholy energy!</color>",
                "<color=red>You feel cursed… but also powerful.</color>",
                "<color=#800000>Your reflection smiles back when you don't.</color>",
                "<color=red>The candy whispers: ‘Join us.’</color>"
            ],

            [CandyKindID.Gray] =
            [
                "<color=gray>You got Gray Candy</color>",
                "<color=lightgray>You turn cold and metallic.</color>",
                "<color=#777>You feel like a living statue.</color>",
                "<color=#999>Movement… difficult…</color>",
                "<color=#555>Impact resistance increased. Emotions: none.</color>"
            ],

            [CandyKindID.Orange] =
            [
                "<color=orange>You got Orange Candy</color>",
                "<color=orange>You feel warm and slightly explosive.</color>",
                "<color=#FFA500>Your body emits sparks of flame!</color>",
                "<color=#FF8C00>You're turning into a walking candle.</color>",
                "<color=#FFA500>Warning: Highly flammable.</color>"
            ],

            [CandyKindID.White] =
            [
                "<color=white>You got White Candy</color>",
                "<color=#DDD>You become ghostly transparent!</color>",
                "<color=#EEE>You pass through doors like a spirit.</color>",
                "<color=#FFF>You feel detached from the mortal plane.</color>",
                "<color=#EEE>Your body fades in and out of reality.</color>"
            ],
        };

    }
}
