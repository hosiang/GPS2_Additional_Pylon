using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickUp : MonoBehaviour
{
    [SerializeField] private Text pickUpText; // to show what to button to press when colliding with Ice

    private bool isPickedUp = false; // to check if it is picked up

    public bool IsPickedUp
    {
        get
        {
            return isPickedUp;
        }
    }

    private void Start()
    {
        pickUpText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pickUpText.gameObject.SetActive(true);
            isPickedUp = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pickUpText.gameObject.SetActive(false);
            isPickedUp = false;
        }

    }

    public void SetIsPickedUpToFalse()
    {
        pickUpText.gameObject.SetActive(false);
        isPickedUp = true;
    }

}
