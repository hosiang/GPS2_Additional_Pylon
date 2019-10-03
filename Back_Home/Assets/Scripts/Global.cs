﻿using System.Collections;
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

    //Tags
    public static string tag_Player = "Player";

    //Controls
    public static string controls_Test = "";

    //Enums
    public enum OresTypes { Iron, Length };

}
