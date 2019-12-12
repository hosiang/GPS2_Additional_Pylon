using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalKeyEvent : MonoBehaviour
{
    [SerializeField] private GameObject finalKeyPrefab;

    private GameObject finalKeyGameObject;
    private Transform finalKeyTransform;

    [SerializeField] private float ableToGetFinalKeyTime = 3.0f;
    private float ableToGetFinalKeyTimer = 0.0f;

    private bool onPlayerFoundFinalKey = false;
    private bool isHavingFinalKey = false;

    public bool OnPlayerFoundFinalKey { get { return onPlayerFoundFinalKey; } }
    public bool IsHavingFinalKey { get{ return isHavingFinalKey; } }

    // Start is called before the first frame update
    void Start()
    {
        finalKeyGameObject = Instantiate<GameObject>(finalKeyPrefab);
        finalKeyTransform = finalKeyGameObject.GetComponent<Transform>();

        finalKeyTransform.position = GenerateFinalKeyLocation();
    }

    

    // Update is called once per frame
    void Update()
    {
        if (!isHavingFinalKey && onPlayerFoundFinalKey)
        {
            ableToGetFinalKeyTimer += Time.deltaTime;

            if (ableToGetFinalKeyTimer > ableToGetFinalKeyTime)
            {
                finalKeyTransform.position = GenerateFinalKeyLocation();
                ableToGetFinalKeyTimer = 0.0f;
                onPlayerFoundFinalKey = false;
            }
        }
    }

    private Vector3 GenerateFinalKeyLocation()
    {
        Vector3 tempRandomPosition = Vector3.zero;

        float angle = Random.Range(0, Mathf.PI * 2);

        tempRandomPosition.x = Random.Range(Mathf.Cos(angle) * Global.zonesRadius[(int)Global.ZoneLevels.MediumZone], Mathf.Cos(angle) * Global.zonesRadius[(int)Global.ZoneLevels.HardZone]);
        tempRandomPosition.y = 0.0f;
        tempRandomPosition.z = Random.Range(Mathf.Sin(angle) * (int)Global.zonesRadius[(int)Global.ZoneLevels.MediumZone], Mathf.Sin(angle) * (int)Global.zonesRadius[(int)Global.ZoneLevels.HardZone]);

        return tempRandomPosition;
    }

    public void PlayerGetFinalKey(object requestObject)
    {
        if(requestObject.GetType().Name == nameof(FinalKey))
        {
            isHavingFinalKey = true;
            Destroy(finalKeyGameObject);
        }
    }

    public void PlayerFoundFinalKey(object requestObject)
    {
        if (requestObject.GetType().Name == nameof(FinalKey))
        {
            if (!onPlayerFoundFinalKey)
            {
                onPlayerFoundFinalKey = true;
            }
        }
        
    }
}
