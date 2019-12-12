using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
[System.Serializable] public class Asteroids
{
    [SerializeField] public List<GameObject> asteroidGameObject = new List<GameObject>();
    [SerializeField] public int amount;
}
*/

public class AsteroidGenerator : MonoBehaviour
{
    [SerializeField, Range(0.0f, 20.0f)] private float eachAsteroidBetweenDistance = 5.0f;
    [SerializeField, Range(0, 50)] private int extraRandomTime = 40;

    [SerializeField] private List<GameObject> asteroidTypes = new List<GameObject>();
    [SerializeField] private List<int> eachZoneAsteroidAmount = new List<int>();


    //[SerializeField] private List<Asteroids> eachZoenAsteroids = new List<Asteroids>();
    
    //readonly string name_AsteroidBigContainner = "BigAsteroidsContainner";
    //readonly string name_AsteroidSmallContainner = "SmallAsteroidsContainner";
    //private GameObject asteroidBigContainner;
    //private GameObject asteroidSmallContainner;
    

    readonly string[] name_ZoneContainner = new string[] { "EasyZoneAsteroidsContainner", "MediumZoneAsteroidsContainner", "HardZoneAsteroidsContainner" };
    private List<GameObject> zoneContainner = new List<GameObject>();
    readonly string[] name_AsteroidContainner = new string[] { "BigAsteroidContainner", "SmallAsteroidContainner" };
    private List<GameObject> asteroidContainner = new List<GameObject>();

    
    private List<GameObject> asteroidBigGameObjects_EasyZone = new List<GameObject>();
    private List<GameObject> asteroidBigGameObjects_MediumZone = new List<GameObject>();
    private List<GameObject> asteroidBigGameObjects_HardZone = new List<GameObject>();

    private List<GameObject> asteroidSmallGameObjects_EasyZone = new List<GameObject>();
    private List<GameObject> asteroidSmallGameObjects_MediumZone = new List<GameObject>();
    private List<GameObject> asteroidSmallGameObjects_HardZone = new List<GameObject>();


    private List<Transform> asteroidBigTransforms_EasyZone = new List<Transform>();
    private List<Transform> asteroidBigTransforms_MediumZone = new List<Transform>();
    private List<Transform> asteroidBigTransforms_HardZone = new List<Transform>();

    private List<Transform> asteroidSmallTransforms_EasyZone = new List<Transform>();
    private List<Transform> asteroidSmallTransforms_MediumZone = new List<Transform>();
    private List<Transform> asteroidSmallTransforms_HardZone = new List<Transform>();


    //private Dictionary<Global.ZoneLevels, List<GameObject>> asteroidBigGameObjects = new Dictionary<Global.ZoneLevels, List<GameObject>>();
    //private Dictionary<Global.ZoneLevels, List<GameObject>> asteroidSmallGameObjects = new Dictionary<Global.ZoneLevels, List<GameObject>>();

    //private Dictionary<Global.ZoneLevels, List<Transform>> asteroidBigTransforms = new Dictionary<Global.ZoneLevels, List<Transform>>();
    //private Dictionary<Global.ZoneLevels, List<Transform>> asteroidSmallTransforms = new Dictionary<Global.ZoneLevels, List<Transform>>();
    

    private void Awake()
    {
        //asteroidBigContainner = new GameObject(name_AsteroidBigContainner);
        //asteroidSmallContainner = new GameObject(name_AsteroidSmallContainner);

        eachAsteroidBetweenDistance = eachAsteroidBetweenDistance > 20.0f ? 20.0f : eachAsteroidBetweenDistance;

        for (int i = (int)Global.ZoneLevels.EasyZone; i < (int)Global.ZoneLevels.Length; i++)
        {
            zoneContainner.Add(new GameObject(name_ZoneContainner[i - 1]));
        }
        
        for (int i = 0; i < zoneContainner.Count; i++)
        {
            for (int j = 0; j < name_AsteroidContainner.Length; j++)
            {
                asteroidContainner.Add(new GameObject(name_AsteroidContainner[j]));
                asteroidContainner[(i * name_AsteroidContainner.Length) + j].transform.SetParent(zoneContainner[i].transform);
            }
        }
        
    }

