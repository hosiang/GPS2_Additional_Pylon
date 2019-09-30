using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global {

    //Layers
    public static int playerLayer = 1 << LayerMask.NameToLayer("Player");

    public static string tagPlayer = "Player";

    public enum OresTypes { Iron, Length };

}
