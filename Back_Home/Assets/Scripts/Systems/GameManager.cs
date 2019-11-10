using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour {

    private EnemySpawner enemySpawner;
    public GameObject taskCompletedUI;

    private void Awake() {
        enemySpawner = transform.Find("Enemy Spawner").GetComponent<EnemySpawner>();

    }

    // Start is called before the first frame update
    void Start() {

        enemySpawner.generate();

    }

    // Update is called once per frame
    void Update() {

        //

    }

    public void TaskCompleted()
    {
        taskCompletedUI.SetActive(true);
        Debug.Log("Task Completed");
    }

}
