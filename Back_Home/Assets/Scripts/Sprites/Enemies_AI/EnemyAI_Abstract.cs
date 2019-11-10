using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAI_Abstract : MonoBehaviour {

    protected enum PirateState { patrol, chase, evacuate, inspect };

    //protected delegate void BehaviourExecute();

    [SerializeField] protected float speed;
    [SerializeField] protected float evacSpeedMultiplier;
    [SerializeField] protected float chaseSpeedMultiplier;

    [SerializeField] protected float patrolRadius;
    [SerializeField] protected float playerDetectionRadius;

    [SerializeField] protected float evacuateDistance;
    [SerializeField] protected float visionDistance;

    [SerializeField] protected float inspectDuration;

    [SerializeField] protected bool debug;

    protected PirateState currentState;

    //protected BehaviourExecute behaviourExecute;

    protected Rigidbody pirateRigidbody;

    protected Vector3 playerPosition;
    protected Vector3 moveToPosition;
    protected Vector3 vibrationPosition;
    protected Vector3 patrolCenter;

    protected float inspectCountdown;

    protected Vector3 RandomRadiusPosition(Vector3 startPos, float distance = 0f) {

        float x = Random.Range(startPos.x - distance, startPos.x + distance);
        float z = Random.Range(startPos.y - distance, startPos.y + distance);

        return new Vector3(x, 0, z);

    }

    protected void Patrol() {

        if (Vector3.Distance(transform.position, patrolCenter) >= patrolRadius) {
            moveToPosition = patrolCenter;
            transform.LookAt(patrolCenter);
        }
        else if (pirateRigidbody.position == moveToPosition || moveToPosition == Vector3.zero) {
            moveToPosition = RandomRadiusPosition(patrolCenter, patrolRadius);
            transform.LookAt(moveToPosition);
        }

        pirateRigidbody.position = Vector3.MoveTowards(transform.position, moveToPosition, speed * Time.deltaTime);

    }

    protected void DetectPlayer() {

        Collider[] playerCollideCheck = Physics.OverlapSphere(transform.position, playerDetectionRadius, Global.layer_Player);

        if (playerCollideCheck.Length > 0) {

            currentState = PirateState.chase;

            moveToPosition = Vector3.zero;
            pirateRigidbody.angularVelocity = Vector3.zero;

            playerPosition = playerCollideCheck[0].gameObject.GetComponent<Transform>().position;

        }
        else if (playerPosition != Vector3.zero) {

            playerPosition = Vector3.zero;

        }
    }

    protected void ChasePlayer() {

        transform.LookAt(playerPosition);
        //pirateRigidbody.rotation = Quaternion.LookRotation(playerPosition - transform.position);

        pirateRigidbody.position = Vector3.MoveTowards(transform.position, playerPosition, speed * chaseSpeedMultiplier * Time.deltaTime);

    }

    protected void Evacuate() {

        if (pirateRigidbody.position == patrolCenter) {
            //Destroy(gameObject);
            currentState = PirateState.patrol;
            return;
        }

        pirateRigidbody.position = Vector3.MoveTowards(transform.position, patrolCenter, speed * evacSpeedMultiplier * Time.deltaTime);

    }

    protected bool MoveToInspect() {

        if(Vector3.Distance(transform.position, vibrationPosition) > visionDistance) {

            pirateRigidbody.position = Vector3.MoveTowards(transform.position, vibrationPosition, speed * Time.deltaTime);

            return false;

        }

        if (inspectCountdown <= 0f) { inspectCountdown = inspectDuration; }

        return true;

    }

    protected void Inspect() {

        inspectCountdown -= Time.deltaTime;

        if(inspectCountdown <= 0f) { currentState = PirateState.patrol; }

    }

    public void VibrationDetected(Vector3 vibrationPosition) {

        this.vibrationPosition = vibrationPosition;

        currentState = PirateState.inspect;

        transform.LookAt(vibrationPosition);

    }

    protected bool visionDetected() {

        if(Physics.Raycast(transform.position, transform.forward * visionDistance)) {
            return true;
        }

        return false;

    }

    protected void debugVision() {
        Debug.DrawRay(transform.position, transform.forward * visionDistance, Color.red);
    }

    abstract public void AttackPlayer();

}
