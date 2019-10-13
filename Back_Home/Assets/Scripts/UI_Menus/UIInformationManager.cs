using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInformationManager : MonoBehaviour
{
    private string text_DistanceBetweenShipAndBase = "Distance With Base: ";
    //private string text_Timer = "Distance With Base: ";

    [SerializeField] private Slider healthPointSlider;
    [SerializeField] private Slider nitroPointSlider;
    [SerializeField] private Text distanceBetweenShipAndBaseText;
    [SerializeField] private Text timerText;

    private float distanceBetweenShipAndBase = 0.0f;

    private ShipEntity shipEntity;
    private BaseSystem baseSystem;

    private Transform shipTransform;
    private Transform baseTransform;

    //private int rawNumber;
    //private int radixPoint;

    private int timeValueRawNumber;
    private int timeValueRadixPoint;

    public Transform basePointerTransform;

    // Start is called before the first frame update
    void Start()
    {
        if(!(shipEntity = FindObjectOfType<ShipEntity>()))
            Debug.Log(this.GetType().Name + " have not found " + typeof(ShipEntity));

        if (!(baseSystem = FindObjectOfType<BaseSystem>()))
            Debug.Log(this.GetType().Name + " have not found " + typeof(BaseSystem));

        if (shipEntity != null)
            shipTransform = shipEntity.GetComponent<Transform>();

        if (baseSystem != null)
            baseTransform = baseSystem.GetComponent<Transform>();

        healthPointSlider.maxValue = shipEntity.HealthPointMaximal;
        nitroPointSlider.maxValue = shipEntity.NitroPointMaximal;
    }

    // Update is called once per frame
    void Update()
    {
        healthPointSlider.value = shipEntity.HealthPoint;
        nitroPointSlider.value = shipEntity.NitroPoint;

        //rawNumber = (int)baseSystem.CurrentShieldRadius;
        //radixPoint = (int)((baseSystem.CurrentShieldRadius - (int)baseSystem.CurrentShieldRadius) * 100);

        timeValueRawNumber = ((int)(baseSystem.CurrentShieldRadius * 100) / 60);
        timeValueRadixPoint = ((int)(baseSystem.CurrentShieldRadius * 100) % 60);

        timerText.text = timeValueRawNumber.ToString() + ":" + ((timeValueRadixPoint < 10) ? ("0" + timeValueRadixPoint.ToString()) : timeValueRadixPoint.ToString());

        distanceBetweenShipAndBase = baseTransform.position.magnitude - shipTransform.position.magnitude;

        distanceBetweenShipAndBaseText.text = text_DistanceBetweenShipAndBase + Mathf.Abs(distanceBetweenShipAndBase) + "m";

        Vector3 shipAndBaseNormalized = (shipTransform.position - baseTransform.position).normalized;

        basePointerTransform.LookAt(baseTransform.position);

        basePointerTransform.position = shipTransform.position - (shipAndBaseNormalized * 1.5f);
    }
}
