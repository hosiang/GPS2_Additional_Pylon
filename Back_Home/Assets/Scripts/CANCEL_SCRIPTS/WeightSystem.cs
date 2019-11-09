using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightSystem : MonoBehaviour
{
    [SerializeField] protected float currentWeight = 100f;
    [SerializeField] protected float maximalWeight = 100f;
    protected Dictionary<Global.OresTypes, float> oresAmount = new Dictionary<Global.OresTypes, float>();

    public float CurrentWeight { get { return currentWeight; } }
    public float MaximalWeight { get { return maximalWeight; } }

    private Rigidbody playerRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        WeightChecker();
    }

    /// <summary>
    /// Drag increase style for Weight–Thrust system
    /// </summary>
    void WeightChecker()
    {
        if (currentWeight < 0)
        {
            playerRigidbody.drag = 0.0f;
        }
        else if (currentWeight == 0)
        {
            playerRigidbody.drag = 0.5f;
        }
        else if (currentWeight > 0 && currentWeight <= (maximalWeight / 3))
        {
            playerRigidbody.drag = 0.7f;
        }
        else if (currentWeight > (maximalWeight / 3) && currentWeight <= ((maximalWeight / 3) * 2))
        {
            playerRigidbody.drag = 0.9f;
        }
        else if (currentWeight > ((maximalWeight / 3) * 2))
        {
            playerRigidbody.drag = 1.1f;
        }
    }

    public float GetWeight()
    {
        return currentWeight;
    }
    public float GetMaxWeight()
    {
        return maximalWeight;
    }
    public void SetWeight()
    {
        currentWeight = 0;
    }
}