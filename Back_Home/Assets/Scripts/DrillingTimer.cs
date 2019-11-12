using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DrillingTimer : MonoBehaviour
{
    private float timeRemaining;
    private const float maxTimeDrill = 5f;
    private bool isHolding = false;
    public Slider drillSlider;

    private PlayerDrill playerDrill;
    private Asteroid asteroid;

    public void StartDrilling()
    {
        isHolding = true;
    }

    public void StopDrilling()
    {
        isHolding = false;
    }
    private void Update()
    {
        drillSlider.value = CalculateDrillTime();

        if (isHolding && playerDrill.isCollidedAsteroid)
        {
            timeRemaining = maxTimeDrill;
            playerDrill.isCollidedAsteroid = true;
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
