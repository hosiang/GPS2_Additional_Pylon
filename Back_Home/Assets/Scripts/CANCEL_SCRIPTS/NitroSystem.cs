using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NitroSystem : MonoBehaviour
{
    [Header("Nitro Point")]
    [SerializeField] protected float currentNitro = 100f;
    [SerializeField] protected float maximalNitro = 100f;

    [SerializeField] private float nitroRegenerationRate = 33f;
    [SerializeField] private float nitroRefuelRate = 27f; // Use in Base, Get from BaseSystem

    [SerializeField] protected bool isOverheat = false;

    public float CurrentNitro { get { return currentNitro; } }
    public float MaximalNitro { get { return maximalNitro; } }
    public bool IsOverheat { get { return isOverheat; } }


    protected void Start_NitroSystem() // This will run on ShipEntity's Start() function
    {

    }

    protected void Update_NitroSystem() // This will run on ShipEntity's Update() function
    {

    }

    private void NitroChecker()
    {
        if (currentNitro <= 0.0f)
        {
            currentNitro = 0.0f;
            isOverheat = true;
        }
    }

    protected void NitroRegeneration()
    {
        if (currentNitro < maximalNitro)
        {
            currentNitro += nitroRegenerationRate * Time.deltaTime;
        }
        else if (isOverheat && currentNitro >= maximalNitro)
        {
            isOverheat = false;
            currentNitro = maximalNitro;
        }
        else if (currentNitro > maximalNitro)
        {
            currentNitro = maximalNitro;
        }
    }

    protected void NitroReduction()
    {
        currentNitro -= nitroRefuelRate * Time.deltaTime;
    }
}
