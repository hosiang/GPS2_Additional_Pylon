using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitaniumPickUp : MonoBehaviour
{
    [SerializeField] private Text pickUpText; // to show what to button to press when colliding with Titanium

    private bool isPickedUp = false; // to check if it is picked up

    private void Start()
    {
        pickUpText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isPickedUp && Input.GetKeyUp(KeyCode.F))
        {
            MineTitanium();
        }
    }

    private void MineTitanium()
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
