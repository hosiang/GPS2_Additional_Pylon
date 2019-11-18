using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private EnemySpawner enemySpawner;
    //public GameObject taskCompletedUI;
    private BaseSystem baseSystem;
    private EndScoreUpdating endScoreUpdating;

    private float currentPermissiveZoneRadius;
    private int currentQuestLevel = 0;
    private bool isGameOver = false;

    // Getter
    public float CurrentPermissiveZoneRadius => currentPermissiveZoneRadius;
    public int CurrentQuestLevel => currentQuestLevel;
    public bool IsGameOver => isGameOver;

    private void Awake() {
        if (Global.gameManager == null) { Global.gameManager = this; }
        else { Destroy(gameObject); return; }

        enemySpawner = transform.Find("Enemy Spawner").GetComponent<EnemySpawner>();

        currentPermissiveZoneRadius = Global.zonesRadius[(int)Global.ZoneLevels.EasyZone];
        Screen.SetResolution((int)(Screen.width * 0.5f), (int)(Screen.height * 0.5f), fullscreen:true);
    }

    // Start is called before the first frame update
    void Start() {

        enemySpawner.generate();

        baseSystem = FindObjectOfType<BaseSystem>();
        endScoreUpdating = FindObjectOfType<EndScoreUpdating>();

    }

    // Update is called once per frame
    void Update() {

        if(!isGameOver && baseSystem.GetBaseStorageOres(Global.OresTypes.Ore_No1) >= Global.targetQuest_OreNo1_Amount[currentQuestLevel]) // For update the quest level
        {
            if (currentQuestLevel < (int)Global.QuestLevels.Quest_03)
            {
                baseSystem.GetOutSomeStorageOresForQuestSystem(this, Global.OresTypes.Ore_No1, (Global.QuestLevels)currentQuestLevel);
                currentQuestLevel++;
                currentPermissiveZoneRadius = Global.zonesRadius[(int)Global.ZoneLevels.EasyZone + currentQuestLevel];
            }
            else
            {
                isGameOver = true;
                TaskCompleted();
            }
        }

    }

    public void TaskCompleted()
    {
        Global.userInterfaceActiveManager.SetMenuVisibilitySmoothly(Global.MenusType.TaskCompletedContainer, true);
        endScoreUpdating.EndGameResult();
        //taskCompletedUI.SetActive(true);
        Debug.Log("Task Completed");
    }

    public void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
