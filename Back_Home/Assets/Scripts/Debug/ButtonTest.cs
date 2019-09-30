using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonTest : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler
{
    public Text buttonStateText;
    private bool buttonState;

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonStateText.text = "State : Pressed";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonStateText.text = "State : None";
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonStateText.text = "State : None";
    }
}
