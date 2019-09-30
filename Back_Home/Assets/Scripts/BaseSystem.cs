using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class BaseSystem : MonoBehaviour
{
    private float detectCircleRadius = 10.0f;
    public Transform detectCircleOriginTransform;
    private Vector3 detectCircleSecondPoint = Vector3.zero;
    [SerializeField] private bool debugMode;
    
    private List<float> storageResources = new List<float>();

    public Text IronText;
    public Text IronStorageText;
    public Text HealthPointText;

    public ShipEntity shipEntity;

    public Material materialNormal;
    public Material materialTriger;

    public MeshRenderer meshPlayer;

    void Start()
    {
        for(int i =0; i < (int)Global.OresTypes.Length; i++)
        {
            storageResources.Add(0.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        IronText.text = "Iron : " + shipEntity.WeightAmount;
        IronStorageText.text = "Iron Storage : " + storageResources[(int)Global.OresTypes.Iron];
        HealthPointText.text = "Health Point : " + (int)shipEntity.HealthPoint;

        Collider[] collider = Physics.OverlapSphere(detectCircleOriginTransform.position, detectCircleRadius, LayerMask.GetMask("Player"));
        if(collider.Length > 0)
        {
            meshPlayer.material = materialTriger;

            if (collider[0].GetComponent<ShipEntity>().WeightAmount > 0)
            {
                storageResources[(int)Global.OresTypes.Iron] = collider[0].GetComponent<ShipEntity>().UnloadResources();
            }
            if (collider[0].GetComponent<ShipEntity>().HealthPoint < collider[0].GetComponent<ShipEntity>().HealthPointMaximal)
            {
                collider[0].GetComponent<ShipEntity>().ReplenishHealthPoint(this);
            }
        }
        else
        {
            meshPlayer.material = materialNormal;
        }
        
    }

    private void OnDrawGizmos()
    {
        if (debugMode)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(detectCircleOriginTransform.position, detectCircleRadius);
        }
    }

}
