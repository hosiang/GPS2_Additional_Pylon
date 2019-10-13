﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAsteroidSpawn : MonoBehaviour
{
    /*[SerializeField] private float asteroidStart;
    [SerializeField] private float asteroidGenerateRate;*/

    [SerializeField] GameObject[] asteroidType;
    [SerializeField] private int maxAsteroid = 10;
    private int asteroidsCounter = 0;
    private int timeGenerate = 1;
    //private int xPos;
    //private int yPos;
    //private int zPos;
    private Vector3 position = Vector3.zero;

    private void Update()
    {
        if (asteroidsCounter < maxAsteroid)
        {
            RandomAsteroidGenerator();
            asteroidsCounter++;
        }
    }
    private void RandomAsteroidGenerator()
    {
        /*
        xPos = Random.Range(-5, 15);
        yPos = Random.Range(-5, 15);
        zPos = Random.Range(-1, 5);
        */

        position.x = Random.Range(-50, 50);
        position.y = 0.0f;
        position.z = Random.Range(-50, 50);

        //Instantiate(asteroidType[(int)Random.Range(0, asteroidType.Length)], new Vector3(xPos, yPos, zPos), Quaternion.identity);
        Instantiate(asteroidType[(int)Random.Range(0, asteroidType.Length)], position, Quaternion.identity);
    }

    /*private void Start()
    {
        InvokeRepeating("RandomEnemyGenerator", asteroidStart, asteroidGenerateRate);
    }

    private void RandomEnemyGenerator()
    {
        xPos = Random.Range(-5, 15);
        yPos = Random.Range(-5, 15);
        zPos = Random.Range(-1, 5);
        Instantiate(asteroidType[(int)Random.Range(0, asteroidType.Length)], new Vector3(xPos, yPos, zPos), Quaternion.identity);
    }*/
}
