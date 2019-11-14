using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global {

    //Managers
    public static GameManager gameManager;
    public static AudioManager audioManager;
    public static UIActiveManager userInterfaceActiveManager;

    //Save location
    public static string saveFile_Test = "";

    //SFX
    public static string sfx_Test = "";

    //BGM
    public static string bgm_Test = "";

    //Animation
    public static string animator_Test = "";

    //Layers
    public static int layer_Player = 1 << LayerMask.NameToLayer("Player");
    public static int layer_Astroid = 1 << LayerMask.NameToLayer("Asteroid");
    public static int layer_Enemy = 1 << LayerMask.NameToLayer("Enemy");
    public static int layer_Ore = 1 << LayerMask.NameToLayer("Ore");
    //Tags
    public static string tag_Player = "Player";
    public static string tag_Astroid = "Asteroid";

    //Controls
    public static string controls_Test = "";

    //Menus
    public static readonly string nameGameObject_UI = "UI";
    public static readonly string[] nameGameObject_Menus = { "ScreenBlock", "UIControllerPrefab", "UIInformationContainer", "QuitConfirmation" };

    //Enums
    public enum AstroidType { AsteroidSmall, AsteroidBig, Special, Ore };
    public enum OresTypes { Special_Ore, Ore_No1, Length };
    public enum ZoneLevels { ShieldZone, EasyZone, MediumZone, HardZone, Length };
    public enum ParticleEffectType { astroid };
    public enum MenusType {ScreenBlock, UIControllerPrefab, UIInformationContainer, QuitConfirmation};

    //List or Array
    public static readonly float[] zoneValues = { 15.0f, 30.0f, 50.0f, 75.0f };
    public static readonly float[] OresWeight = { 0.0f, 2.5f };

    // Quest
    public static float targetOreToWinValue = 125.0f;

    public static float ValueToTime(float value)
    {
        return (60.0f / 100.0f) * value;
    }

    public static float TimeToValue(float time)
    {
        return (100.0f / 60.0f) * time;
    }

}
