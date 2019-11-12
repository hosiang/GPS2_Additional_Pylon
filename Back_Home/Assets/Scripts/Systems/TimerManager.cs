using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    private float currentTime = 0.0f;
    [SerializeField] private float timeMinute = 7.0f;
    private float startTime = 0.0f;
    private float targetToExtendTime = 0.0f;
    private float timeExtendProgress = 0.0f;
    private bool isTimeEnd = false;

    private readonly float eachSpecialOreExtendTimeValue = 10.0f;

    public float CurrentTime { get { return currentTime; } }
    public float StartTime { get { return startTime; } }
    public bool IsTimeEnd { get { return isTimeEnd; } }

    public void Start()
    {
        currentTime = startTime = timeMinute * 60.0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            StopCoroutine("ExtendingTimeCoroutine");
            StartCoroutine("ExtendingTimeCoroutine");
        }
        TimerUpdate();
    }

    private void TimerUpdate()
    {
        if(currentTime > 0.0f)
        {
            currentTime -= Time.deltaTime;
        }

        if (currentTime < 0.0f)
        {
            currentTime = 0.0f;
            isTimeEnd = true;
        }

    }

    public void TimeExtend(Object requireObject)
    {
        if(requireObject.GetType().Name == nameof(BaseSystem))
        {
            StopCoroutine("ExtendingTimeCoroutine");
            StartCoroutine("ExtendingTimeCoroutine");
        }
        else
        {
            Debug.LogError("Alert! Have some Unauthorized class try to entry to invoking this functions!");
        }
        
    }

    private IEnumerator ExtendingTimeCoroutine()
    {
        float eachFrameExtendTimeValue = timeExtendProgress;
        targetToExtendTime += eachSpecialOreExtendTimeValue;
        while (true)
        {
            currentTime += 1.0f;
            timeExtendProgress += 1.0f;

            if (timeExtendProgress >= targetToExtendTime)
            {
                Debug.Log(currentTime);
                timeExtendProgress = 0.0f;
                targetToExtendTime = 0.0f;
                break;
            }

            yield return new WaitForSeconds(0.01f);
            
        }
        
    }
}
