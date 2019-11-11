using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseSystem : MonoBehaviour
{
    private float currentTime = 0.0f;
    private float targetToExtendTime = 0.0f;
    private float currentShieldRadius = 0.0f;
    private float targetToExtendShieldRadius = 0.0f;
    private float startShieldRadius = 10.0f;
    private float minimalShieldRadius = 4.5f;
    [SerializeField] private float maximalShieldRadius = 10.0f;
    [SerializeField] private float shieldDeclineRate = 0.01f; // 0.01s is one second
    private float shieldSizeCovertValue = 1.5f;
    private Transform detectShieldOriginTransform;
    private bool isExtendingShield = false;
    private LayerMask layerMask_player;
    [SerializeField] private Transform shieldTransform;
    private Vector3 shieldOriginalScale;

    private float eachSpecialOreIncreaseTime = 0.05f;

    [SerializeField] private bool debugMode;

    public float CurrentTime { get { return currentTime; } }

    public float BeginShieldRadius { get { return startShieldRadius; } }

    public float CurrentShieldRadius { get { return currentShieldRadius; } }

    public bool IsExtendingShield { get { return isExtendingShield; } }

    protected Dictionary<Global.OresTypes, float> storageOresResources = new Dictionary<Global.OresTypes, float>();

    #region !! Border Lines Marker Rotation !!
    [Header("Border Lines Marker")]
    [SerializeField] private int borderLinesAmount = 20;
    [SerializeField] private GameObject borderLine;
    private List<GameObject> borderLines_GameObject = new List<GameObject>();
    private List<Transform> borderLines_Transform = new List<Transform>();
    private List<SpriteRenderer> borderLines_SpriteRenderer = new List<SpriteRenderer>();
    [SerializeField] private Color colorNotShield;
    private Color borderLineOriginalColor;
    readonly string name_borderLinesContainner = "BorderLinesContainner";
    private GameObject borderLinesContainner;

    private float eachAngle = 0.0f;
    private Vector3 eachBorderLinesPosition = Vector3.zero;
    private Quaternion eachBorderLinesRotation = Quaternion.identity;
    private float slowDownBorderLinesRotationSpeed = 5f;
    #endregion

    private Collider[] playerCollider;

    public bool IsPlayerInBase { get{ return playerCollider.Length > 0; } }

    private void Awake()
    {
        
        borderLinesContainner = new GameObject(name_borderLinesContainner); // Create the border line containner
        borderLineOriginalColor = borderLine.GetComponentInChildren<SpriteRenderer>().color;

        for (int i = 0; i < borderLinesAmount; i++) // Create the border line
        {
            borderLines_GameObject.Add(Instantiate<GameObject>(borderLine));
            borderLines_GameObject[i].name = borderLine.name + i;
            borderLines_SpriteRenderer.Add(borderLines_GameObject[i].GetComponentInChildren<SpriteRenderer>());
            borderLines_Transform.Add(borderLines_GameObject[i].GetComponent<Transform>());
            borderLines_Transform[i].SetParent(borderLinesContainner.transform);
            
        }
        
        detectShieldOriginTransform = this.GetComponent<Transform>();

        for (int i = 0; i < (int)Global.OresTypes.Length; i++) // Initialise the ores types resourses
        {
            storageOresResources.Add((Global.OresTypes)i, 0.0f);
        }
    }

    void Start()
    {
        layerMask_player = LayerMask.GetMask("Player");

        eachAngle = (2f * Mathf.PI) / borderLinesAmount; // For count the each border lines's angle
        
        currentTime = Global.ValueToTime(startShieldRadius);

        shieldOriginalScale = shieldTransform.localScale;
    }

    void Update()
    {
        ShieldSizeUpdate();
        BorderLinesMarkerRotating();

        playerCollider = Physics.OverlapSphere(detectShieldOriginTransform.position, currentShieldRadius, layerMask_player);
        
        if(playerCollider.Length > 0)
        {

            if (playerCollider[0].GetComponentInParent<ShipEntity>().CurrentWeight > 0.0f)
            {
                storageOresResources = playerCollider[0].GetComponentInParent<ShipEntity>().UnloadResources(this, storageOresResources);

                Debug.Log("Iron = " + storageOresResources[Global.OresTypes.Ore_No1]);
                Debug.Log("No2_Ores = " + storageOresResources[Global.OresTypes.Special_Ore]);
                RepairTheShield();
            }
            if (currentTime > 0.0f)
            {
                playerCollider[0].GetComponentInParent<ShipEntity>().ReplenishHealthPoint(this);
                playerCollider[0].GetComponentInParent<ShipEntity>().ReplenishNitroPoint(this);
            }

        }
        else
        {
            // Set something here
        }
        
    }

    public float GetBaseStorageOres(Global.OresTypes oresTypes)
    {
        return storageOresResources[oresTypes];
    }
    
    private void BorderLinesMarkerRotating()
    {
        for (int i = 0; i < borderLinesAmount; i++)
        {
            eachBorderLinesPosition.x = currentShieldRadius * Mathf.Sin((Time.time / slowDownBorderLinesRotationSpeed) + (eachAngle * i));
            eachBorderLinesPosition.y = 0.0f; //borderLines_Transform[i].position.y;
            eachBorderLinesPosition.z = currentShieldRadius * Mathf.Cos((Time.time / slowDownBorderLinesRotationSpeed) + (eachAngle * i));

            eachBorderLinesRotation.SetLookRotation(borderLines_Transform[i].position - transform.position);
            eachBorderLinesRotation *= Quaternion.Euler(0.0f, 90.0f, 0.0f);

            borderLines_Transform[i].SetPositionAndRotation(eachBorderLinesPosition, eachBorderLinesRotation);

            if(currentShieldRadius <= minimalShieldRadius)
            {
                borderLines_SpriteRenderer[i].color = colorNotShield;
            }
            else if(currentShieldRadius > minimalShieldRadius && borderLines_SpriteRenderer[i].color != borderLineOriginalColor)
            {
                borderLines_SpriteRenderer[i].color = borderLineOriginalColor;
            }

            //Debug.Log("Sin : " + Mathf.Rad2Deg * 180 + ", Cos : " + Mathf.Rad2Deg * 180);
        }
    }
    
    private void ShieldSizeFollowSafeZoneRadius()
    {

    }

    private void ShieldSizeUpdate()
    {
        if ((currentTime > 0.0f) && !isExtendingShield)
        {
            currentTime -= shieldDeclineRate * Time.deltaTime;
            if(currentTime < 0.0f) { currentTime = 0.0f; }

            currentShieldRadius = (((maximalShieldRadius - minimalShieldRadius) / maximalShieldRadius) * Global.TimeToValue(currentTime)) + minimalShieldRadius;

            targetToExtendTime = currentTime;
            targetToExtendShieldRadius = currentShieldRadius;
            if(currentShieldRadius > maximalShieldRadius) { currentShieldRadius = maximalShieldRadius; }
        }
    }

    private void RepairTheShield()
    {
        if (playerCollider.Length > 0)
        {
            if (storageOresResources[Global.OresTypes.Special_Ore] > 0.0f)
            {
                isExtendingShield = true;

                targetToExtendTime = (targetToExtendTime != currentTime) ? (targetToExtendTime + (eachSpecialOreIncreaseTime * storageOresResources[Global.OresTypes.Special_Ore])) : (currentTime + (eachSpecialOreIncreaseTime * storageOresResources[Global.OresTypes.Special_Ore]));
                Debug.Log(targetToExtendTime - currentTime );
                targetToExtendShieldRadius = (((maximalShieldRadius - minimalShieldRadius) / maximalShieldRadius) * Global.TimeToValue(targetToExtendTime)) + minimalShieldRadius;
                if (targetToExtendShieldRadius > maximalShieldRadius) { targetToExtendShieldRadius = maximalShieldRadius; }

                storageOresResources[Global.OresTypes.Special_Ore] = 0.0f;

                StopCoroutine("SmoothlyExtendTheShield");
                StartCoroutine("SmoothlyExtendTheShield");
            }
        }
        /*
        else
        {
            storageOresResources[Global.OresTypes.Special_Ore] = 0.0f;
        }
        */
    }

    private IEnumerator SmoothlyExtendTheShield()
    {
        float countTimeRate = 0.0f;
        while (true)
        {
            countTimeRate += 0.5f * Time.deltaTime;

            currentTime = Mathf.Lerp(currentTime, targetToExtendTime, countTimeRate);
            currentShieldRadius = Mathf.Lerp(currentShieldRadius, targetToExtendShieldRadius, countTimeRate);

            if (currentTime == targetToExtendTime)
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

    private void OnDestroy()
    {
        foreach (var tempBorderLines_Transform in borderLines_Transform) // Create the border line
        {
            Destroy(tempBorderLines_Transform);
        }
        foreach (var tempBorderLines_GameObject in borderLines_GameObject) // Create the border line
        {
            Destroy(tempBorderLines_GameObject);
        }
        Destroy(borderLinesContainner); // Delete the border line containner
    }

}
