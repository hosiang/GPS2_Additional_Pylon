﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipEntity : MonoBehaviour
{
    [Header("Health")]
    private float currentHealth = 100f;
    private float maximalHealth = 100f;

    private float healthRegenerationRate = 10f; // Take from BaseSystem

    [SerializeField] private bool isDead = false;
    private bool onHealing = false;

    public float CurrentHealth { get { return currentHealth; } }
    public float MaximalHealth { get { return maximalHealth; } }
    public bool IsDead { get { return isDead; } }

    [Header("Nitro")]
    [SerializeField] private float currentNitro = 100f;
    [SerializeField] private float maximalNitro = 100f;

    private float nitroRegenerationRate = 1f;
    private float nitroRefuelRate = 10f; // Use in Base, Get from BaseSystem

    [SerializeField] private bool isOverheat = false;

    public float CurrentNitro { get { return currentNitro; } }
    public float MaximalNitro { get { return maximalNitro; } }
    public bool IsOverheat { get { return isOverheat; } }


    [Header("Weight")]
    [SerializeField] private float currentWeight = 0f;
    [SerializeField] private float maximalWeight = 100f;

    public float CurrentWeight { get { return currentWeight; } }
    public float MaximalWeight { get { return maximalWeight; } }


    private Dictionary<Global.OresTypes, float> oresAmount = new Dictionary<Global.OresTypes, float>();

    private BaseSystem baseSystem;


    [SerializeField] private Animator playerAnimator;

    //Particle
    [SerializeField] private ParticleSystem astroidHitParticle;
    [SerializeField] private float astroidCollisionSpeed;

    void Start()
    {

        for (int i = 0; i < (int)Global.OresTypes.Length; i++)
        {
            oresAmount.Add((Global.OresTypes)i, 0.0f); // Set up all kind of ores type to |*|oresAmount|*|
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
        // !!! For Debug
        //Debug.Log("No 1 ore : " + oresAmount[Global.OresTypes.Iron] + " , No 2 ore : " + oresAmount[Global.OresTypes.no2_Ores]);
    }

    private void OnCollisionEnter(Collision collision) {

        if (collision.relativeVelocity.magnitude >= astroidCollisionSpeed && collision.collider.CompareTag(Global.tag_Astroid)) {

            playCollisionEffect(ref collision, Global.ParticleEffectType.astroid);
        }
    }

    private void playCollisionEffect(ref Collision collision, Global.ParticleEffectType particleObject) {

        for (int i = 0; i < collision.contacts.Length; ++i) {

            switch (particleObject) {

                case Global.ParticleEffectType.astroid:
                    Instantiate(astroidHitParticle, collision.contacts[i].point, Quaternion.identity);
                    break;
            }
        }
    }


    #region |* Health fuctions *|
    private void HealthChecker()
    {
        if (currentHealth <= 0.0f)
        {
            currentHealth = 0.0f;
            isDead = true;
        }
    }
    public void TakeDamage(float damage)
    {
        //Debug.Log($"<color=red>Player took {damage}</color>");
        playerAnimator.SetTrigger("isHurt");
        //blingHurt.Play();
        //boomHurt.Play();

        currentHealth -= damage;
        HealthChecker();
    }

    public void ReplenishHealthPoint(Object requireObject)
    {
        //if(requireObject.GetType().Name == nameof(BaseSystem))
        if (requireObject == baseSystem)
        {
            if (currentHealth < maximalHealth)
            {
                //healing1.Play();
                //healing2.Play();

                currentHealth += healthRegenerationRate * Time.deltaTime; // Smooth the regeneration speed.
            }
            else if (currentHealth > maximalHealth) // This only will trigger one time when |*|currentHealth|*| overstep the |*|maximalHealth|*|
            {
                //if (healing1.isPlaying || healing2.isPlaying || healing2.isPlaying)
                //{
                 //   healing1.Stop();
                  //  healing2.Stop();
                //}

                currentHealth = maximalHealth; // For limit the overstep |*|currentHealth|*| to |*|maximalHealth|*|
            }
        }
        else
        {
            Debug.LogError("Alert! Have some Unauthorized class try to entry to invoking this functions!");
        }
    }
    #endregion |* End Of Health fuctions *|


    #region |* Nitro fuctions *|
    private void NitroChecker()
    {
        if (currentNitro <= 0.0f)
        {
            currentNitro = 0.0f;
            isOverheat = true;
        }
    }
    public void ReplenishNitroPoint(Object requireObject)
    {
        //Debug.Log("Recover Nitro : Object " + requireObject.GetType().Name);
        if (requireObject.GetType().Name == nameof(PlayerControl))
        {
            if (currentNitro < maximalNitro)
            {
                currentNitro += (isOverheat ? (nitroRegenerationRate * 20f) : nitroRegenerationRate) * Time.deltaTime;
                if (currentNitro > maximalNitro) { currentNitro = maximalNitro; }
            }
            // If isOverheat is true and |*|currentNitro|*| is more then or equal to |*|maximalNitro|*|, then it will allow player able to thrust again
            else if (isOverheat && currentNitro >= maximalNitro)
            {
                isOverheat = false;
                currentNitro = maximalNitro;
            }
        }
        else if (requireObject == baseSystem)
        {
            if (currentNitro < maximalNitro)
            {
                currentNitro += nitroRegenerationRate * 2.0f * Time.deltaTime;
                if (currentNitro > maximalNitro) { currentNitro = maximalNitro; }
            }
            // If isOverheat is true and |*|currentNitro|*| is more then or equal to |*|maximalNitro|*|, then it will allow player able to thrust again
            else if (isOverheat && currentNitro >= maximalNitro)
            {
                isOverheat = false;
                currentNitro = maximalNitro;
            } 
            
        }
        else
        {
            Debug.LogError("Alert! Have some Unauthorized class try to entry to invoking this functions!");
        }
    }
    public void NitroReduction()
    {
        currentNitro -= nitroRefuelRate * Time.deltaTime;
        NitroChecker();
    }
    #endregion |* End Of Nitro fuctions *|


    #region |* Weight fuctions *|
    public void UnloadResources(Object requireObject, ref Dictionary<Global.OresTypes, float> baseOresAmount, ref Dictionary<Global.OresTypes, float> finalOresAmount)
    {
        if (requireObject == baseSystem)
        {
            for (int i = 0; i < (int)Global.OresTypes.Length; i++)
            {
                baseOresAmount[(Global.OresTypes)i] += oresAmount[(Global.OresTypes)i];
                finalOresAmount[(Global.OresTypes)i] += oresAmount[(Global.OresTypes)i];
                oresAmount[(Global.OresTypes)i] = 0.0f;
            }
            currentWeight = 0.0f;
        }
        else
        {
            Debug.LogError("Alert! Have some Unauthorized class try to entry to invoking this functions!");
        }
    }

    public void GainOres(Object requireObject, Global.OresTypes oresTypes, float oreAmount = 1.0f)
    {
        if (requireObject.GetType().Name == nameof(Ores) || requireObject.GetType().Name == nameof(FinalKey))
        {
            switch (oresTypes)
            {
                case Global.OresTypes.Ore_No1:
                    this.oresAmount[oresTypes] += oreAmount;
                    CheckWeightAmount();
                    break;
                case Global.OresTypes.Special_Ore:
                    baseSystem.GainOreToExtendTime(this);
                    break;
                case Global.OresTypes.FinalKey:
                    this.oresAmount[oresTypes] += oreAmount;
                    Debug.Log((int)oresTypes + ", " + oreAmount);
                    //CheckWeightAmount();
                    break;
            }
                
        }

    }
    private void CheckWeightAmount()
    {
        float tempWeight = 0.0f;
        for (int i = 0; i < (int)Global.OresTypes.Length; i++)
        {
            tempWeight += oresAmount[(Global.OresTypes)i] * Global.OresWeight[i];
        }

        currentWeight = tempWeight;
    }
    #endregion |* End Of Weight fuctions *|


    public float GetShipOresAmount(Global.OresTypes oresTypes) { return oresAmount[oresTypes]; }

}
