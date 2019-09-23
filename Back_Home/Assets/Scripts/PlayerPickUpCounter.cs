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

    private void OnTriggerEnter(Collider collision)
    {
        if ((Input.GetKeyUp(KeyCode.K)) && collision.GetComponent<IcePickUp>()) // i couldnt get the counter work properly even 
        {
            iceAmount += 1;                                                     // if i use without input keycode, could work but collide will 
                                                                                //  automatically increase the counter
        }
        if ((Input.GetKeyUp(KeyCode.K)) && collision.GetComponent<TitaniumPickUp>())
        {
            titaniumAmount += 1;
        }
    }
}
