using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OresPickUpCounter : MonoBehaviour
{
    [SerializeField] private Text oresCounterText;

    private float oresAmount;

    private void Start()
    {
        oresCounterText.text = "ORES: " + oresAmount.ToString();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ores")
        {
            other.gameObject.SetActive(false);
            oresAmount += 1;
            oresCounterText.text = "ORES: " + oresAmount.ToString();
        }
    }
}
