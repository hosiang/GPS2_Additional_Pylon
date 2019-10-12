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

    [SerializeField] HealthSystem healthSystem;
    [SerializeField] NitroSystem nitroSystem;
    [SerializeField] WeightSystem weightSystem;

    private float facingAngle;
    private float currentFacingAngle;
    [SerializeField] private JoystickTouchController joystickTouchController;

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
        WeightToNitroConsume();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            healthSystem.TakeDamage(50f);
            //heatSystem.AddHeatAmount(heatCollisionEnemyIncreaseRate);
        }
    }

    void Rotation()
    {
        if(joystickTouchController.InputDirection != Vector3.zero)
        {
            facingAngle = Mathf.Atan2(joystickTouchController.InputDirection.x, joystickTouchController.InputDirection.y) * Mathf.Rad2Deg;
            if (facingAngle - currentFacingAngle > 180f) facingAngle -= 360f;
            else if (facingAngle - currentFacingAngle < -180f) facingAngle += 360f;

                currentFacingAngle = Mathf.Lerp(currentFacingAngle, facingAngle, 1f * Time.deltaTime);

            transform.rotation = Quaternion.Euler(0.0f, currentFacingAngle, 0.0f);
        }
        


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

    public void Thrusting()
    {
        if (nitroSystem.GetNitro() > 0)
        {
            playerRigidbody.velocity = transform.forward * (thrustPower * Time.deltaTime);
            //playerRigidbody.AddForce(transform.forward * thrustPower);
            nitroSystem.NitroReduction(nitroConsume);
        }
    }

    private void Thrust()
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
