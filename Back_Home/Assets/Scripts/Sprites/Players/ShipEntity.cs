using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipEntity : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] protected float currentHealth = 100f;
    [SerializeField] protected float maximalHealth = 100f;

    [SerializeField] private float healthRegenerationRate = 1f; // Take from BaseSystem

    [SerializeField] protected bool isDead = false;

    public float CurrentHealth { get { return currentHealth; } }
    public float MaximalHealth { get { return maximalHealth; } }
    public bool IsDead { get { return isDead; } }


    [Header("Nitro")]
    [SerializeField] protected float currentNitro = 100f;
    [SerializeField] protected float maximalNitro = 100f;

    [SerializeField] private float nitroRegenerationRate = 33f;
    [SerializeField] private float nitroRefuelRate = 27f; // Use in Base, Get from BaseSystem

    [SerializeField] protected bool isOverheat = false;

    public float CurrentNitro { get { return currentNitro; } }
    public float MaximalNitro { get { return maximalNitro; } }
    public bool IsOverheat { get { return isOverheat; } }


    [Header("Weight")]
    [SerializeField] protected float currentWeight = 0f;
    [SerializeField] protected float maximalWeight = 100f;

    public float CurrentWeight { get { return currentWeight; } }
    public float MaximalWeight { get { return maximalWeight; } }


    protected Dictionary<Global.OresTypes, float> oresAmount = new Dictionary<Global.OresTypes, float>();

    private BaseSystem baseSystem;

    //Particle
    [SerializeField] private ParticleSystem astroidHitParticle;
    [SerializeField] private float astroidCollisionSpeed;

    void Start()
    {
        for(int i = 0; i < (int)Global.OresTypes.Length; i++)
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
                currentHealth += healthRegenerationRate * Time.deltaTime; // Smooth the regeneration speed.
            }
            else if (currentHealth > maximalHealth) // This only will trigger one time when |*|currentHealth|*| overstep the |*|maximalHealth|*|
            {
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
        if (requireObject == baseSystem)
        {
            if (currentNitro < maximalNitro)
            {
                currentNitro += nitroRegenerationRate * Time.deltaTime;
            }
            // If isOverheat is true and |*|currentNitro|*| is more then or equal to |*|maximalNitro|*|, then it will allow player able to thrust again
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
        else
        {
            Debug.LogError("Alert! Have some Unauthorized class try to entry to invoking this functions!");
        }
    }
    public void NitroReduction()
    {
        currentNitro -= nitroRefuelRate * Time.deltaTime;
    }
    #endregion |* End Of Nitro fuctions *|


    #region |* Weight fuctions *|
    public Dictionary<Global.OresTypes, float> UnloadResources(Object requireObject, Dictionary<Global.OresTypes, float> baseOresAmount)
    {
        if (requireObject == baseSystem)
        {
            for (int i = 0; i < (int)Global.OresTypes.Length; i++)
            {
                baseOresAmount[(Global.OresTypes)i] += oresAmount[(Global.OresTypes)i];
                oresAmount[(Global.OresTypes)i] = 0.0f;
            }
            currentWeight = 0.0f;

            return baseOresAmount;
        }
        else
        {
            Debug.LogError("Alert! Have some Unauthorized class try to entry to invoking this functions!");
            return null;
        }
    }

    public void GainOresFromAsteroid(Object requireObject, Global.OresTypes oresTypes, float oreAmount = 1.0f)
    {
        if (requireObject.GetType().Name == nameof(Asteroid))
        {
            if (oresTypes == Global.OresTypes.Special_Ore)
            {
                // time system
            }
            else
            {
                oresAmount[oresTypes] += oreAmount;
                CheckWeightAmount();
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
