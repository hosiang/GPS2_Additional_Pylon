using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class CustomButton : Selectable
{
    
    [SerializeField] private UnityEvent onClick;
    [SerializeField] private UnityEvent onPressing;
    [SerializeField] private UnityEvent onRelease;

    [Header("Double Click")]
    [SerializeField] private bool multiClick = false;
    [SerializeField] private float multiClickInterval = 0.5f;
    [SerializeField] private UnityEvent onMultiClick;
    [SerializeField, Range(2, 10)] private int multiClickAmount = 2;
    private int clicks = 0;

    private bool buttonOnPressing = false;
    public bool ButtonOnPressing { get{ return buttonOnPressing; } }

    protected override void Awake()
    {
        //if (onPressing == null) onPressing = new UnityEvent();
        base.Awake();
    }

    private void SetButtonDoubleClickActive(bool active)
    {
        multiClick = active;
    }

    private void DoubleClickCheck()
    {
        if (clicks < (multiClickAmount - 1))
        {
            clicks += 1;

            StopCoroutine("SecondClickDetect");
            StartCoroutine("SecondClickDetect");
        }
        else
        {
            clicks = 0;
            StopCoroutine("SecondClickDetect");

            onMultiClick.Invoke();
            Debug.Log("Double Click");
        }
        
        
    }

    private IEnumerator SecondClickDetect()
    {
        float time = 0.0f;

        while (true)
        {
            time += Time.deltaTime;

            if (time >= multiClickInterval)
            {
                clicks = 0;
            }

            yield return null;
        }

    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (multiClick) DoubleClickCheck();

        if (interactable)
        {
            buttonOnPressing = true;

            onClick.Invoke();

            StopCoroutine("StartOnPressingFuctions");
            StartCoroutine("StartOnPressingFuctions");
        }

        base.OnPointerDown(eventData);
    }
    
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (buttonOnPressing == true)
        {
            buttonOnPressing = false;
            onRelease.Invoke();
        }
        base.OnPointerUp(eventData);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        //base.OnPointerEnter(eventData);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (buttonOnPressing == true)
        {
            EventSystem.current.SetSelectedGameObject(null);
            DoStateTransition(SelectionState.Normal, true);

            buttonOnPressing = false;
            onRelease.Invoke();
        }

        //base.OnPointerExit(eventData);
    }

    private IEnumerator StartOnPressingFuctions()
    {
        while (buttonOnPressing)
        {
            if (!interactable)
            {
                buttonOnPressing = false;
                onRelease.Invoke();
            }
            onPressing.Invoke();
            yield return null;
            
        }
    }

}
