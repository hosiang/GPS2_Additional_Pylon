using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateAI_Shank : PirateAI_Abstract
{

    [SerializeField] private float shankForce;
    [SerializeField] private float shankDistance;
    [SerializeField] private float shankRecoveryTime;

    private float shankRecoveryCountdown;

    private void Awake()
    {

        inspectCountdown = 0f;
        shankRecoveryCountdown = 0f;

        playerPosition = Vector3.zero;

        pirateRigidbody = GetComponent<Rigidbody>();

    }

    // Start is called before the first frame update
    void Start()
    {

        currentState = PirateState.patrol;

        moveToPosition = RandomRadiusPosition(patrolCenter, patrolRadius);

        transform.LookAt(moveToPosition);

    }

    // Update is called once per frame
    void Update()
    {

        switch (currentState)
        {

            case PirateState.patrol:

                Patrol();
                DetectPlayer();

                break;

            case PirateState.chase:

                if (shankRecoveryCountdown <= 0)
                {

                    DetectPlayer();
                    ChasePlayer();

                    float playerDistance = Vector3.Distance(transform.position, playerPosition);

                    if (playerDistance >= playerDetectionRadius) { currentState = PirateState.patrol; }
                    else if (playerDistance <= shankDistance) { AttackPlayer(); }

                }
                else
                {

                    ShankRecovery();

                }

                break;

            case PirateState.evacuate:

                Evacuate();

                break;

            case PirateState.inspect:

                if (MoveToInspect())
                {
                    Inspect();
                    DetectPlayer();
                }

                break;

        }

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.CompareTag(Global.tag_Player))
        {

            //collision.collider.GetComponent<Player>().reduceOre(amount); //placeholder
            //collision.collider.GetComponent<Player>().reduceHealth(amount); //placeholder

            currentState = PirateState.evacuate;

        }

    }

    private void OnCollisionExit(Collision collision)
    {

        pirateRigidbody.velocity = Vector3.zero;
        pirateRigidbody.angularVelocity = Vector3.zero;

        transform.LookAt(patrolCenter);

    }

    public override void AttackPlayer()
    {

        pirateRigidbody.AddForce((playerPosition - transform.position) * shankForce);

        shankRecoveryCountdown = shankRecoveryTime;

    }

    private void ShankRecovery()
    {

        shankRecoveryCountdown -= Time.deltaTime;

        if (shankRecoveryCountdown <= 0.1 * shankRecoveryTime)
        {

            pirateRigidbody.velocity = Vector3.zero;
            pirateRigidbody.angularVelocity = Vector3.zero;

        }

    }

}
