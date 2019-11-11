using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Asteroid : MonoBehaviour
{
    private float[] AstroidOreProvide = { 3, 5, 4 };
    
    [SerializeField] private Global.AstroidType astroidType;

    [SerializeField] private GameObject ore1;
    [SerializeField] private GameObject ore2;
    [SerializeField] private GameObject ore3;
    [SerializeField] private GameObject ore4;
    [SerializeField] private GameObject ore5;
    [SerializeField] private GameObject ore6;
    [SerializeField] private GameObject ore7;
    [SerializeField] private GameObject ore8;

    [SerializeField] private float health;
    [SerializeField] private float oreScatterRaius;
    //[SerializeField] private float vibrationFrequency;
    [SerializeField] private float vibrationDissipateRate;

    //[SerializeField] private PirateAI_Shank parasiteToSpawn;

    private float vibrationDistance;
    private float vibrationFrequency;
    private float vibrationChangeCounter;

    [SerializeField] private Animator specialOresAnimator;

    public GameObject boomEffect;
    private bool isBoom = false;

    private void Awake()
    {

        vibrationChangeCounter = 0f;

        switch (astroidType)
        {

            case Global.AstroidType.small:
                //transform.localScale = Vector3(); //placeholder
                break;

            case Global.AstroidType.big:
                //transform.localScale = Vector3(); //placeholder
                break;

        }

    }

    private void Start()
    {
        specialOresAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (vibrationFrequency > 0f) { VibrationExpand(); }
        else if (vibrationDistance > 0f) { VibrationDissipate(); }

        AlertEnemy();

        if (health <= 0f)
        {
            ExplosionEffect();
            ConvertToOre();
            Explode();
        }

    }

    private void ConvertToOre()
    {
        switch (astroidType)
        {
            case Global.AstroidType.small:
                FindObjectOfType<ShipEntity>().GainOresFromAsteroid(this, Global.OresTypes.Ore_No1, AstroidOreProvide[(int)Global.AstroidType.small]);
                break;
            case Global.AstroidType.big:
                FindObjectOfType<ShipEntity>().GainOresFromAsteroid(this, Global.OresTypes.Ore_No1, AstroidOreProvide[(int)Global.AstroidType.big]);
                break;
            case Global.AstroidType.special:
                FindObjectOfType<ShipEntity>().GainOresFromAsteroid(this, Global.OresTypes.Special_Ore, AstroidOreProvide[(int)Global.AstroidType.special]);
                specialOresAnimator.SetBool("isDestroyed", true);
                break;
        }

        Destroy(gameObject);
    }

    private void VibrationDissipate()
    {

        if (vibrationChangeCounter < 1f)
        {
            vibrationChangeCounter += Time.deltaTime;
            return;
        }

        vibrationDistance -= vibrationDissipateRate;

        if (vibrationDistance < 0f) { vibrationDistance = 0f; }

        vibrationChangeCounter = 0f;

    }

    private void VibrationExpand()
    {

        if (vibrationChangeCounter < 1f)
        {
            vibrationChangeCounter += Time.deltaTime;
            return;
        }

        vibrationDistance += vibrationFrequency;

        vibrationFrequency = 0f;

        vibrationChangeCounter = 0f;

    }

    
    private void Explode()
    {

        //Quickly made

        int oresToSpawn = 0;

        switch (astroidType)
        {

            case Global.AstroidType.small:
                oresToSpawn = Random.Range(1,4);
                break;

            case Global.AstroidType.big:
                oresToSpawn = Random.Range(4, 8);
                break;

        }

        for (int i = 0; i < oresToSpawn; ++i)
        {

            Vector3 spawnPosition = new Vector3(Random.Range(transform.position.x + 1, transform.position.x + oreScatterRaius), 0, Random.Range(transform.position.z + 1, transform.position.z + oreScatterRaius));

            int randomTypeAsteroid = Random.Range(1,9);

            switch(randomTypeAsteroid)
            {
                case 1:
                    Instantiate(ore1, spawnPosition, transform.rotation);
                    break;
                case 2:
                    Instantiate(ore2, spawnPosition, transform.rotation);
                    break;
                case 3:
                    Instantiate(ore3, spawnPosition, transform.rotation);
                    break;
                case 4:
                    Instantiate(ore4, spawnPosition, transform.rotation);
                    break;
                case 5:
                    Instantiate(ore5, spawnPosition, transform.rotation);
                    break;
                case 6:
                    Instantiate(ore6, spawnPosition, transform.rotation);
                    break;
                case 7:
                    Instantiate(ore7, spawnPosition, transform.rotation);
                    break;
                case 8:
                    Instantiate(ore8, spawnPosition, transform.rotation);
                    break;

            }

        }

        //Instantiate(parasiteToSpawn);

        Destroy(gameObject);

    }

    private void AlertEnemy()
    {

        Collider[] enemyCollideCheck = Physics.OverlapSphere(transform.position, vibrationDistance, Global.layer_Enemy);

        if (enemyCollideCheck.Length > 0)
        {

            for (int i = 0; i < enemyCollideCheck.Length; ++i)
            {

                enemyCollideCheck[i].GetComponentInParent<EnemyAI_Abstract>().VibrationDetected(transform.position);

            }

        }

    }

    public void Drill(float damage, float vibrationFrequency)
    {

        if (vibrationChangeCounter == 0f)
        {
            this.vibrationFrequency = vibrationFrequency;
            health -= damage;
        }

    }

    private void ExplosionEffect()
    {
        Instantiate(boomEffect, transform.position, transform.rotation);
    }

    public Global.AstroidType GetAstroidType()
    {
        return astroidType;
    }

}
