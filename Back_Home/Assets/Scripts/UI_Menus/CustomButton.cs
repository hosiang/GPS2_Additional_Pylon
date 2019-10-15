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

    private bool buttonOnPressing = false;
    public bool ButtonOnPressing { get{ return buttonOnPressing; } }

    protected override void Awake()
    {
        //if (onPressing == null) onPressing = new UnityEvent();
        base.Awake();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        buttonOnPressing = true;

        onClick.Invoke();

        StopCoroutine("StartOnPressingFuctions");
        StartCoroutine("StartOnPressingFuctions");

        base.OnPointerDown(eventData);
    }
    
    public override void OnPointerUp(PointerEventData eventData)
    {
        buttonOnPressing = false;
        onRelease.Invoke();

        base.OnPointerUp(eventData);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        buttonOnPressing = false;
        onRelease.Invoke();

        base.OnPointerExit(eventData);
    }

    private IEnumerator StartOnPressingFuctions()
    {
        while (buttonOnPressing)
        {
            onPressing.Invoke();
            yield return null;
        }
    }

}
