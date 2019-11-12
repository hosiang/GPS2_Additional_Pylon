using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    //[SerializeField] private float safeZoneRadius;
    //[SerializeField] private float easyZoneRadius;
    //[SerializeField] private float midZoneRadius;
    //[SerializeField] private float hardZoneRadius;

    //[SerializeField] private float distanceBetweenEnemies;

    [SerializeField] private int enemiesInEasyZone;
    [SerializeField] private int enemiesInMidZone;
    [SerializeField] private int enemiesInHardZone;

    [SerializeField] private GameObject[] enemiesToSpawn;

    private Transform container;

    private Vector3 previousPosition = Vector3.zero;

    private Vector3 randomPosition(float min, float max) {

        Vector3 position;

        position.y = 0f;

        while (true) {

            position.x = Random.Range(-max, max);
            position.z = Random.Range(-max, max);

            float radius = Vector3.Distance(position, Vector3.zero);

            if (radius > min && radius <= max) {

                //if (Vector3.Distance(position, previousPosition) >= distanceBetweenEnemies) {
                    return position;
                //}

            }

        }

    }

    private void spawnEnemies(int amountToSpawn, Global.ZoneLevels currentZone) {

        for (int i = 0; i < amountToSpawn; ++i) {

            Instantiate(
                        enemiesToSpawn[Random.Range(0, enemiesToSpawn.Length)],
                        randomPosition(Global.zoneValues[(int)currentZone - 1], Global.zoneValues[(int)currentZone]),
                        new Quaternion()
                    ).gameObject.transform.SetParent(container);
            /*
            switch (currentZone) {

                case Global.ZoneLevels.EasyZone:

                    Instantiate(
                        enemiesToSpawn[Random.Range(0, enemiesToSpawn.Length)],
                        randomPosition(Global.zoneValues[(int)currentZone-1], Global.zoneValues[(int)currentZone]),
                        new Quaternion()
                    ).gameObject.transform.SetParent(container);

                    break;

                case Global.ZoneLevels.MediumZone:

                    Instantiate(
                        enemiesToSpawn[Random.Range(0, enemiesToSpawn.Length)],
                        randomPosition(Global.zoneValues[(int)currentZone - 1], Global.zoneValues[(int)currentZone]),
                        new Quaternion()
                    ).gameObject.transform.SetParent(container);

                    break;

                case Global.ZoneLevels.HardZone:

                    Instantiate(
                        enemiesToSpawn[Random.Range(0, enemiesToSpawn.Length)],
                        randomPosition(Global.zoneValues[(int)currentZone - 1], Global.zoneValues[(int)currentZone]),
                        new Quaternion()
                    ).gameObject.transform.SetParent(container);

                    break;

            }
            */
        }

    }

    public void generate() {

        container = (new GameObject("Enemy Container")).transform;

        spawnEnemies(enemiesInEasyZone, Global.ZoneLevels.EasyZone);
        spawnEnemies(enemiesInMidZone, Global.ZoneLevels.MediumZone);
        spawnEnemies(enemiesInHardZone, Global.ZoneLevels.HardZone);

    }

}
