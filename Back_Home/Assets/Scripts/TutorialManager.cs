using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject[] popUps;
    [SerializeField] private GameObject asteroid1;
    [SerializeField] private GameObject asteroid2;
    [SerializeField] private GameObject enemySpawner;
    private int popUpIndex;

    private float enemyWaitSpawn = 2f;
    private float startWaitTime = 9f;
    private PlayerControl playerControl;
    private BaseSystem basePlayer;
    private void Start()
    {

    }
    private void Update()
    {
        for(int i = 0; i<popUps.Length; i++)
        {
            if (i == popUpIndex && startWaitTime <= 0) 
            {
                popUps[i].SetActive(true);
            }
            else
            {
                popUps[i].SetActive(false);
                startWaitTime -= Time.deltaTime;
            }
        }
        if (popUpIndex == 0)
        {
            if(Input.GetMouseButtonDown(0))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 1)
        {
            if(Input.GetMouseButtonDown(0)) 
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 2)
        {
            if(Input.GetMouseButtonDown(0))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 3)
        {
            if (Input.GetMouseButtonDown(0))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 4)
        {
            if(Input.GetMouseButtonDown(0))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 5)
        {
            if (Input.GetMouseButtonDown(0))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 6)
        {
            if (asteroid1 == null)
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 7)
        {
            if (asteroid2 == null)
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 8)
        {
            if (enemyWaitSpawn <= 0)
            {
                enemySpawner.SetActive(true);
                popUpIndex++;
            }
            else
            {
                enemyWaitSpawn -= Time.deltaTime;
            }
        }
        else if (popUpIndex == 9)
        {
            if(Global.userInterfaceActiveManager.MenusVisibilityState[(int)Global.MenusType.Hurt_Effect])
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 10)
        {
            if(basePlayer.IsPlayerInBase)
            {
                //Switch to main scene?
            }
        }
    }
}
