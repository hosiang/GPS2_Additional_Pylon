using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[System.Serializable] public class DynamicUnityEvent_Float : UnityEvent<float> { }

public class CustomSlider : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    // public Button selectable;

    private string name_BackgroundImage = "BackgroundImage";
    private string name_SliderHandle = "SliderHandle";
    
    private Image backgroundImage;
    private Image sliderHandle;

    private float inputDirectionX;
    private Vector2 touchingPosition = Vector2.zero;
    private Vector2 touchingSliderPosition = Vector2.zero;

    [SerializeField] private DynamicUnityEvent_Float onValueChange;

    private bool buttonOnPressing = false;

    // Getter
    public bool ButtonOnPressing { get { return buttonOnPressing; } }
    public float InputDirectionX { get { return inputDirectionX; } }
    public Vector2 TouchingSliderPosition { get { return touchingSliderPosition; } }

    // Start is called before the first frame update
    void Start()
    {
        backgroundImage = transform.Find(name_BackgroundImage).GetComponent<Image>();
        sliderHandle = transform.Find(name_SliderHandle).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //DoStateTransition(SelectionState.Disabled, true);
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);

        buttonOnPressing = true;

        StopCoroutine("OnPressingCoroutine");
        StartCoroutine("OnPressingCoroutine");
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(backgroundImage.rectTransform, eventData.position, eventData.pressEventCamera, out touchingPosition);
        touchingSliderPosition = touchingPosition;

        touchingPosition.y = 0.0f;
        if (Mathf.Abs(touchingPosition.x) > (backgroundImage.rectTransform.sizeDelta.x / 2) )
        {
            touchingPosition.x = touchingPosition.normalized.x * (backgroundImage.rectTransform.sizeDelta.x / 2);
            inputDirectionX = touchingPosition.normalized.x;
        }
        else
        {
            inputDirectionX = touchingPosition.x / (backgroundImage.rectTransform.sizeDelta.x / 2);
        }

        touchingSliderPosition = touchingPosition;
        sliderHandle.rectTransform.anchoredPosition = touchingPosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonOnPressing = false;

        inputDirectionX = 0.0f;
        sliderHandle.rectTransform.anchoredPosition = Vector3.zero;
        //base.OnPointerUp(eventData);
    }

    private IEnumerator OnPressingCoroutine()
    {
        while (buttonOnPressing)
        {
            onValueChange.Invoke(inputDirectionX);
            yield return null;
        }
    }


}