    void Start()
    {
        int randomAsteroids = 0;

        for (int i = 0; i < eachZoneAsteroidAmount[0]; i++) // Easy Zone
        {
            randomAsteroids = Random.Range(0, asteroidTypes.Count);

            if (i < (int)(eachZoneAsteroidAmount[0] / 3))
            {
                asteroidBigGameObjects_EasyZone.Add(Instantiate<GameObject>(asteroidTypes[randomAsteroids]));
                asteroidBigTransforms_EasyZone.Add(asteroidBigGameObjects_EasyZone[i].GetComponent<Transform>());
                asteroidBigTransforms_EasyZone[i].SetParent(asteroidContainner[0].transform);

                asteroidBigGameObjects_EasyZone[i].GetComponent<Asteroid>().SetAsteroidSize(this, Global.AstroidType.AsteroidBig, Global.ZoneLevels.EasyZone); // Set the type of the asteroid
            }
            else
            {
                asteroidSmallGameObjects_EasyZone.Add(Instantiate<GameObject>(asteroidTypes[randomAsteroids]));
                asteroidSmallTransforms_EasyZone.Add(asteroidSmallGameObjects_EasyZone[i - (int)(eachZoneAsteroidAmount[0] / 3)].GetComponent<Transform>());
                asteroidSmallTransforms_EasyZone[i - (int)(eachZoneAsteroidAmount[0] / 3)].localScale /= 3.0f;
                asteroidSmallTransforms_EasyZone[i - (int)(eachZoneAsteroidAmount[0] / 3)].SetParent(asteroidContainner[1].transform);

                asteroidSmallGameObjects_EasyZone[i - (int)(eachZoneAsteroidAmount[0] / 3)].GetComponent<Asteroid>().SetAsteroidSize(this, Global.AstroidType.AsteroidSmall, Global.ZoneLevels.EasyZone); // Set the type of the asteroid
            }
        }
        for (int i = 0; i < eachZoneAsteroidAmount[1]; i++) // Medium Zone
        {
            randomAsteroids = Random.Range(0, asteroidTypes.Count);

            if (i < (int)(eachZoneAsteroidAmount[1] / 3))
            {
                asteroidBigGameObjects_MediumZone.Add(Instantiate<GameObject>(asteroidTypes[randomAsteroids]));
                asteroidBigTransforms_MediumZone.Add(asteroidBigGameObjects_MediumZone[i].GetComponent<Transform>());
                asteroidBigTransforms_MediumZone[i].SetParent(asteroidContainner[2].transform);

                asteroidBigGameObjects_MediumZone[i].GetComponent<Asteroid>().SetAsteroidSize(this, Global.AstroidType.AsteroidBig, Global.ZoneLevels.MediumZone); // Set the type of the asteroid
            }
            else
            {
                asteroidSmallGameObjects_MediumZone.Add(Instantiate<GameObject>(asteroidTypes[randomAsteroids]));
                asteroidSmallTransforms_MediumZone.Add(asteroidSmallGameObjects_MediumZone[i - (int)(eachZoneAsteroidAmount[1] / 3)].GetComponent<Transform>());
                asteroidSmallTransforms_MediumZone[i - (int)(eachZoneAsteroidAmount[1] / 3)].localScale /= 3.0f;
                asteroidSmallTransforms_MediumZone[i - (int)(eachZoneAsteroidAmount[1] / 3)].SetParent(asteroidContainner[3].transform);

                asteroidSmallGameObjects_MediumZone[i - (int)(eachZoneAsteroidAmount[1] / 3)].GetComponent<Asteroid>().SetAsteroidSize(this, Global.AstroidType.AsteroidSmall, Global.ZoneLevels.MediumZone); // Set the type of the asteroid
            }
        }
        
        for (int i = 0; i < eachZoneAsteroidAmount[2]; i++) // Hard Zone
        {
            randomAsteroids = Random.Range(0, asteroidTypes.Count);

            if (i < (int)(eachZoneAsteroidAmount[2] / 3))
            {
                asteroidBigGameObjects_HardZone.Add(Instantiate<GameObject>(asteroidTypes[randomAsteroids]));
                asteroidBigTransforms_HardZone.Add(asteroidBigGameObjects_HardZone[i].GetComponent<Transform>());
                asteroidBigTransforms_HardZone[i].SetParent(asteroidContainner[4].transform);

                asteroidBigGameObjects_HardZone[i].GetComponent<Asteroid>().SetAsteroidSize(this, Global.AstroidType.AsteroidBig, Global.ZoneLevels.HardZone); // Set the type of the asteroid
            }
            else
            {
                asteroidSmallGameObjects_HardZone.Add(Instantiate<GameObject>(asteroidTypes[randomAsteroids]));
                asteroidSmallTransforms_HardZone.Add(asteroidSmallGameObjects_HardZone[i - (int)(eachZoneAsteroidAmount[2] / 3)].GetComponent<Transform>());
                asteroidSmallTransforms_HardZone[i - (int)(eachZoneAsteroidAmount[2] / 3)].localScale /= 3.0f;
                asteroidSmallTransforms_HardZone[i - (int)(eachZoneAsteroidAmount[2] / 3)].SetParent(asteroidContainner[5].transform);

                asteroidSmallGameObjects_HardZone[i - (int)(eachZoneAsteroidAmount[2] / 3)].GetComponent<Asteroid>().SetAsteroidSize(this, Global.AstroidType.AsteroidSmall, Global.ZoneLevels.HardZone); // Set the type of the asteroid
            }
        }
        
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
        

        RandomPosition( eachZoneAsteroidAmount[0],
            ref asteroidBigTransforms_EasyZone, ref asteroidSmallTransforms_EasyZone,
            Global.zonesRadius[(int)Global.ZoneLevels.ShieldZone], Global.zonesRadius[(int)Global.ZoneLevels.EasyZone] );

        RandomPosition(eachZoneAsteroidAmount[1],
            ref asteroidBigTransforms_MediumZone, ref asteroidSmallTransforms_MediumZone,
            Global.zonesRadius[(int)Global.ZoneLevels.EasyZone], Global.zonesRadius[(int)Global.ZoneLevels.MediumZone]);

        RandomPosition(eachZoneAsteroidAmount[2],
            ref asteroidBigTransforms_HardZone, ref asteroidSmallTransforms_HardZone,
            Global.zonesRadius[(int)Global.ZoneLevels.MediumZone], Global.zonesRadius[(int)Global.ZoneLevels.HardZone]);


        for (int i = 0; i < 50; i++)
        {
            ExtraRandomPosition(eachZoneAsteroidAmount[0],
            ref asteroidBigTransforms_EasyZone, ref asteroidSmallTransforms_EasyZone,
            Global.zonesRadius[(int)Global.ZoneLevels.ShieldZone], Global.zonesRadius[(int)Global.ZoneLevels.EasyZone]);

            ExtraRandomPosition(eachZoneAsteroidAmount[1],
            ref asteroidBigTransforms_MediumZone, ref asteroidSmallTransforms_MediumZone,
            Global.zonesRadius[(int)Global.ZoneLevels.EasyZone], Global.zonesRadius[(int)Global.ZoneLevels.MediumZone]);

            ExtraRandomPosition(eachZoneAsteroidAmount[2],
            ref asteroidBigTransforms_HardZone, ref asteroidSmallTransforms_HardZone,
            Global.zonesRadius[(int)Global.ZoneLevels.MediumZone], Global.zonesRadius[(int)Global.ZoneLevels.HardZone]);
        }

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

    void CreateAsteroid()
    {

    }

    void RandomPosition(int sizeOfAll,ref List<Transform> bigObject,ref List<Transform> smallObject, float zoneInnerSize, float zoneOutsiteSize)
    {
        Vector3 tempRandomPosition = new Vector3();
        float angle = 0.0f;

        for (int i = 0; i < sizeOfAll; i++) // Easy Zone
        {
            angle = Random.Range(0, Mathf.PI * 2);

            tempRandomPosition.x = Random.Range(Mathf.Cos(angle) * zoneInnerSize, Mathf.Cos(angle) * zoneOutsiteSize);
            tempRandomPosition.y = 0.0f;
            tempRandomPosition.z = Random.Range(Mathf.Sin(angle) * zoneInnerSize, Mathf.Sin(angle) * zoneOutsiteSize);

            if (i < bigObject.Count) {
                bigObject[i].position = tempRandomPosition;
                bigObject[i].Rotate(0.0f, Random.Range(0, 360.0f), 0.0f);
            }
            else {

                smallObject[i - bigObject.Count].position = tempRandomPosition;
                smallObject[i - bigObject.Count].Rotate(0.0f, Random.Range(0, 360.0f), 0.0f);
            }

        }

    }

    void ExtraRandomPosition(int sizeOfAll, ref List<Transform> bigObject, ref List<Transform> smallObject, float zoneInnerSize, float zoneOutsiteSize)
    {

        Vector3 tempPosition = new Vector3();
        Vector3 tempRandomPosition = new Vector3();
        LayerMask layerMask = LayerMask.GetMask("Asteroid") + LayerMask.GetMask("Enemy");
        Collider[] tempCollider;
        float angle = 0.0f;

        //bool test = Physics.CheckSphere(tempRandomPosition, eachAsteroidBetweenDistance, layerMask);


        for (int i = 0; i < sizeOfAll; i++) // Easy Zone
        {
            if (i < bigObject.Count)
            {
                tempPosition = bigObject[i].position;
            }
            else
            {
                tempPosition = smallObject[i - bigObject.Count].position;
            }

            tempCollider = Physics.OverlapSphere(tempPosition, eachAsteroidBetweenDistance, layerMask);

            if (tempCollider.Length <= 0) continue;

            while (tempCollider.Length > 0)
            {
                angle = Random.Range(0, Mathf.PI * 2);
                tempRandomPosition.x = Random.Range(Mathf.Cos(angle) * zoneInnerSize, Mathf.Cos(angle) * zoneOutsiteSize);
                tempRandomPosition.y = 0.0f;
                tempRandomPosition.z = Random.Range(Mathf.Sin(angle) * zoneInnerSize, Mathf.Sin(angle) * zoneOutsiteSize);

                tempCollider = Physics.OverlapSphere(tempRandomPosition, eachAsteroidBetweenDistance, layerMask);

                if (tempCollider.Length > 0)
                {
                    continue;
                }
                else
                {
                    if (i < bigObject.Count)
                    {
                        bigObject[i].position = tempRandomPosition;
                    }
                    else
                    {
                        smallObject[i - bigObject.Count].position = tempRandomPosition;
                    }
                    break;
                }

            }

        }

    }

    private void OnDestroy()
    {
        asteroidTypes.Clear();
        eachZoneAsteroidAmount.Clear();

        zoneContainner.Clear();
        asteroidContainner.Clear();

        asteroidBigGameObjects_EasyZone.Clear();
        asteroidBigGameObjects_MediumZone.Clear();
        asteroidBigGameObjects_HardZone.Clear();

        asteroidSmallGameObjects_EasyZone.Clear();
        asteroidSmallGameObjects_MediumZone.Clear();
        asteroidSmallGameObjects_HardZone.Clear();


        asteroidBigTransforms_EasyZone.Clear();
        asteroidBigTransforms_MediumZone.Clear();
        asteroidBigTransforms_HardZone.Clear();

        asteroidSmallTransforms_EasyZone.Clear();
        asteroidSmallTransforms_MediumZone.Clear();
        asteroidSmallTransforms_HardZone.Clear();
    }

}

