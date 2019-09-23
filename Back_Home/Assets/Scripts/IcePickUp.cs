using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IcePickUp : MonoBehaviour
{
    [SerializeField] private Text pickUpText; // to show what to button to press when colliding with Ice

    private bool isPickedUp = false; // to check if it is picked up

    private void Start()
    {
        pickUpText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isPickedUp && Input.GetKeyUp(KeyCode.F))
        { 
            MineIce();
        }
    }

    private void MineIce()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Player"))
        {
            pickUpText.gameObject.SetActive(true);
            isPickedUp = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Equals("Player"))
        {
            pickUpText.gameObject.SetActive(false);
            isPickedUp = false;
        }
    }
}
