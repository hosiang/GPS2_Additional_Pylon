using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global {

    //Managers
    public static GameManager gameManager;
    public static AudioManager audioManager;
    public static UIActiveManager userInterfaceActiveManager;

    //Save location
    public static string saveFile_HighScore = "/HighScore.dat";

    //SFX
    public static string sfx_Test = "";

    //BGM
    public static string bgm_Test = "";

    //Animation
    public static string animator_Trigger_SpecialAsteroid_isDestroyed = "isDestroyed";
    public static string animator_Trigger_SpecialAsteroid_isKnockBack = "isKnockBack";

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
    public static readonly string[] nameGameObject_Menus = { "ScreenBlock", "UIControllerPrefab", "UIInformationContainer", "QuitConfirmation", "TaskCompletedContainer", "Hurt Effect" };

    //Enums
    public enum AstroidType { AsteroidSmall, AsteroidBig, Special };
    public enum OresTypes { Special_Ore, Ore_No1, Length };
    public enum ZoneLevels { ShieldZone, EasyZone, MediumZone, HardZone, Length };
    public enum ParticleEffectType { astroid };
    public enum MenusType {ScreenBlock, UIControllerPrefab, UIInformationContainer, QuitConfirmation, TaskCompletedContainer, Hurt_Effect, Length };
    public enum QuestLevels { Quest_01, Quest_02, Quest_03, Length };
    public enum GameSceneIndex { MainMenu, Level_01 };
    public enum OresSpawn { Minimal, Maximal, Skill_Broken };
    public enum SkillsTree { DoubleThrust, Length };

    //List or Array
    public static readonly float[] zonesRadius = { 20.0f, 40.0f, 80.0f, 160.0f };
    public static readonly float[] OresWeight = { 0.0f, 2.5f };
    public static readonly float[] eachAsteroidHealthPoint = { 20.0f, 40.0f, 40.0f };

    //Mutil List or Mutil Dimension Array
    public static readonly int[,,] eachZoneAsteroidSpwanOreAmount = new int[3,3,3] { // 1_D = ZoneLevels, 2_D = AstroidType, 3_D = OresSpawn
                                                                            {
                                                                                {0, 2, 0}, //SmallAsteroid
                                                                                {2, 4, 1}, //BigAsteroid
                                                                                {1, 1, 1} //SpecialAsteroid
                                                                            }, //EasyZone:
                                                                            {
                                                                                {1, 4, 1}, //SmallAsteroid
                                                                                {3, 6, 2}, //BigAsteroid
                                                                                {1, 1, 1} //SpecialAsteroid
                                                                            }, //MediumZone:
                                                                            {
                                                                                {2, 6, 1}, //SmallAsteroid
                                                                                {4, 8, 3}, //BigAsteroid
                                                                                {1, 1, 1}  //SpecialAsteroid
                                                                            } //HardZone:
                                                                        };

    // Quest
    public static float[] targetQuest_OreNo1_Amount = { 1, 100, 150 };
    public static float targetOreToWinValue = 1.0f;

    public static float ValueToTime(float value)
    {
        return (60.0f / 100.0f) * value;
    }

    public static float TimeToValue(float time)
    {
        return (100.0f / 60.0f) * time;
    }

}
