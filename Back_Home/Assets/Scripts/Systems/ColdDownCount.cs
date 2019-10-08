using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class ColdDownCount
{
    private float timeCount;
    [SerializeField] private float coldDownTime;
    private float defalutTime;

    private bool isTiming;

    /// <summary>
    /// Initialise The Timer and set a number for cold down time.
    /// </summary>
    /// <param name="coldDown">The maximal number of the cold down timer.</param>
    public ColdDownCount(float coldDown)
    {
        Mathf.Abs(coldDown);
        defalutTime = 0.0f;
        coldDownTime = coldDown;
    }

    /// <summary>
    /// Initialise The Timer and set two number for start time and end time.
    /// </summary>
    /// <param name="startTime">The minimal number of the timer.</param>
    /// <param name="endTime">The maximal number of the timer.</param>
    public ColdDownCount(float startTime, float endTime)
    {
        defalutTime = startTime > endTime ? endTime : startTime;
        coldDownTime = startTime > endTime ? startTime : endTime;
    }

    /// <summary>
    /// Do checking while the timer is on counting, and it will return the timer status
    /// </summary>
    /// <returns>true = Time Up, false = Counting</returns>
    public bool CountingAndCheck()
    {
        if(!(isTiming = timeCount >= coldDownTime))
        {
            timeCount += 1.0f * Time.deltaTime;
        }
        return isTiming;
    }

    /// <summary>
    /// Do counting and set the timer status to isTimming boolean.
    /// </summary>
    public void Counting()
    {
        if (!(isTiming = timeCount >= coldDownTime))
        {
            timeCount += 1.0f * Time.deltaTime;
        }

    }

    /// <summary>
    /// Checking the current timer value is minimal number or not?
    /// If the current timer value is minimal number then return true, else return false.
    /// </summary>
    /// <returns>true = current timer value is minimal number, flase = current timer value is not minimal number</returns>
    public bool IsTimerValueIsDefault()
    {
        return timeCount == defalutTime;
    }

    /// <summary>
    /// Reset the current timer value to minimal number.
    /// </summary>
    public void ResetTimer()
    {
        timeCount = defalutTime;
    }

    /// <summary>
    /// Getting the timer status.
    /// If the timer is time up now then will return true, else will return false mean the timer still on counting progrss.
    /// </summary>
    /// <returns>true = time up, false = counting</returns>
    public bool GetIsTiming()
    {
        return isTiming;
    }

}
