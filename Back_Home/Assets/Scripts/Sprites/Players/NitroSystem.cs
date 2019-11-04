﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NitroSystem : MonoBehaviour
{
    [SerializeField] private float maxNitro = 100f;
    [SerializeField] private float currentNitro = 100f;
    [SerializeField] private float nitroRegenerationRate = 33f;
    [SerializeField] private float nitroRefuelRate = 27f; // Use in Base, Get from BaseSystem

    // Update is called once per frame
    void Update()
    {
        NitroRegeneration();
    }

    // Enter Base increase the regeneration rate
    private void OnCollisionEnter(Collision collision)
    {
        /*
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
        */
    }

    void NitroRegeneration()
    {
        if (currentNitro < maxNitro)
        {
            currentNitro += nitroRegenerationRate * Time.deltaTime;
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
        currentNitro -= reduceValue * Time.deltaTime;
    }
}
