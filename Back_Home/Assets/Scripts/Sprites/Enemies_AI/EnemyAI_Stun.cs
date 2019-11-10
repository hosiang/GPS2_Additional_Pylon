using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_Stun : EnemyAI_Abstract {

    [SerializeField] Animator stunnerAnimator;

    [SerializeField] private float stunRadius;
    [SerializeField] private float stunRecoveryTime;

    [SerializeField] private GameObject stunBullet;
    [SerializeField] private ParticleSystem chargeStun;
    [SerializeField] private ParticleSystem boomStun;

    private float stunRecoveryCountdown;

    private void Awake() {


        inspectCountdown = 0f;
        stunRecoveryCountdown = 0f;

        playerPosition = Vector3.zero;

        pirateRigidbody = GetComponent<Rigidbody>();

    }

    // Start is called before the first frame update
    void Start() {

        chargeStun.Stop();
        boomStun.Stop();

        currentState = PirateState.patrol;

        //moveToPosition = RandomRadiusPosition(patrolCenter, patrolRadius);

        //transform.LookAt(moveToPosition);
    }

    // Update is called once per frame
    void Update() {

        switch (currentState) {

            case PirateState.patrol:

                if (stunRecoveryCountdown <= 0f) { LookAtPlayer(); }

                break;

            case PirateState.chase:

                float playerDistance = Vector3.Distance(transform.position, playerPosition);

                if (playerDistance >= playerDetectionRadius) { currentState = PirateState.patrol; }
                else if (playerDistance <= stunRadius) { AttackPlayer(); }

                break;

            case PirateState.evacuate:

                Evacuate();

                break;

            case PirateState.inspect:

                if (MoveToInspect()) { Inspect(); }

                break;

        }

        StunRecovery();

    }

    private void LookAtPlayer()
    {
        Collider[] playerCollideCheck = Physics.OverlapSphere(transform.position, stunRadius, Global.layer_Player);

        if (playerCollideCheck.Length > 0)
        {
            currentState = PirateState.chase;

            playerPosition = playerCollideCheck[0].gameObject.GetComponent<Transform>().position;

            stunnerAnimator.SetTrigger("startMoving");

            transform.LookAt(playerPosition);

            stunnerAnimator.SetTrigger("stopMoving");
        }
        else if (playerPosition != Vector3.zero)
        {
            playerPosition = Vector3.zero;
        }

    }

    public override void AttackPlayer() {

        if (stunRecoveryCountdown <= 0f) {

            Collider[] playerCollideCheck = Physics.OverlapSphere(transform.position, stunRadius, Global.layer_Player);

            //playerCollideCheck[0].gameObject.GetComponent<Player>().stun(); //placeholder

            stunRecoveryCountdown = stunRecoveryTime;

            currentState = PirateState.evacuate;

            StartCoroutine(Charge());
        }

    }

    private void StunRecovery() {

        if (stunRecoveryCountdown > 0f) {

            stunRecoveryCountdown -= Time.deltaTime;
        }

    }

    IEnumerator Charge()
    {
        stunnerAnimator.SetTrigger("isStunning");
        chargeStun.Play();
        boomStun.Play();

        yield return new WaitForSeconds(stunnerAnimator.GetCurrentAnimatorStateInfo(0).length);

        GameObject newBullet = Instantiate(stunBullet, transform.position, transform.rotation);
        newBullet.GetComponent<Enemy_StunBullet>().InitializeBullet(Vector3.forward, this.tag);

        yield break;
    }

}
