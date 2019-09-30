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

    public Rigidbody objectRigidbody;

    public Text axisXText;
    public Text axisYText;

    Vector3 InputDirection
    {
        get
        {
            return inputDirection;
        }
    }

    private Vector2 touchPosition = Vector2.zero;

    void Start()
    {
        joystickContainer = GetComponent<Image>();
        joystick = transform.Find(name_Joystick).GetComponent<Image>(); //this command is used because there is only one child in hierarchy

        //Debug.Log(joystickContainer.rectTransform.sizeDelta.x);
        //Debug.Log(joystickContainer.rectTransform.pivot.x);
    }

    private void Update()
    {
        objectRigidbody.velocity = new Vector3(inputDirection.x * 5f, 0.0f, inputDirection.y * 5f);

        axisXText.text = "Axis X : " + inputDirection.x;
        axisYText.text = "Axis Y : " + inputDirection.y;
        
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        /*
        touchPosition = Vector2.zero;

        //To get Input Direction
        RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickContainer.rectTransform, pointerEventData.position, pointerEventData.pressEventCamera, out touchPosition);

        touchPosition.x = (touchPosition.x / joystickContainer.rectTransform.sizeDelta.x);
        touchPosition.y = (touchPosition.y / joystickContainer.rectTransform.sizeDelta.y);

        float x = (touchPosition.x < 1.0f) ? touchPosition.x : 1.0f;
        float y = (touchPosition.y < 1.0f) ? touchPosition.y : 1.0f;

        joystick.rectTransform.anchoredPosition = new Vector2(x * (joystickContainer.rectTransform.sizeDelta.x/2), y * (joystickContainer.rectTransform.sizeDelta.y/2));
        */
        
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
