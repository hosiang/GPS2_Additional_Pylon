using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInformationManager : MonoBehaviour
{
    private string text_DistanceBetweenShipAndBase = "Distance With Base: ";
    private string text_OreAmount = " x ";
    //private string text_Timer = "Distance With Base: ";

    [SerializeField] private Slider healthPointSlider;
    [SerializeField] private Image nitroPointImage;
    private float eachNitroPointRate;
    private float coverNitroPointRate = 0.04f;
    [SerializeField] private Slider weightPointSlider;
    [SerializeField] private Text distanceBetweenShipAndBaseText;
    [SerializeField] private Text timerText;
    [SerializeField] private Text oreAmountText;

    private float distanceBetweenShipAndBase = 0.0f;

    private ShipEntity shipEntity;
    private BaseSystem baseSystem;
    private TimerManager timerManager;

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
        if (!(shipEntity = FindObjectOfType<ShipEntity>()))
            Debug.Log(this.GetType().Name + " have not found " + typeof(ShipEntity));

        if (!(baseSystem = FindObjectOfType<BaseSystem>()))
            Debug.Log(this.GetType().Name + " have not found " + typeof(BaseSystem));

        if (!(timerManager = FindObjectOfType<TimerManager>()))
            Debug.Log(this.GetType().Name + " have not found " + typeof(TimerManager));
        
        if (shipEntity != null)
            shipTransform = shipEntity.GetComponent<Transform>();

        if (baseSystem != null)
            baseTransform = baseSystem.GetComponent<Transform>();

        healthPointSlider.maxValue = shipEntity.MaximalHealth;
        eachNitroPointRate = (1.0f - (coverNitroPointRate * 2.0f)) / shipEntity.MaximalNitro;
        nitroPointImage.fillAmount = (eachNitroPointRate * shipEntity.MaximalNitro) + coverNitroPointRate;
        weightPointSlider.maxValue = shipEntity.MaximalWeight;
    }

    // Update is called once per frame
    void Update()
    {
        healthPointSlider.value = shipEntity.CurrentHealth;
        nitroPointImage.fillAmount = (eachNitroPointRate * shipEntity.CurrentNitro) + coverNitroPointRate;
        weightPointSlider.value = shipEntity.CurrentWeight;

        oreAmountText.text = shipEntity.GetShipOresAmount(Global.OresTypes.Ore_No1).ToString() + text_OreAmount;

        //rawNumber = (int)baseSystem.CurrentShieldRadius;
        //radixPoint = (int)((baseSystem.CurrentShieldRadius - (int)baseSystem.CurrentShieldRadius) * 100);

        timeValueRawNumber = (int)(timerManager.CurrentTime * 1) / 60; //((int)(baseSystem.CurrentShieldRadius * 100) / 60);
        timeValueRadixPoint = (int)(timerManager.CurrentTime * 1) % 60; //((int)(baseSystem.CurrentShieldRadius * 100) % 60);

        timerText.text = timeValueRawNumber.ToString() + ":" + ((timeValueRadixPoint < 10) ? ("0" + timeValueRadixPoint.ToString()) : timeValueRadixPoint.ToString());

        distanceBetweenShipAndBase = baseTransform.position.magnitude - shipTransform.position.magnitude; // Count the distance between base and ship
        distanceBetweenShipAndBase = (int)(distanceBetweenShipAndBase * 100f); // Getting radix point first step (3.200f * 100)
        distanceBetweenShipAndBase = distanceBetweenShipAndBase / 100f; // Getting radix point last step

        distanceBetweenShipAndBaseText.text = text_DistanceBetweenShipAndBase + Mathf.Abs(distanceBetweenShipAndBase).ToString() + "m";

        Vector3 shipAndBaseNormalized = (shipTransform.position - baseTransform.position).normalized;

        basePointerTransform.LookAt(baseTransform.position);

        basePointerTransform.position = shipTransform.position - (shipAndBaseNormalized * 1.5f);
    }
}
