using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [Header("Health Point")]
    [SerializeField] protected float currentHealth = 100f;
    [SerializeField] protected float maximalHealth = 100f;

    [SerializeField] private float healthRegenerationRate = 1f; // Take from BaseSystem

    [SerializeField] protected bool isDead = false;

    public float CurrentHealth { get { return currentHealth; } }
    public float MaximalHealth { get { return maximalHealth; } }
    public bool IsDead { get { return isDead; } }

    protected void Start_HealthSystem() // This will run on ShipEntity's Start() function
    {
        HealthChecker();
    }

    protected void Update_HealthSystem() // This will run on ShipEntity's Update() function
    {
        
    }

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

    protected void RegenerationHealth()
    {
        if(currentHealth < maximalHealth)
        {
            currentHealth += healthRegenerationRate * Time.deltaTime; // Smooth the regeneration speed.
        }
        else if (currentHealth > maximalHealth) // This only will trigger one time when |*|currentHealth|*| overstep the |*|maximalHealth|*|
        {
            currentHealth = maximalHealth; // For limit the overstep |*|currentHealth|*| to |*|maximalHealth|*|
        }
    }
}
