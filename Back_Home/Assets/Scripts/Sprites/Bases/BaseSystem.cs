using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseSystem : MonoBehaviour
{
    private float currentShieldRadius = 0.0f;
    private float targetToExtendShieldRadius = 0.0f;
    [SerializeField] private float startShieldRadius = 6.0f;
    [SerializeField] private float maximalShieldRadius = 12.0f;
    [SerializeField] private float shieldDeclineRate = 1f;
    public Transform detectShieldOriginTransform;
    [SerializeField] private bool debugMode;

    public float CurrentShieldRadius { get { return currentShieldRadius; } }

    private bool isExtendingShield = false;

    protected Dictionary<Global.OresTypes, float> storageOresResources = new Dictionary<Global.OresTypes, float>();
    
    public ShipEntity shipEntity;

    /*
    public Text IronText;
    public Text IronStorageText;
    public Text no2OreText;
    public Text no2OreStorageText;
    public Text HealthPointText;
    
    public Material materialNormal;
    public Material materialTriger;
    
    public MeshRenderer meshPlayer;

    [SerializeField] private Transform shieldVisualize;
    [SerializeField] private Transform originalShieldVisualize;
    [SerializeField] private Transform maximalShieldVisualize;
    */
    private Collider[] shieldCollider;

    void Start()
    {
        for(int i =0; i < (int)Global.OresTypes.Length; i++)
        {
            storageOresResources.Add((Global.OresTypes)i, 0.0f); // Initialise the ores types resourses
        }

        currentShieldRadius = startShieldRadius;
    }

    void Update()
    {
        //TestingTextUIUpdate();
        ShieldSizeUpdate();
        //TestingShieldVisualise();

        shieldCollider = Physics.OverlapSphere(detectShieldOriginTransform.position, currentShieldRadius, LayerMask.GetMask("Player"));

        if(shieldCollider.Length > 0)
        {
            //meshPlayer.material = materialTriger;

            if (shieldCollider[0].GetComponentInParent<ShipEntity>().WeightAmount > 0.0f)
            {
                storageOresResources = shieldCollider[0].GetComponentInParent<ShipEntity>().UnloadResources(this);
            }

            shieldCollider[0].GetComponentInParent<ShipEntity>().ReplenishHealthPoint(this);
            shieldCollider[0].GetComponentInParent<ShipEntity>().ReplenishNitroPoint(this);

        }
        else
        {
            //meshPlayer.material = materialNormal;
        }
        
    }
    /*
    private void TestingTextUIUpdate()
    {
        IronText.text = "Iron : " + shipEntity.GetOresAmount(this)[Global.OresTypes.Iron];
        IronStorageText.text = "Iron Storage : " + storageOresResources[Global.OresTypes.Iron];
        no2OreText.text = "Ore 2 : " + shipEntity.GetOresAmount(this)[Global.OresTypes.no2_Ores];
        no2OreStorageText.text = "Ore 2 Storage : " + storageOresResources[Global.OresTypes.no2_Ores];
        HealthPointText.text = "Health Point : " + (int)shipEntity.HealthPoint;
    }

    private void TestingShieldVisualise()
    {
        shieldVisualize.position = new Vector3(currentShieldRadius * Mathf.Sin(Time.time * 3.0f), 0.0f, currentShieldRadius * Mathf.Cos(Time.time * 3.0f));
        originalShieldVisualize.position = new Vector3(startShieldRadius * Mathf.Sin(Time.time * 3.0f), 0.0f, startShieldRadius * Mathf.Cos(Time.time * 3.0f));
        maximalShieldVisualize.position = new Vector3(maximalShieldRadius * Mathf.Sin(Time.time * 3.0f), 0.0f, maximalShieldRadius * Mathf.Cos(Time.time * 3.0f));
    }
    */
    private void ShieldSizeUpdate()
    {
        if ((currentShieldRadius > 0.0f) && !isExtendingShield)
        {
            currentShieldRadius -= (shieldDeclineRate * 0.01f) * Time.deltaTime;
            if(currentShieldRadius < 0.0f) { currentShieldRadius = 0.0f; }
            targetToExtendShieldRadius = currentShieldRadius;
        }
    }

    public void RepairTheShield()
    {
        if (shieldCollider.Length > 0)
        {
            if((storageOresResources[Global.OresTypes.no2_Ores] >= 100.0f) && (targetToExtendShieldRadius < (maximalShieldRadius - 5.0f)))
            {
                isExtendingShield = true;

                storageOresResources[Global.OresTypes.no2_Ores] -= 100.0f;

                targetToExtendShieldRadius = (targetToExtendShieldRadius != currentShieldRadius) ? (targetToExtendShieldRadius + 5.0f) : (currentShieldRadius + 5.0f);
                if (targetToExtendShieldRadius > maximalShieldRadius) targetToExtendShieldRadius = maximalShieldRadius;

                StopCoroutine("SmoothlyExtendTheShield");
                StartCoroutine("SmoothlyExtendTheShield");
            }
        }
    }

    private IEnumerator SmoothlyExtendTheShield()
    {
        float countTimeRate = 0.0f;
        while (true)
        {
            countTimeRate += 0.1f * Time.deltaTime;

            currentShieldRadius = Mathf.Lerp(currentShieldRadius, targetToExtendShieldRadius, countTimeRate);

            if (currentShieldRadius == targetToExtendShieldRadius)
            {
                isExtendingShield = false;
                break;
            }

            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        if (debugMode)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(detectShieldOriginTransform.position, currentShieldRadius);
        }
    }

}
