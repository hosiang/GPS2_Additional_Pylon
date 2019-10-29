using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipEntity : MonoBehaviour
{
    [SerializeField]
    protected float healthPoint = 0;
    protected float healthPointMaximal = 100;
    [SerializeField]
    protected float healthRecoverPoint = 0;


    public float HealthPoint { get { return healthPoint; } }
    public float HealthPointMaximal { get { return healthPointMaximal; } }

    [SerializeField]
    protected float weightAmount = 0;
    protected Dictionary<Global.OresTypes, float> oresAmount = new Dictionary<Global.OresTypes, float>();
    protected float weightAmountMaximal = 100;

    public float WeightAmount { get { return weightAmount; } }
    public float WeightAmountMaximal { get { return weightAmountMaximal; } }

    protected float nitroPoint = 0; // nitroPoint?
    protected float nitroPointMaximal = 100;
    protected float nitroDepletePoint = 0;

    public float NitroPoint { get { return nitroPoint; } }
    public float NitroPointMaximal { get { return nitroPointMaximal; } }

    private BaseSystem baseSystem;

    //private float 

    void Start()
    {
        for(int i = 0; i < (int)Global.OresTypes.Length; i++)
        {
            oresAmount.Add((Global.OresTypes)i, 0.0f);
        }
        //Debug.Log(oresAmount.Count);

        if(!(baseSystem = FindObjectOfType<BaseSystem>()))
        {
            Debug.Log("BaseSystem are missing!!!");
        }

        // !!! For Testing
        //oresAmount[Global.OresTypes.Iron] = 100.0f;
        //oresAmount[Global.OresTypes.no2_Ores] = 300.0f;
    }

    void Update()
    {
        //Debug.Log("No 1 ore : " + oresAmount[Global.OresTypes.Iron] + " , No 2 ore : " + oresAmount[Global.OresTypes.no2_Ores]);
    }


    public Dictionary<Global.OresTypes, float> UnloadResources(Object requireObject, Dictionary<Global.OresTypes, float> baseOresAmount)
    {
        if (requireObject == baseSystem)
        {
            for (int i = 0; i < (int)Global.OresTypes.Length; i++)
            {
                baseOresAmount[(Global.OresTypes)i] += oresAmount[(Global.OresTypes)i];
                oresAmount[(Global.OresTypes)i] = 0.0f;
            }
            weightAmount = 0.0f;

            return baseOresAmount;
        }
        else
        {
            return null;
        }
    }

    public void ReplenishHealthPoint(Object requireObject)
    {
        //if(requireObject.GetType().Name == nameof(BaseSystem))
        if (requireObject == baseSystem && healthPoint < healthPointMaximal)
        {
            healthPoint += healthRecoverPoint * Time.deltaTime;
        }
    }

    public void ReplenishNitroPoint(Object requireObject)
    {
        if (requireObject == baseSystem && nitroPoint < nitroPointMaximal)
        {
            nitroPoint = nitroPointMaximal;
        }
    }

    public Dictionary<Global.OresTypes, float> GetOresAmount(Object requireObject)
    {
        if (requireObject == baseSystem)
        {
            return oresAmount;
        }
        else
        {
            return null;
        }
    }

    public void GainOresFromAsteroid(Object requireObject, Global.OresTypes oresTypes, float oreAmount)
    {
        if (requireObject.GetType().Name == nameof(Asteroid))
        {
            oresAmount[oresTypes] += oreAmount;
            CheckWeightAmount();
        }
    }

    private void CheckWeightAmount()
    {
        weightAmount = (oresAmount[Global.OresTypes.Iron] * 1) + (oresAmount[Global.OresTypes.no2_Ores] * 2);
    }

    public float GetShipOresAmount(Global.OresTypes oresTypes)
    {
        return oresAmount[oresTypes];
    }
}
