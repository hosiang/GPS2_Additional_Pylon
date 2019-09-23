using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickUp : MonoBehaviour
{
    [SerializeField] private Text pickUpText; // to show what to button to press when colliding with Ice

    private bool isPickedUp = false; // to check if it is picked up

    private void Start()
    {
        pickUpText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ice")
        {
            pickUpText.gameObject.SetActive(true);
            isPickedUp = true;
            if (isPickedUp && Input.GetKeyUp(KeyCode.F))
            {
                Destroy(other.gameObject);
            }
        }

        if (other.gameObject.tag == "Titanium")
        {
            pickUpText.gameObject.SetActive(true);
            isPickedUp = true;
            if (isPickedUp && Input.GetKeyUp(KeyCode.F))
            {
                Destroy(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == ("Ice"))
        {
            pickUpText.gameObject.SetActive(false);
            isPickedUp = false;
        }

        if (other.gameObject.tag == "Titanium")
        {
            pickUpText.gameObject.SetActive(false);
            isPickedUp = false;
        }
    }
}
