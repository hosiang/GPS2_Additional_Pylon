using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerPickUpCounter : MonoBehaviour
{
    [SerializeField] private Text iceCounterText;
    [SerializeField] private Text titaniumCounterText;

    [SerializeField] private float iceAmount; // increment of Ice
    [SerializeField] private float titaniumAmount; // increment of titanium

    /// <summary>
    /// # Important! Don't simply use this function, this only for Debugging!!!
    /// </summary>
    public float Debug_IceAmount
    {
        get
        {
            return iceAmount;
        }
        set
        {
            iceAmount = value;
            iceCounterText.text = "ICE : " + iceAmount.ToString();
        }
    }

    public float IceAmount
    {
        get
        {
            return iceAmount;
        }
    }

    public float TitaniumAmount
    {
        get
        {
            return titaniumAmount;
        }
    }

    private void Start()
    {
        iceCounterText.text = "ICE : " + iceAmount.ToString();
        titaniumCounterText.text = "TITANIUM : " + titaniumAmount.ToString();
    }

    private void OnTriggerStay(Collider collision)
    {
        // && collision.GetComponent<ItemPickUp>().IsPickedUp.Equals(true)
        if (Input.GetKeyUp(KeyCode.F))
        {

            switch (collision.tag)
            {
                case "Ice":
                    //collision.GetComponent<ItemPickUp>().SetIsPickedUpToFalse();
                    collision.gameObject.SetActive(false);

                    iceAmount += 1;
                    iceCounterText.text = "ICE : " + iceAmount.ToString();
                    break;

                case "Titanium":
                    //collision.GetComponent<ItemPickUp>().SetIsPickedUpToFalse();
                    collision.gameObject.SetActive(false);

                    titaniumAmount += 1;
                    titaniumCounterText.text = "TITANIUM : " + titaniumAmount.ToString();
                    break;
            }

        }

    }

    public void ReduceIceAmount(float iceReduceRate)
    {
        iceAmount -= iceReduceRate;
        iceCounterText.text = "ICE : " + iceAmount.ToString();
    }

}
