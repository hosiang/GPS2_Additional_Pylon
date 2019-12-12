using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoystickTouchController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private string name_Joystick = "Joystick";

    private Image joystickContainer;
    private Image joystick;

    private Vector3 inputDirection = Vector3.zero;
    private Vector2 touchingPosition = Vector2.zero;
    private Vector2 touchingJoystickPosition = Vector2.zero;
    //private Vector2 touchingScreenJoystickPosition = Vector2.zero;

    public Vector3 InputDirection { get { return inputDirection; } }
    public Vector2 TouchingJoystickPosition { get { return touchingJoystickPosition; } }
    //public Vector2 TouchingScreenJoystickPosition { get { return touchingScreenJoystickPosition; } }

    /*
    public Rigidbody objectRigidbody;

    public Text axisXText;
    public Text axisYText;
    */
    void Start()
    {
        joystickContainer = GetComponent<Image>();
        joystick = transform.Find(name_Joystick).GetComponent<Image>(); //this command is used because there is only one child in hierarchy
    }

    private void Update()
    {
        /*
        objectRigidbody.velocity = new Vector3(inputDirection.x * 5f, 0.0f, inputDirection.y * 5f);

        axisXText.text = "Axis X : " + inputDirection.x;
        axisYText.text = "Axis Y : " + inputDirection.y;
        */
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        /*
        Vector2 touchPosition = Vector2.zero;
        
        //To get Input Direction
        RectTransformUtility.ScreenPointToLocalPointInRectangle (joystickContainer.rectTransform, pointerEventData.position, pointerEventData.pressEventCamera, out touchPosition);

        touchPosition.x = (touchPosition.x / joystickContainer.rectTransform.sizeDelta.x);
        touchPosition.y = (touchPosition.y / joystickContainer.rectTransform.sizeDelta.y);

        float x = touchPosition.x * 2;
        float y = touchPosition.y * 2;

        //float x = (joystickContainer.rectTransform.pivot.x == 1f) ? touchPosition.x * 2 + 1 : touchPosition.x * 2 - 1;
        //float y = (joystickContainer.rectTransform.pivot.y == 1f) ? touchPosition.y * 2 + 1 : touchPosition.y * 2 - 1;

        inputDirection = new Vector3(x, y, 0);

        inputDirection = (inputDirection.magnitude > 1) ? inputDirection.normalized : inputDirection;

        //to define the area in which joystick can move around
        joystick.rectTransform.anchoredPosition = new Vector3(inputDirection.x * (joystickContainer.rectTransform.sizeDelta.x / 3), inputDirection.y * (joystickContainer.rectTransform.sizeDelta.y) / 3);
        */

        RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickContainer.rectTransform, pointerEventData.position, pointerEventData.pressEventCamera, out touchingPosition);
        touchingJoystickPosition = touchingPosition;

        if (touchingPosition.magnitude > ((joystickContainer.rectTransform.sizeDelta.x / 2) - (joystick.rectTransform.sizeDelta.x / 2)))
        {
            inputDirection = touchingPosition.normalized;
            touchingPosition = touchingPosition.normalized * ((joystickContainer.rectTransform.sizeDelta / 2) - (joystick.rectTransform.sizeDelta / 2));
        }
        else
        {
            inputDirection = touchingPosition / ((joystickContainer.rectTransform.sizeDelta.x / 2) - (joystick.rectTransform.sizeDelta.x / 2));
        }

        //inputDirection = (touchPosition.magnitude > ((joystickContainer.rectTransform.sizeDelta.x / 2) - (joystick.rectTransform.sizeDelta.x / 2))) ? touchPosition.normalized : (touchPosition / ((joystickContainer.rectTransform.sizeDelta.x / 2) - (joystick.rectTransform.sizeDelta.x / 2)));
        //touchPosition = (touchPosition.magnitude > ((joystickContainer.rectTransform.sizeDelta.x / 2) - (joystick.rectTransform.sizeDelta.x / 2))) ? (touchPosition.normalized * ((joystickContainer.rectTransform.sizeDelta / 2) - (joystick.rectTransform.sizeDelta / 2))) : touchPosition;
        touchingJoystickPosition = touchingPosition;
        joystick.rectTransform.anchoredPosition = touchingPosition;

        //Debug.Log(joystickMagnitude);
        //Debug.Log(touchingJoystickPosition);
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        OnDrag(pointerEventData);
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        inputDirection = Vector3.zero;
        joystick.rectTransform.anchoredPosition = Vector3.zero;
    }

}
