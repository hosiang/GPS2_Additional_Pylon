using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPickUpCounter : MonoBehaviour
{
    [SerializeField] private Text iceCounterText; 
    [SerializeField] private Text titaniumCounterText; 

    private int iceAmount; // increment of Ice
    private int titaniumAmount; // increment of titanium

    private void Update()
    {
        iceCounterText.text = "ICE : " + iceAmount.ToString();
        titaniumCounterText.text = "TITANIUM : " + titaniumAmount.ToString();
    }

    private void OnTriggerStay(Collider collision)
    {

        if (Input.GetKeyUp(KeyCode.F) && collision.GetComponent<ItemPickUp>().IsPickedUp.Equals(true))
        {

            switch (collision.tag)
            {
                case "Ice":
                    collision.GetComponent<ItemPickUp>().SetIsPickedUpToFalse();
                    collision.gameObject.SetActive(false);

                    iceAmount += 1;
                    break;

                case "Titanium":
                    collision.GetComponent<ItemPickUp>().SetIsPickedUpToFalse();
                    collision.gameObject.SetActive(false);

                    titaniumAmount += 1;
                    break;
            }

        }

    }
}
