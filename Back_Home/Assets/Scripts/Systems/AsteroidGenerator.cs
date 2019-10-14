using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public struct Asteroids
{
    [SerializeField] public GameObject asteroidGameObject;
    [SerializeField] public int amount;
}

public class AsteroidGenerator : MonoBehaviour
{
    [SerializeField] private List<Asteroids> asteroids = new List<Asteroids>();

    readonly string name_AsteroidBigContainner = "BigAsteroidsContainner";
    readonly string name_AsteroidSmallContainner = "SmallAsteroidsContainner";
    private GameObject asteroidBigContainner;
    private GameObject asteroidSmallContainner;

    private List<GameObject> asteroidBigGameObjects = new List<GameObject>();
    private List<GameObject> asteroidSmallGameObjects = new List<GameObject>();

    private List<Transform> asteroidBigTransforms = new List<Transform>();
    private List<Transform> asteroidSmallTransforms = new List<Transform>();

    private void Awake()
    {
        asteroidBigContainner = new GameObject(name_AsteroidBigContainner);
        asteroidSmallContainner = new GameObject(name_AsteroidSmallContainner);
    }

    void Start()
    {
        Debug.Log(Random.insideUnitCircle);

        for (int i = 0; i < asteroids[0].amount; i++)
        {
            asteroidBigGameObjects.Add(Instantiate<GameObject>(asteroids[0].asteroidGameObject));
            asteroidBigTransforms.Add(asteroidBigGameObjects[i].GetComponent<Transform>());
            asteroidBigTransforms[i].SetParent(asteroidBigContainner.transform);
        }

        /*
        for (int i = 0; i < asteroids[1].amount; i++)
        {
            asteroidSmallGameObjects.Add(Instantiate<GameObject>(asteroids[1].asteroidGameObject));
            asteroidSmallTransforms.Add(asteroidSmallGameObjects[i].GetComponent<Transform>());
            asteroidSmallTransforms[i].SetParent(asteroidSmallContainner.transform);
        }
        */

        Vector3 tempRandomPosition = new Vector3();
        float angle = 0.0f;

        for (int i = 0; i <asteroidBigTransforms.Count; i++)
        {
            angle = Random.Range(0, Mathf.PI * 2);
            if(i <= (int)(asteroidBigTransforms.Count / 6) * 1)
            {
                tempRandomPosition.x = Mathf.Cos(angle) * Global.EasyZoneValue;
                tempRandomPosition.y = 0.0f;
                tempRandomPosition.z = Random.Range(Mathf.Sin(angle) * Global.ShieldZoneValue, Mathf.Sin(angle) * Global.EasyZoneValue);
            }
            else if(i <= (int)(asteroidBigTransforms.Count / 6) * 3)
            {
                tempRandomPosition.x = Mathf.Cos(angle) * Global.MediumZoneValue;
                tempRandomPosition.y = 0.0f;
                tempRandomPosition.z = Random.Range(Mathf.Sin(angle) * Global.EasyZoneValue, Mathf.Sin(angle) * Global.MediumZoneValue);
            }
            else if (i <= (int)(asteroidBigTransforms.Count / 6) * 6)
            {
                tempRandomPosition.x = Mathf.Cos(angle) * Global.HardZoneValue;
                tempRandomPosition.y = 0.0f;
                tempRandomPosition.z = Random.Range(Mathf.Sin(angle) * Global.MediumZoneValue, Mathf.Sin(angle) * Global.HardZoneValue);
            }
            
            asteroidBigTransforms[i].position = tempRandomPosition;
        }

        /*
        for (int i = 0; i < asteroidSmallTransforms.Count; i++)
        {
            angle = Random.Range(0, Mathf.PI * 2);

            if (i <= (int)(asteroidBigTransforms.Count / 6) * 1)
            {
                tempRandomPosition.x = Mathf.Cos(angle) * Global.EasyZoneValue;
                tempRandomPosition.y = 0.0f;
                tempRandomPosition.z = Random.Range(Mathf.Sin(angle) * Global.ShieldZoneValue, Mathf.Sin(angle) * Global.EasyZoneValue);
            }
            else if (i <= (int)(asteroidBigTransforms.Count / 6) * 3)
            {
                tempRandomPosition.x = Mathf.Cos(angle) * Global.MediumZoneValue;
                tempRandomPosition.y = 0.0f;
                tempRandomPosition.z = Random.Range(Mathf.Sin(angle) * Global.EasyZoneValue, Mathf.Sin(angle) * Global.MediumZoneValue);
            }
            else if (i <= (int)(asteroidBigTransforms.Count / 6) * 6)
            {
                tempRandomPosition.x = Mathf.Cos(angle) * Global.HardZoneValue;
                tempRandomPosition.y = 0.0f;
                tempRandomPosition.z = Random.Range(Mathf.Sin(angle) * Global.MediumZoneValue, Mathf.Sin(angle) * Global.HardZoneValue);
            }

            asteroidSmallTransforms[i].position = tempRandomPosition;
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
