using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Asteroid : MonoBehaviour
{
    //private float[] AstroidOreProvide = { 3, 5, 4 };
    
    [SerializeField] private Global.AstroidType astroidType;

    [SerializeField] private List<GameObject> brokenOresTypes = new List<GameObject>();
    /*
    [SerializeField] private GameObject ore1;
    [SerializeField] private GameObject ore2;
    [SerializeField] private GameObject ore3;
    [SerializeField] private GameObject ore4;
    [SerializeField] private GameObject ore5;
    [SerializeField] private GameObject ore6;
    [SerializeField] private GameObject ore7;
    [SerializeField] private GameObject ore8;
    */

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
    //private bool isBoom = false;
    private bool brokenByDoubleThrust = false;
    
    private List<float> spawnOreAmount = new List<float> { 3, 5, 1, 2 };
    private float[] eachAsteridSizeHealth = new float[]{ 30.0f, 50.0f };

    private void Awake()
    {

        vibrationChangeCounter = 0f;

        switch (astroidType)
        {

            case Global.AstroidType.AsteroidSmall:
                //transform.localScale = Vector3(); //placeholder
                break;

            case Global.AstroidType.AsteroidBig:
                //transform.localScale = Vector3(); //placeholder
                break;

        }

    }

    private void Start()
    {
        specialOresAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (vibrationFrequency > 0f) { VibrationExpand(); }
        else if (vibrationDistance > 0f) { VibrationDissipate(); }

        AlertEnemy();

        if (health <= 0f)
        {
            ConvertToOre();
        }

    }
    
    private void ConvertToOre()
    {
        switch (astroidType)
        {
            case Global.AstroidType.AsteroidSmall:
            case Global.AstroidType.AsteroidBig:
                Explode();
                Destroy(gameObject);
                break;
            case Global.AstroidType.Special:
                this.GetComponentInChildren<Ores>().SetOresToColletable(this); // Make sure the special ore is get from the correct way
                specialOresAnimator.SetBool("isDestroyed", true); // Special Ore Animation
                break;
        }

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

            case Global.AstroidType.AsteroidSmall:
                oresToSpawn = brokenByDoubleThrust ? 1 : 3;
                break;

            case Global.AstroidType.AsteroidBig:
                oresToSpawn = brokenByDoubleThrust ? 2 : 5;
                break;

        }

        Vector3 spawnPosition = Vector3.zero;
        int randomTypeAsteroid = 0;
        float rotationY = 0.0f;
        for (int i = 0; i < oresToSpawn; ++i)
        {
            spawnPosition.x = Random.Range(transform.position.x - oreScatterRaius, transform.position.x + oreScatterRaius);
            spawnPosition.y = 0.0f;
            spawnPosition.z = Random.Range(transform.position.z - oreScatterRaius, transform.position.z + oreScatterRaius);

            rotationY = Random.Range(0.0f, 360.0f);

            randomTypeAsteroid = Random.Range(0, brokenOresTypes.Count);

            GameObject tempGameObject = Instantiate(brokenOresTypes[randomTypeAsteroid], spawnPosition, Quaternion.Euler(0.0f, rotationY, 0.0f));
            tempGameObject.GetComponent<Ores>().SetOresToColletable(this); // Make sure the ore is explode in correct way
            /*
            switch (randomTypeAsteroid)
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
            */
        }

        //Instantiate(parasiteToSpawn);
        ExplosionEffect();
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
            
        }

        if(health > 0.0f)
        {
            health -= damage * Time.deltaTime;
            health = (health <= 0.0f) ? 0.0f : health; 
        }
        


    }

    private void ExplosionEffect()
    {
        GameObject tempGameObject =  Instantiate(boomEffect, transform.position, transform.rotation);
        Destroy(tempGameObject, 10.0f); // Destroy the boom effect after 10 second
    }

    public Global.AstroidType GetAstroidType()
    {
        return astroidType;
    }

    public void DoubleThrustBroken()
    {

    }

    public void SetAsteroidSize(Object requestObject, Global.AstroidType astroidType)
    {
        if(requestObject.GetType().Name == nameof(AsteroidGenerator) && (int)astroidType < 2)
        {
            this.astroidType = astroidType;
            health = eachAsteridSizeHealth[(int)astroidType];
        }
        
    }
}
