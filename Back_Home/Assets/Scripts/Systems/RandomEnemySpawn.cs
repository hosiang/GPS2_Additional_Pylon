using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemySpawn : MonoBehaviour
{
    [SerializeField] private float enemyStart;
    [SerializeField] private float enemyGenerateRate;
    [SerializeField] GameObject[] enemyType;
    private int timeGenerate = 1;
    private int xPos;
    private int yPos;
    private int zPos;

    private void Start()
    {
        InvokeRepeating("RandomEnemyGenerator", enemyStart, enemyGenerateRate);
    }

    private void RandomEnemyGenerator()
    {
        xPos = Random.Range(-3, 10);
        yPos = Random.Range(-3, 10);
        zPos = Random.Range(-1, 5);
        Instantiate(enemyType[(int)Random.Range(0, enemyType.Length)], new Vector3(xPos,yPos,zPos), Quaternion.identity);
    }
}
