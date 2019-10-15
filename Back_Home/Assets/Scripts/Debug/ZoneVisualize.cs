using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class ZoneDetails
{
    [SerializeField] public int borderLinesAmount = 20;
    [SerializeField] public Color colorBorderLines = new Color(1.0f, 1.0f, 1.0f, 1.0f);
}

[System.Serializable] public class ZoneBorderLinesDetails
{
    public List<GameObject> borderLines_GameObject = new List<GameObject>();
    public List<Transform> borderLines_Transform = new List<Transform>();
    public List<SpriteRenderer> borderLines_SpriteRenderer = new List<SpriteRenderer>();
}

public class ZoneVisualize : MonoBehaviour
{
    [Header("Border Lines Marker")]
    [SerializeField] private GameObject borderLine;
    [SerializeField] private List<ZoneDetails> zoneDetails = new List<ZoneDetails>();
    private List<ZoneBorderLinesDetails> zoneBorderLinesDetails = new List<ZoneBorderLinesDetails>();

    readonly List<string> name_ZoneContainners = new List<string>() { "EasyZoneContainner", "MediumZoneContainner", "HardZoneContainner" };
    private List<GameObject> zoneContainners = new List<GameObject>();

    private List<float> eachAngles = new List<float>();
    private Vector3 eachBorderLinesPosition = Vector3.zero;
    private Quaternion eachBorderLinesRotation = Quaternion.identity;
    private float slowDownBorderLinesRotationSpeed = 5f;

    private Transform baseTransform;

    // Start is called before the first frame update
    void Start()
    {
        baseTransform = FindObjectOfType<BaseSystem>().transform;

        for (int i = 0; i < name_ZoneContainners.Count; i++)
        {
            zoneContainners.Add(new GameObject(name_ZoneContainners[i]));
        }
        for (int i = 0; i < zoneDetails.Count; i++)
        {
            zoneBorderLinesDetails.Add(new ZoneBorderLinesDetails());
            for (int j = 0; j < zoneDetails[i].borderLinesAmount; j++) // Create the border line
            {
                //Debug.Log("<color=red>Not Here!</color>");
                zoneBorderLinesDetails[i].borderLines_GameObject.Add(Instantiate<GameObject>(borderLine));
                zoneBorderLinesDetails[i].borderLines_GameObject[j].name = borderLine.name + i;
                zoneBorderLinesDetails[i].borderLines_SpriteRenderer.Add(zoneBorderLinesDetails[i].borderLines_GameObject[j].GetComponentInChildren<SpriteRenderer>());
                zoneBorderLinesDetails[i].borderLines_SpriteRenderer[j].color = zoneDetails[i].colorBorderLines;
                zoneBorderLinesDetails[i].borderLines_Transform.Add(zoneBorderLinesDetails[i].borderLines_GameObject[j].GetComponent<Transform>());
                zoneBorderLinesDetails[i].borderLines_Transform[j].SetParent(zoneContainners[i].transform);

            }
        }

        for (int i = 0; i < zoneDetails.Count; i++)
        {
            eachAngles.Add((2f * Mathf.PI) / zoneDetails[i].borderLinesAmount);
        }
         // For count the each border lines's angle
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < zoneDetails.Count; i++)
        {
            for (int j = 0; j < zoneDetails[i].borderLinesAmount; j++)
            {
                eachBorderLinesPosition = Vector3.zero;
                eachBorderLinesRotation = Quaternion.identity;

                eachBorderLinesPosition.x = Global.zoneValues[i+1] * Mathf.Sin((Time.time / slowDownBorderLinesRotationSpeed) + (eachAngles[i] * j));
                eachBorderLinesPosition.y = 0.0f; //borderLines_Transform[i].position.y;
                eachBorderLinesPosition.z = Global.zoneValues[i + 1] * Mathf.Cos((Time.time / slowDownBorderLinesRotationSpeed) + (eachAngles[i] * j));

                eachBorderLinesRotation.SetLookRotation(zoneBorderLinesDetails[i].borderLines_Transform[j].position - baseTransform.position);
                eachBorderLinesRotation *= Quaternion.Euler(0.0f, 90.0f, 0.0f);

                zoneBorderLinesDetails[i].borderLines_Transform[j].SetPositionAndRotation(eachBorderLinesPosition, eachBorderLinesRotation);

                //Debug.Log("Sin : " + Mathf.Rad2Deg * 180 + ", Cos : " + Mathf.Rad2Deg * 180);
            }
        }
    }
}
