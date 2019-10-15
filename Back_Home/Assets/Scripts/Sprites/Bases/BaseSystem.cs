using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseSystem : MonoBehaviour
{
    private float currentShieldRadius = 0.0f;
    private float targetToExtendShieldRadius = 0.0f;
    [SerializeField] private float startShieldRadius = 6.0f;
    private float minimalShieldRadius = 4.5f;
    [SerializeField] private float maximalShieldRadius = 12.0f;
    [SerializeField] private float shieldDeclineRate = 1f;
    private float shieldSizeCovertValue = 1.5f;
    private Transform detectShieldOriginTransform;
    private bool isExtendingShield = false;
    private LayerMask layerMask_player;

    [SerializeField] private bool debugMode;

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

        currentShieldRadius = startShieldRadius;
    }

    void Update()
    {
        

        ShieldSizeUpdate();
        BorderLinesMarkerRotating();

        playerCollider = Physics.OverlapSphere(detectShieldOriginTransform.position, ((currentShieldRadius + minimalShieldRadius) * shieldSizeCovertValue), layerMask_player);
        
        if(playerCollider.Length > 0)
        {

            if (playerCollider[0].GetComponentInParent<ShipEntity>().WeightAmount > 0.0f)
            {
                storageOresResources = playerCollider[0].GetComponentInParent<ShipEntity>().UnloadResources(this, storageOresResources);

                Debug.Log("Iron = " + storageOresResources[Global.OresTypes.Iron]);
                Debug.Log("No2_Ores = " + storageOresResources[Global.OresTypes.no2_Ores]);
            }
            if (currentShieldRadius > 0.0f)
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

    private void BorderLinesMarkerRotating()
    {
        for (int i = 0; i < borderLinesAmount; i++)
        {
            eachBorderLinesPosition.x = ((currentShieldRadius + minimalShieldRadius) * shieldSizeCovertValue) * Mathf.Sin((Time.time / slowDownBorderLinesRotationSpeed) + (eachAngle * i));
            eachBorderLinesPosition.y = 0.0f; //borderLines_Transform[i].position.y;
            eachBorderLinesPosition.z = ((currentShieldRadius + minimalShieldRadius) * shieldSizeCovertValue) * Mathf.Cos((Time.time / slowDownBorderLinesRotationSpeed) + (eachAngle * i));

            eachBorderLinesRotation.SetLookRotation(borderLines_Transform[i].position - transform.position);
            eachBorderLinesRotation *= Quaternion.Euler(0.0f, 90.0f, 0.0f);

            borderLines_Transform[i].SetPositionAndRotation(eachBorderLinesPosition, eachBorderLinesRotation);

            if(currentShieldRadius <= 0.0f)
            {
                borderLines_SpriteRenderer[i].color = colorNotShield;
            }
            else if(currentShieldRadius > 0.0f && borderLines_SpriteRenderer[i].color != borderLineOriginalColor)
            {
                borderLines_SpriteRenderer[i].color = borderLineOriginalColor;
            }

            //Debug.Log("Sin : " + Mathf.Rad2Deg * 180 + ", Cos : " + Mathf.Rad2Deg * 180);
        }
    }

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
        if (playerCollider.Length > 0)
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
            Gizmos.DrawWireSphere(detectShieldOriginTransform.position, ((currentShieldRadius + minimalShieldRadius) * shieldSizeCovertValue));
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
