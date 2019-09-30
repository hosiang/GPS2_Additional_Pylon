using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float thrustPower = 200f;
    [SerializeField] private float rotateSpeed = 90f;
    [SerializeField] private float nitroConsume = 15f;
    private int rotationSetting = 0;

    private BoxCollider playerCollision;
    private Rigidbody playerRigidbody;
    private Vector3 moveDirection = Vector3.zero;

    [SerializeField] NitroSystem nitroSystem;
    [SerializeField] WeightSystem weightSystem;

    //[SerializeField] HeatSystem heatSystem;
    //[SerializeField] private float heatAmount;
    //[SerializeField] private float maxHeatAmount;
    //[SerializeField] private float heatCollisionEnemyIncreaseRate;
    //[SerializeField] private float heatCollisionThrustRate;

    // Start is called before the first frame update
    void Start()
    {
        playerCollision = GetComponent<BoxCollider>();
        playerRigidbody = GetComponent<Rigidbody>();

        //heatAmount = GetComponent<HeatSystem>().getHeatAmount();
        //maxHeatAmount = GetComponent<HeatSystem>().getMaxHeatAmount();
    }

    // Update is called once per frame
    void Update()
    {
        Rotation();
        Thrust();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //heatSystem.AddHeatAmount(heatCollisionEnemyIncreaseRate);
        }
    }

    void Rotation()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //transform.rotation = Quaternion.Euler(0, -rotateSpeed * Time.deltaTime, 0); // Old one didn't use rotate speed just whole number
            //transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
            //transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0);
            rotationSetting = 1;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rotationSetting = 2;
        }
        else rotationSetting = 0;

        if (rotationSetting == 1)
        {
            transform.Rotate(0, -rotateSpeed, 0);
        }
        else if (rotationSetting == 2)
        {
            transform.Rotate(0, rotateSpeed, 0);
        }
        else if (rotationSetting == 0) { }
    }

    void Thrust()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (nitroSystem.GetNitro() > 0)
            {
                playerRigidbody.AddForce(transform.right * thrustPower);
                nitroSystem.NitroReduction(nitroConsume);
            }

            //if (heatSystem.GetHeatAmount() < heatSystem.GetMaxHeatAmount())
            //{
            //    playerRigidbody.AddForce(transform.right * thrustPower);
            //    heatSystem.AddHeatAmount(heatCollisionThrustRate);
            //}
            //else {}
        }
    }

    /// <summary>
    /// Energy deplete point style for Weight–Thrust system
    /// </summary>
    void WeightToNitroConsume()
    {
        if (weightSystem.GetWeight() <= 0)
        {
            nitroConsume = 15f;
        }
        else if (weightSystem.GetWeight() > 0 && weightSystem.GetWeight() <= weightSystem.GetMaxWeight() / 3)
        {
            nitroConsume = 17f;
        }
        else if (weightSystem.GetWeight() > weightSystem.GetMaxWeight() / 3 && weightSystem.GetWeight() <= (weightSystem.GetMaxWeight() / 3) * 2)
        {
            nitroConsume = 19f;
        }
        else if (weightSystem.GetWeight() > (weightSystem.GetMaxWeight() / 3) * 2)
        {
            nitroConsume = 21f;
        }
    }
    // Thrust power style for Weight–Thrust system
    //void WeightToThrustPower()
    //{
    //    if (weightSystem.GetWeight() <= 0)
    //    {
    //        thrustPower = 200f;
    //    }
    //    else if (weightSystem.GetWeight() > 0 && weightSystem.GetWeight() <= weightSystem.GetMaxWeight() / 3)
    //    {
    //        thrustPower = 170f;
    //    }
    //    else if (weightSystem.GetWeight() > weightSystem.GetMaxWeight() / 3 && weightSystem.GetWeight() <= (weightSystem.GetMaxWeight() / 3) * 2)
    //    {
    //        thrustPower = 140f;
    //    }
    //    else if (weightSystem.GetWeight() > (weightSystem.GetMaxWeight() / 3) * 2)
    //    {
    //        thrustPower = 110f;
    //    }
    //}
}
