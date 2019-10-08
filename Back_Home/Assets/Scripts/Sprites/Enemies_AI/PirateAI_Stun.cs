using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateAI_Stun : PirateAI_Abstract {

    [SerializeField] private float stunRadius;
    [SerializeField] private float stunRecoveryTime;

    private float stunRecoveryCountdown;

    private void Awake() {

        stunRecoveryCountdown = 0f;

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

        switch (currentState) {

            case PirateState.patrol:

                Patrol();

                if (stunRecoveryCountdown <= 0f) { DetectPlayer(); }

                break;

            case PirateState.chase:

                DetectPlayer();
                ChasePlayer();

                float playerDistance = Vector3.Distance(transform.position, playerPosition);

                if (playerDistance >= playerDetectionRadius) { currentState = PirateState.patrol; }
                else if (playerDistance <= stunRadius) { AttackPlayer(); }

                break;

            case PirateState.evacuate:

                Evacuate();

                break;

        }

        StunRecovery();

    }

    public override void AttackPlayer() {

        if (stunRecoveryCountdown <= 0f) {

            Collider[] playerCollideCheck = Physics.OverlapSphere(transform.position, stunRadius, Global.layer_Player);

            //playerCollideCheck[0].gameObject.GetComponent<Player>().stun();

            stunRecoveryCountdown = stunRecoveryTime;

            currentState = PirateState.evacuate;

            transform.LookAt(patrolCenter);

        }

    }

    private void StunRecovery() {

        if (stunRecoveryCountdown > 0f) {

            stunRecoveryCountdown -= Time.deltaTime;

        }

    }

}
