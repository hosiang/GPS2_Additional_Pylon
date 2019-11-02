using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DrillingTimer : MonoBehaviour
{
    private float timeRemaining;
    private const float maxTimeDrill = 4f;
    private bool isHolding = false;
    public Slider drillSlider;

    public void StartDrilling()
    {
        isHolding = false;
    }

    public void StopDrilling()
    {
        isHolding = true;
    }
    private void Update()
    {
        drillSlider.value = CalculateDrillTime();

        if (isHolding)
        {
            timeRemaining = maxTimeDrill;
        }
        else
        {
            isHolding = false;
        }

        if (timeRemaining <= 0)
        {
            timeRemaining = 0f;
        }
        else if (timeRemaining >= 0)
        {
            timeRemaining -= Time.deltaTime;
        }
    }

    private float CalculateDrillTime()
    {
        return (timeRemaining / maxTimeDrill);
    }
}
