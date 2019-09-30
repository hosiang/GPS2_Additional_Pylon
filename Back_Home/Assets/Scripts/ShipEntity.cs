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
    protected float weightAmountMaximal = 100;

    public float WeightAmount { get { return weightAmount; } }
    public float WeightAmountMaximal { get { return weightAmountMaximal; } }

    protected float energyPoint = 0; // nitroPoint?
    protected float energyPointMaximal = 100;
    protected float energyDepletePoint = 0;

    public float EnergyPoint { get { return energyPoint; } }
    public float EnergyPointMaximal { get { return energyPointMaximal; } }

    //private float 

    void Start()
    {

    }

    void Update()
    {

    }


    public float UnloadResources()
    {
        float unloadResourcesAmount = weightAmount;
        weightAmount = 0.0f;
        return unloadResourcesAmount;
    }

    public void ReplenishHealthPoint(Object requireObject)
    {
        if(requireObject.GetType().Name == nameof(BaseSystem))
        {
            healthPoint += healthRecoverPoint*Time.deltaTime;
        }
    }

}
