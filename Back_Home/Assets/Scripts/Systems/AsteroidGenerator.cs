using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class Asteroids
{
    [SerializeField] public List<GameObject> asteroidGameObject = new List<GameObject>();
    [SerializeField] public int amount;
}


public class AsteroidGenerator : MonoBehaviour
{
    [SerializeField] private List<Asteroids> eachZoenAsteroids = new List<Asteroids>();
    /*
    readonly string name_AsteroidBigContainner = "BigAsteroidsContainner";
    readonly string name_AsteroidSmallContainner = "SmallAsteroidsContainner";
    private GameObject asteroidBigContainner;
    private GameObject asteroidSmallContainner;
    */
    readonly List<string> name_ZoneContainner = new List<string> { "EasyZoneAsteroidsContainner", "MediumZoneAsteroidsContainner", "HardZoneAsteroidsContainner" };
    private List<GameObject> zoneContainner = new List<GameObject>();

    private Dictionary<Global.ZoneLevels, GameObject> asteroidBigGameObjects = new Dictionary<Global.ZoneLevels, GameObject>();
    private Dictionary<Global.ZoneLevels, GameObject> asteroidSmallGameObjects = new Dictionary<Global.ZoneLevels, GameObject>();

    private Dictionary<Global.ZoneLevels, Transform> asteroidBigTransforms = new Dictionary<Global.ZoneLevels, Transform>();
    private Dictionary<Global.ZoneLevels, Transform> asteroidSmallTransforms = new Dictionary<Global.ZoneLevels, Transform>();

    private void Awake()
    {
        //asteroidBigContainner = new GameObject(name_AsteroidBigContainner);
        //asteroidSmallContainner = new GameObject(name_AsteroidSmallContainner);
        
        for (int i = 0; i < ((int)Global.ZoneLevels.Length - 1); i++)
        {
            GameObject tempGameObject = new GameObject(name_ZoneContainner[i]);
            zoneContainner.Add(tempGameObject);
        }
    }

