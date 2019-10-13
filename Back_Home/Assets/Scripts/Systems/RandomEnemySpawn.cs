﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemySpawn : MonoBehaviour
{
    [SerializeField] GameObject[] enemyType;
    private int maxEnemies = 10; // depend on the level set, can set to how many enemies to be spawned
    private int enemyCounter = 0; // to check the maximum enemies?
    private int timeGenerate = 1;
    private int xPos;
    private int yPos;
    private int zPos;

    private void Update()
    {
        if (enemyCounter < maxEnemies)
        {
            RandomEnemyGenerator();
            enemyCounter++;
        }
        
    }

    private void RandomEnemyGenerator()
    {
        xPos = Random.Range(-3, 10);
        yPos = Random.Range(-3, 10);
        zPos = Random.Range(-1, 5);
        Instantiate(enemyType[(int)Random.Range(0, enemyType.Length)], new Vector3(xPos,yPos,zPos), Quaternion.identity);
    }
}
