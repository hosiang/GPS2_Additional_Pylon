using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightSystem : MonoBehaviour
{
    [SerializeField] private float maxWeight = 100;
    [SerializeField] private float currentWeight = 0;

    private Rigidbody playerRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //WeightChecker();
    }

    /// <summary>
    /// Drag increase style for Weight–Thrust system
    /// </summary>
    //void WeightChecker()
    //{
    //    if (currentWeight < 0)
    //    {
    //        playerRigidbody.drag = 0.0f;
    //    }
    //    else if (currentWeight == 0)
    //    {
    //        playerRigidbody.drag = 0.5f;
    //    }
    //    else if (currentWeight > 0 && currentWeight <= (maxWeight / 3))
    //    {
    //        playerRigidbody.drag = 0.7f;
    //    }
    //    else if (currentWeight > (maxWeight / 3) && currentWeight <= ((maxWeight / 3) * 2))
    //    {
    //        playerRigidbody.drag = 0.9f;
    //    }
    //    else if (currentWeight > ((maxWeight / 3) * 2))
    //    {
    //        playerRigidbody.drag = 1.1f;
    //    }
    //}

    public float GetWeight()
    {
        return currentWeight;
    }
    public float GetMaxWeight()
    {
        return maxWeight;
    }
    public void SetWeight()
    {
        currentWeight = 0;
    }
}
