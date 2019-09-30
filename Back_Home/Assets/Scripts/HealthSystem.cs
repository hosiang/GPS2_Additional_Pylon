using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth = 100f;
    [SerializeField] private float healthRegenerationRate = 1f; // Take from BaseSystem
    [SerializeField] private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        HealthChecker();
    }

    // Update is called once per frame
    void Update()
    {
        HealthChecker();
    }
    // Enter the Base to regenerate
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Base")) // Check name of Base tag
        {
            if (currentHealth < maxHealth)
            {
                currentHealth += healthRegenerationRate;
            }
            else if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }
    }

    void HealthChecker()
    {
        if (currentHealth <= 0)
        {
            isDead = true;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }
}