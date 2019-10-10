using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour {

    private enum AstroidType { type1, type2 };
    private enum AstroidSize { small, big };

    [SerializeField] private float health;
    [SerializeField] private float vibrationDissipateRate;

    [SerializeField] private AstroidType astroidType;
    [SerializeField] private AstroidSize astroidSize;

    [SerializeField] private PirateAI_Shank parasiteToSpawn;

    private float vibrationDistance;
    private float vibrationFrequency;
    private float vibrationChangeCounter;

    private void Awake() {

        vibrationChangeCounter = 0f;

        switch(astroidSize) {

            case AstroidSize.small:
                //transform.localScale = Vector3(); //placeholder
                break;

            case AstroidSize.big:
                //transform.localScale = Vector3(); //placeholder
                break;

        }

    }

    // Update is called once per frame
    void Update() {

        if(vibrationFrequency > 0f) { VibrationExpand(); }
        else if(vibrationDistance > 0f) { VibrationDissipate(); }
        
        AlertEnemy();

        if(health <= 0f) { Explode(); }

    }

    private void VibrationDissipate() {

        if (vibrationChangeCounter < 1f) {
            vibrationChangeCounter += Time.deltaTime;
            return;
        }

        vibrationDistance -= vibrationDissipateRate;

        if (vibrationDistance < 0f) { vibrationDistance = 0f; }

        vibrationChangeCounter = 0f;

    }

    private void VibrationExpand() {

        if(vibrationChangeCounter < 1f) {
            vibrationChangeCounter += Time.deltaTime;
            return;
        }

        vibrationDistance += vibrationFrequency;

        vibrationFrequency = 0f;

        vibrationChangeCounter = 0f;

    }

    private void Explode() {

        switch(astroidType) {

            case AstroidType.type1:
                //give player ore //placeholder
                break;

            case AstroidType.type2:
                //give player ore //placeholder
                break;

        }

        Instantiate(parasiteToSpawn);

        Destroy(gameObject);

    }

    private void AlertEnemy() {

        Collider[] enemyCollideCheck = Physics.OverlapSphere(transform.position, vibrationDistance, Global.layer_Player);

        if (enemyCollideCheck.Length > 0) {

            for (int i = 0; i < enemyCollideCheck.Length; ++i) {

                enemyCollideCheck[i].GetComponent<PirateAI_Abstract>().VibrationDetected(transform.position);

            }

        }

    }

    public void Drill(float damage, float vibrationFrequency) {

        if(vibrationChangeCounter == 0f) {
            this.vibrationFrequency = vibrationFrequency;
            health -= damage;
        }
        
    }

}
