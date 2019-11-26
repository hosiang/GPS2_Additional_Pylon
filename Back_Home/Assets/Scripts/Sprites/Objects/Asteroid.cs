using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Asteroid : MonoBehaviour
{
    //private float[] AstroidOreProvide = { 3, 5, 4 };

    private Global.ZoneLevels formWhichZone;
    [SerializeField] private Global.AstroidType astroidType;
    private Vector3 originalScale;

    [SerializeField] private List<GameObject> brokenOresTypes = new List<GameObject>();

    [SerializeField] private float health;
    [SerializeField] private float oreScatterRaius;
    //[SerializeField] private float vibrationFrequency;
    [SerializeField] private float vibrationDissipateRate;

    //[SerializeField] private PirateAI_Shank parasiteToSpawn;

    private float vibrationDistance;
    private float vibrationFrequency;
    private float vibrationChangeCounter;

    [SerializeField] private Animator specialOresAnimator;
    private Rigidbody playerRigidbody;
    private float knockBackForce = 200.0f;
    [SerializeField] private float knockBackDelayTime = 0.2f;
    private bool onKnockBack = false;

    public GameObject boomEffect;
    //private bool isBoom = false;
    private bool brokenByDoubleThrust = false;
    

    //private List<float> spawnOreAmount = new List<float> { 3, 5, 1, 2 };

    public Global.ZoneLevels FormWhichZone => formWhichZone;
    public Global.AstroidType AstroidType => astroidType;
    public bool OnKnockBack => onKnockBack;

    private void Awake()
    {
        vibrationChangeCounter = 0f;

        originalScale = transform.localScale;
    }

    private void Start()
    {
        specialOresAnimator = GetComponentInChildren<Animator>();
        playerRigidbody = FindObjectOfType<PlayerControl>().GetComponent<Rigidbody>();
    }

    void Update()
    {

        if (vibrationFrequency > 0f) { VibrationExpand(); }
        else if (vibrationDistance > 0f) { VibrationDissipate(); }

        AlertEnemy();
    }

    public void Drill(float damage, float vibrationFrequency)
    {
        if(astroidType == Global.AstroidType.Special)
        {
            if (!onKnockBack) // Not allow trigger again while it on knock back
            {
                onKnockBack = true;
                specialOresAnimator.SetTrigger(Global.animator_Trigger_SpecialAsteroid_isKnockBack);
                StartCoroutine("KnockBack");
            }
        }
        else // If the asteroid isn't SpecialAsteroid
        {
            if (vibrationChangeCounter == 0f)
            {
                this.vibrationFrequency = vibrationFrequency;
            }

            if (health > 0.0f)
            {
                health -= damage * Time.deltaTime;
                health = (health <= 0.0f) ? 0.0f : health;
            }

            if (health <= 0f)
            {
                ConvertToOre();
            }
        }

    }

    private IEnumerator KnockBack()
    {
        yield return new WaitForSeconds(knockBackDelayTime);

        onKnockBack = false;
        playerRigidbody.AddForce(-(transform.position - playerRigidbody.position).normalized * knockBackForce, ForceMode.Force);
    }

    #region | Vibration Functions |
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
    #endregion


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
                specialOresAnimator.SetTrigger(Global.animator_Trigger_SpecialAsteroid_isDestroyed); // Special Ore Animation
                break;
        }

    }

    private void Explode()
    {

        int oresToSpawn = Random.Range(Global.eachZoneAsteroidSpwanOreAmount[(int)formWhichZone, (int)astroidType, (int)Global.OresSpawn.Minimal],
        Global.eachZoneAsteroidSpwanOreAmount[(int)formWhichZone, (int)astroidType, (int)Global.OresSpawn.Maximal]); // Get the random number of each zone of ores will spawn

        Vector3 spawnPosition = Vector3.zero;
        float rotationY = 0.0f;
        int randomTypeAsteroid = 0;

        for (int i = 0; i < oresToSpawn; ++i)
        {
            spawnPosition.x = Random.Range(transform.position.x - oreScatterRaius, transform.position.x + oreScatterRaius);
            spawnPosition.y = 0.0f;
            spawnPosition.z = Random.Range(transform.position.z - oreScatterRaius, transform.position.z + oreScatterRaius);

            rotationY = Random.Range(0.0f, 360.0f);

            randomTypeAsteroid = Random.Range(0, brokenOresTypes.Count); // Get the random one type of the 8 different broken_asteroid

            GameObject tempGameObject = Instantiate(brokenOresTypes[randomTypeAsteroid], spawnPosition, Quaternion.Euler(0.0f, rotationY, 0.0f)); // Spawn the ore
            tempGameObject.GetComponent<Ores>().SetOresToColletable(this); // Only explode the ore when player using correct way the getting
        }

        //Instantiate(parasiteToSpawn);
        ExplosionEffect();
        Destroy(gameObject);

    }

    private void ExplosionEffect()
    {
        GameObject tempGameObject =  Instantiate(boomEffect, transform.position, transform.rotation);
        Destroy(tempGameObject, 3.0f); // Destroy the boom effect after 3 second
    }

    public void DoubleThrustBroken()
    {

    }

    #region | Asteroid Genarator Funtions |
    public void SetAsteroidSize(Object requestObject, Global.AstroidType astroidType, Global.ZoneLevels zoneLevels)
    {
        if(requestObject.GetType().Name == nameof(AsteroidGenerator) && (int)astroidType < 2)
        {
            this.astroidType = astroidType;
            formWhichZone = zoneLevels;
            SizeGenerator();
        }
        
    }

    private void SizeGenerator()
    {
        switch (astroidType)
        {
            case Global.AstroidType.AsteroidSmall:
                transform.localScale = originalScale / 3.0f;
                health = Global.eachAsteroidHealthPoint[(int)astroidType];
                break;
            case Global.AstroidType.AsteroidBig:
                transform.localScale = originalScale;
                health = Global.eachAsteroidHealthPoint[(int)astroidType];
                break;
            case Global.AstroidType.Special:
                transform.localScale = originalScale;
                health = Global.eachAsteroidHealthPoint[(int)astroidType];
                break;
        }
    }
    #endregion

}
