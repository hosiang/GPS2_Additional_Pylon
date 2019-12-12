using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalKey : MonoBehaviour
{
    private ShipEntity shipEntity;
    private Collider[] playerCollider;

    private Transform finalKeyTransform;
    private FinalKeyEvent finalKeyEvent;

    private LayerMask layerMask_player;

    [SerializeField] private float detectPlayerRadius = 2.0f;
    [SerializeField] private bool debug = false;

    // Start is called before the first frame update
    void Start()
    {
        layerMask_player = LayerMask.GetMask("Player");

        shipEntity = FindObjectOfType<ShipEntity>();
        finalKeyTransform = GetComponent<Transform>();
        finalKeyEvent = FindObjectOfType<FinalKeyEvent>();
    }

    private void Update()
    {
        playerCollider = Physics.OverlapSphere(finalKeyTransform.position, detectPlayerRadius, layerMask_player);
        if (playerCollider.Length > 0 )
        {
            finalKeyEvent.PlayerFoundFinalKey(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == Global.tag_Player)
        {
            shipEntity.GainOres(this, Global.OresTypes.FinalKey);
            finalKeyEvent.PlayerGetFinalKey(this);
            Debug.Log("Player Getting Final Key");
        }
    }

    private void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(finalKeyTransform.position, detectPlayerRadius);
        }
    }
}
