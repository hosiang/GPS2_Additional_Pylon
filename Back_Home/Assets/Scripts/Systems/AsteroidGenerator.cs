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

    readonly float shieldZoneValue = 10;
    readonly float easyZoneValue = 20;
    readonly float mediumZoneValue = 40;
    readonly float hardZoneValue = 60;

    private BaseSystem baseSystem;

    private void Awake()
    {
        asteroidBigContainner = new GameObject(name_AsteroidBigContainner);
        asteroidSmallContainner = new GameObject(name_AsteroidSmallContainner);

        baseSystem = FindObjectOfType<BaseSystem>();
        for (int i = 0; i < asteroids[0].amount; i++)
        {
            asteroidBigGameObjects.Add(Instantiate<GameObject>(asteroids[0].asteroidGameObject));
            asteroidBigTransforms.Add(asteroidBigGameObjects[i].GetComponent<Transform>());
            asteroidBigTransforms[i].SetParent(asteroidBigContainner.transform);
        }
        for (int i = 0; i < asteroids[1].amount; i++)
        {
            asteroidSmallGameObjects.Add(Instantiate<GameObject>(asteroids[1].asteroidGameObject));
            asteroidSmallTransforms.Add(asteroidSmallGameObjects[i].GetComponent<Transform>());
            asteroidSmallTransforms[i].SetParent(asteroidSmallContainner.transform);
        }
    }

    void Start()
    {
        Debug.Log(Random.insideUnitCircle);

        for (int i = 0; i < asteroidBigTransforms.Count; i++)
        {
            asteroidBigTransforms[i].position = Random.insideUnitCircle * Random.Range(shieldZoneValue, easyZoneValue);
        }
        for (int i = 0; i < asteroidSmallTransforms.Count; i++)
        {
            asteroidSmallTransforms[i].position = Random.insideUnitCircle * Random.Range(easyZoneValue, mediumZoneValue);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