    void Start()
    {
        /*
        for (Global.ZoneLevels i = Global.ZoneLevels.EasyZone; i < Global.ZoneLevels.Length; i++)
        {
            //for (int j = 0; j < asteroids[0].amount; j++)
            for (int j = 0; j < eachZoenAsteroids[(int)i - 1].amount; j++) // Big asteroids
            {
                int randomAsteroids = Random.Range(0, eachZoenAsteroids[(int)i - 1].asteroidGameObject.Count);
                asteroidBigGameObjects.Add(i, Instantiate<GameObject>(eachZoenAsteroids[(int)i - 1].asteroidGameObject[randomAsteroids]));
                asteroidBigTransforms.Add(i, asteroidBigGameObjects[(Global.ZoneLevels)i].GetComponent<Transform>());
                asteroidBigTransforms[j].SetParent(zoneContainner[0].transform);
                //Debug.Log(asteroidBigTransforms[j].parent.name);
            }
            Debug.Log("zone amount = " + asteroidBigGameObjects.Count);
            
            for (int j = 0; j < (eachZoneAsteroidAmount[i] / 3) * 2; j++) // Small asteroids
            {
                asteroidSmallGameObjects.Add(Instantiate<GameObject>(asteroids[1].asteroidGameObject));
                asteroidSmallTransforms.Add(asteroidSmallGameObjects[j].GetComponent<Transform>());
                asteroidSmallTransforms[j].SetParent(zoneContainner[i].transform);
            }
            
        }
    */
        Debug.Log("child amount = " + zoneContainner[0].transform.GetChildCount());

        #region !! dont delete !!
        /*
        for (int i = 0; i < asteroids[1].amount; i++)
        {
            asteroidSmallGameObjects.Add(Instantiate<GameObject>(asteroids[1].asteroidGameObject));
            asteroidSmallTransforms.Add(asteroidSmallGameObjects[i].GetComponent<Transform>());
            asteroidSmallTransforms[i].SetParent(asteroidSmallContainner.transform);
        }
        */
        #endregion

        Vector3 tempRandomPosition = new Vector3();
        float angle = 0.0f;
        /*
        for (int i = 0; i < asteroidBigTransforms.Count; i++)
        {
            angle = Random.Range(0, Mathf.PI * 2);
            if (i <= (int)(asteroidBigTransforms.Count / 6) * 1)
            {
                tempRandomPosition.x = Mathf.Cos(angle) * Global.zoneValues[(int)Global.ZoneLevels.EasyZone];
                tempRandomPosition.y = 0.0f;
                tempRandomPosition.z = Random.Range(Mathf.Sin(angle) * Global.zoneValues[(int)Global.ZoneLevels.ShieldZone], Mathf.Sin(angle) * Global.zoneValues[(int)Global.ZoneLevels.EasyZone]);
            }
            else if (i <= (int)(asteroidBigTransforms.Count / 6) * 3)
            {
                tempRandomPosition.x = Mathf.Cos(angle) * Global.zoneValues[(int)Global.ZoneLevels.MediumZone];
                tempRandomPosition.y = 0.0f;
                tempRandomPosition.z = Random.Range(Mathf.Sin(angle) * Global.zoneValues[(int)Global.ZoneLevels.EasyZone], Mathf.Sin(angle) * Global.zoneValues[(int)Global.ZoneLevels.MediumZone]);
            }
            else if (i <= (int)(asteroidBigTransforms.Count / 6) * 6)
            {
                tempRandomPosition.x = Mathf.Cos(angle) * Global.zoneValues[(int)Global.ZoneLevels.HardZone];
                tempRandomPosition.y = 0.0f;
                tempRandomPosition.z = Random.Range(Mathf.Sin(angle) * Global.zoneValues[(int)Global.ZoneLevels.MediumZone], Mathf.Sin(angle) * Global.zoneValues[(int)Global.ZoneLevels.HardZone]);
            }

            for (int j = 0; j < asteroidSmallTransforms.Count; j++)
            {
                angle = Random.Range(0, Mathf.PI * 2);
                if (j <= (int)(asteroidSmallTransforms.Count / 6) * 1)
                {
                    tempRandomPosition.x = Mathf.Cos(angle) * Global.zoneValues[(int)Global.ZoneLevels.EasyZone];
                    tempRandomPosition.y = 0.0f;
                    tempRandomPosition.z = Random.Range(Mathf.Sin(angle) * Global.zoneValues[(int)Global.ZoneLevels.ShieldZone], Mathf.Sin(angle) * Global.zoneValues[(int)Global.ZoneLevels.EasyZone]);
                }
                else if (j <= (int)(asteroidSmallTransforms.Count / 6) * 3)
                {
                    tempRandomPosition.x = Mathf.Cos(angle) * Global.zoneValues[(int)Global.ZoneLevels.MediumZone];
                    tempRandomPosition.y = 0.0f;
                    tempRandomPosition.z = Random.Range(Mathf.Sin(angle) * Global.zoneValues[(int)Global.ZoneLevels.EasyZone], Mathf.Sin(angle) * Global.zoneValues[(int)Global.ZoneLevels.MediumZone]);
                }
                else if (j <= (int)(asteroidSmallTransforms.Count / 6) * 6)
                {
                    tempRandomPosition.x = Mathf.Cos(angle) * Global.zoneValues[(int)Global.ZoneLevels.HardZone];
                    tempRandomPosition.y = 0.0f;
                    tempRandomPosition.z = Random.Range(Mathf.Sin(angle) * Global.zoneValues[(int)Global.ZoneLevels.MediumZone], Mathf.Sin(angle) * Global.zoneValues[(int)Global.ZoneLevels.HardZone]);
                }

                asteroidBigTransforms[i].position = tempRandomPosition;
                asteroidSmallTransforms[j].position = tempRandomPosition;
            }
        }
        */
        #region !! dont delete !!
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
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

