using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

        !!! Abstract/interface class will be implemented soon !!!

*/

public class PirateAI : MonoBehaviour {

    private enum PirateState { patrol, chase, evacuate };

    [SerializeField] private float speed;
    [SerializeField] private float evacSpeedMultiplier;
    [SerializeField] private float chaseSpeedMultiplier;

    [SerializeField] private float shankForce;
    [SerializeField] private float shankDistance;
    [SerializeField] private float shankRecoveryTime;

    [SerializeField] private float patrolRadius;
    [SerializeField] private float playerDetectionRadius;

    [SerializeField] private float evacuateDistance;

    [SerializeField] private Vector3 patrolCenter;
    [SerializeField] private LayerMask playerLayers;

    PirateState currentState;

    private Rigidbody pirateRigidbody;

    private Vector3 playerPosition;
    private Vector3 moveToPosition;

    private float shankRecoveryCountdown;

    private void Awake() {

        shankRecoveryCountdown = 0f;

        playerPosition = Vector3.zero;

        pirateRigidbody = GetComponent<Rigidbody>();

    }

    // Start is called before the first frame update
    void Start() {

        currentState = PirateState.patrol;

        moveToPosition = RandomRadiusPosition(patrolCenter, patrolRadius);

        transform.LookAt(moveToPosition);

    }

    // Update is called once per frame
    void Update() {

        switch(currentState) {

            case PirateState.patrol:
                Patrol();
                break;

            case PirateState.chase:
                if (shankRecoveryCountdown <= 0) { ChasePlayer(); }
                else { ShankRecovery(); }
                break;

            case PirateState.evacuate:
                Evacuate();
                break;

        }

    }

    private void OnCollisionEnter(Collision collision) {

        if (collision.collider.CompareTag(Global.tagPlayer)) {

            //collision.collider.GetComponent<Player>().reduceOre(amount); //placeholder
            //collision.collider.GetComponent<Player>().reduceHealth(amount); //placeholder

            currentState = PirateState.evacuate;

        }

    }

    private void OnCollisionExit(Collision collision) {

        pirateRigidbody.velocity = Vector3.zero;
        pirateRigidbody.angularVelocity = Vector3.zero;

        transform.LookAt(patrolCenter);

    }

    private Vector3 RandomRadiusPosition(Vector3 startPos, float distance = 0f) {

        float x = Random.Range(startPos.x - distance, startPos.x + distance);
        float z = Random.Range(startPos.y - distance, startPos.y + distance);

        return new Vector3(x, 0, z);

    }

    private void Patrol() {

        if(Vector3.Distance(transform.position, patrolCenter) >= patrolRadius) {
            moveToPosition = patrolCenter;
            transform.LookAt(patrolCenter);
        }
        else if(pirateRigidbody.position == moveToPosition) {
            moveToPosition = RandomRadiusPosition(patrolCenter, patrolRadius);
            transform.LookAt(moveToPosition);
        }

        pirateRigidbody.position = Vector3.MoveTowards(transform.position, moveToPosition, speed * Time.deltaTime);

        DetectPlayer();

    }

    private void DetectPlayer() {

        Collider[] playerCollideCheck = Physics.OverlapSphere(transform.position, playerDetectionRadius, Global.playerLayer);

        if (playerCollideCheck.Length > 0) {

            currentState = PirateState.chase;

            moveToPosition = Vector3.zero;
            pirateRigidbody.angularVelocity = Vector3.zero;

            playerPosition = playerCollideCheck[0].gameObject.GetComponent<Transform>().position;

        }
        else if(playerPosition != Vector3.zero) {

            playerPosition = Vector3.zero; 

        }

    }

    private void ChasePlayer() {

        DetectPlayer();

        transform.LookAt(playerPosition);
        //pirateRigidbody.rotation = Quaternion.LookRotation(playerPosition - transform.position);

        float playerDistance = Vector3.Distance(transform.position, playerPosition);

        if (playerDistance >= playerDetectionRadius) { currentState = PirateState.patrol; }
        else if(playerDistance <= shankDistance) { AttackPlayer(); }

        pirateRigidbody.position = Vector3.MoveTowards(transform.position, playerPosition, speed * chaseSpeedMultiplier * Time.deltaTime);

    }

    private void AttackPlayer() {

        if (Vector3.Distance(transform.position, playerPosition) <= shankDistance) {

            pirateRigidbody.AddForce((playerPosition - transform.position) * shankForce);

            shankRecoveryCountdown = shankRecoveryTime;

        }

    }

    private void ShankRecovery() {

        if (shankRecoveryCountdown >= 0) {

            shankRecoveryCountdown -= Time.deltaTime;

            if (shankRecoveryCountdown <= 0.1 * shankRecoveryTime) {
                pirateRigidbody.velocity = Vector3.zero;
                pirateRigidbody.angularVelocity = Vector3.zero;
            }

        }

    }

    private void Evacuate() {

        if(pirateRigidbody.position == patrolCenter) {
            //Destroy(gameObject);
            currentState = PirateState.patrol;
        }

        pirateRigidbody.position = Vector3.MoveTowards(transform.position, patrolCenter, speed * evacSpeedMultiplier * Time.deltaTime);

    }

}
