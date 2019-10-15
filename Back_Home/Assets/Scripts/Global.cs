using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global {

    //Managers
    public static AudioManager audioManager;

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

    //Tags
    public static string tag_Player = "Player";

    //Controls
    public static string controls_Test = "";

    //Enums
    public enum AstroidType { small, big, special };
    public enum OresTypes { Iron, no2_Ores, Length };
    public enum ZoneLevels { ShieldZone, EasyZone, MediumZone, HardZone };

    //List or Array
    public static readonly float[] zoneValues = { 10.0f, 20.0f, 40.0f, 60.0f };

}
