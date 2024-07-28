using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Game/BotDatasIns")]
public class BotDatasIns : ScriptableObject

{
    public static List<string> BotName = new List<string>
    {
        "FireFox7",
        "QuickShot",
        "BlueSky99",
        "Misty",
        "SilverFox",
        "ZeroHero",
        "SkyHigh",
        "LuckyMe",
        "DarkMode",
        "AceKing",
        "ZenMaster",
        "HyperJump",
        "WildChild",
        "CrazyCat",
        "IronHand",
        "TurboFly",
        "lovePizza",
        "SilentWolf",
        "SuperBee",
        "justGuess",
        "newName"
    };
    public static List<Material> BotMaterials = new List<Material>();
}
