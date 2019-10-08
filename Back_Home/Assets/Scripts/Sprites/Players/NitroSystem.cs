using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NitroSystem : MonoBehaviour
{
    [SerializeField] private float maxNitro = 100f;
    [SerializeField] private float currentNitro = 100f;
    [SerializeField] private float nitroRegenerationRate = 33f;
    [SerializeField] private float nitroRefuelRate = 27f; // Use in Base, Get from BaseSystem

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        NitroRegeneration();
    }
    // Enter Base increase the regeneration rate
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Base"))
        {
            if (currentNitro < maxNitro)
            {
                currentNitro += nitroRefuelRate;
            }
            else if (currentNitro > maxNitro)
            {
                currentNitro = maxNitro;
            }
        }
    }

    void NitroRegeneration()
    {
        if (currentNitro < maxNitro)
        {
            currentNitro += nitroRegenerationRate;
        }
        else if (currentNitro > maxNitro)
        {
            currentNitro = maxNitro;
        }
    }

    public float GetNitro()
    {
        return currentNitro;
    }
    public void NitroReduction(float reduceValue)
    {
        currentNitro -= reduceValue;
    }
}
