using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private EnemySpawner enemySpawner;
    public GameObject taskCompletedUI;
    private BaseSystem baseSystem;

    private void Awake() {
        if (Global.gameManager == null) { Global.gameManager = this; }
        else { Destroy(gameObject); return; }

        enemySpawner = transform.Find("Enemy Spawner").GetComponent<EnemySpawner>();

    }

    // Start is called before the first frame update
    void Start() {

        enemySpawner.generate();

        baseSystem = FindObjectOfType<BaseSystem>();

    }

    // Update is called once per frame
    void Update() {
        
    }

    public void TaskCompleted()
    {
        taskCompletedUI.SetActive(true);
        Debug.Log("Task Completed");
    }

    public void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
