using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private float[] AstroidOreProvide = { 3, 5, 4 };
    private enum AstroidType { small, big, special };

    [SerializeField] private AstroidType astroidType;

    [SerializeField] private GameObject ore;

    [SerializeField] private float health;
    [SerializeField] private float oreScatterRaius;
    [SerializeField] private float vibrationDissipateRate;

    [SerializeField] private PirateAI_Shank parasiteToSpawn;

    private float vibrationDistance;
    private float vibrationFrequency;
    private float vibrationChangeCounter;

    private void Awake()
    {

        vibrationChangeCounter = 0f;

        switch (astroidType)
        {

            case AstroidType.small:
                //transform.localScale = Vector3(); //placeholder
                break;

            case AstroidType.big:
                //transform.localScale = Vector3(); //placeholder
                break;

        }

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
            //Explode();
        }

    }

    private void ConvertToOre()
    {
        switch (astroidType)
        {
            case AstroidType.small:
                FindObjectOfType<ShipEntity>().GainOresFromAsteroid(this, Global.OresTypes.Iron, AstroidOreProvide[(int)AstroidType.small]);
                break;
            case AstroidType.big:
                FindObjectOfType<ShipEntity>().GainOresFromAsteroid(this, Global.OresTypes.Iron, AstroidOreProvide[(int)AstroidType.big]);
                break;
            case AstroidType.special:
                FindObjectOfType<ShipEntity>().GainOresFromAsteroid(this, Global.OresTypes.no2_Ores, AstroidOreProvide[(int)AstroidType.special]);
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

    /*
    private void Explode()
    {

        //Quickly made

        int oresToSpawn = 0;

        switch (astroidSize)
        {

            case AstroidSize.small:
                oresToSpawn = 3;
                break;

            case AstroidSize.big:
                oresToSpawn = 6;
                break;

        }

        for (int i = 0; i < oresToSpawn; ++i)
        {

            Vector3 spawnPosition = new Vector3(Random.Range(transform.position.x + 1, transform.position.x + oreScatterRaius), 0, Random.Range(transform.position.z + 1, transform.position.z + oreScatterRaius));

            Instantiate(ore, spawnPosition, transform.rotation);

        }

        Instantiate(parasiteToSpawn);

        Destroy(gameObject);

    }
    */

    private void AlertEnemy()
    {

        Collider[] enemyCollideCheck = Physics.OverlapSphere(transform.position, vibrationDistance, Global.layer_Enemy);

        if (enemyCollideCheck.Length > 0)
        {

            for (int i = 0; i < enemyCollideCheck.Length; ++i)
            {

                enemyCollideCheck[i].GetComponent<PirateAI_Abstract>().VibrationDetected(transform.position);

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

}
