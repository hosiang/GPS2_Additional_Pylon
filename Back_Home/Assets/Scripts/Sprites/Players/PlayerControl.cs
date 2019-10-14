using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float thrustPower = 20f;
    [SerializeField] private float rotateSpeed = 90f;
    [SerializeField] private float nitroConsume = 15f;

    private BoxCollider playerCollision;
    private Rigidbody playerRigidbody;
    private Transform playerTransform;
    private Vector3 moveDirection = Vector3.zero;
    

    [SerializeField] HealthSystem healthSystem;
    [SerializeField] NitroSystem nitroSystem;
    [SerializeField] WeightSystem weightSystem;

    private Vector3 playerOriginVector3 = Vector3.zero;
    private float rotationSpeed = 100f;
    private float facingAngle;
    private float currentFacingAngle;
    [SerializeField] private JoystickTouchController joystickTouchController;

    private Vector3 basePosition = Vector3.zero;

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
        playerTransform = GetComponent<Transform>();

        basePosition = FindObjectOfType<BaseSystem>().transform.position;

        //heatAmount = GetComponent<HeatSystem>().getHeatAmount();
        //maxHeatAmount = GetComponent<HeatSystem>().getMaxHeatAmount();
    }

    // Update is called once per frame
    void Update()
    {
        Rotation();
        Thrust();
        WeightToNitroConsume();

        CircularEdgeWallEffect();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            healthSystem.TakeDamage(50f);
            //heatSystem.AddHeatAmount(heatCollisionEnemyIncreaseRate);
        }
    }

    private void CircularEdgeWallEffect()
    {
        if( Vector3.Distance(basePosition, transform.position) > Global.zoneValues[(int)Global.ZoneLevels.HardZone])
        {
            playerRigidbody.velocity = Vector3.zero;
            transform.position = transform.position.normalized * Global.zoneValues[(int)Global.ZoneLevels.HardZone];
        }
    }

    void Rotation()
    {
        if (joystickTouchController.InputDirection != Vector3.zero)
        {
            /*
            if (playerRigidbody.position.y != 0.0f)
            {
                playerOriginVector3.x = playerTransform.position.x;
                playerOriginVector3.y = Mathf.Lerp(playerTransform.position.y, 0.0f, (0.1f / Mathf.Abs(playerOriginVector3.y - playerTransform.position.y)) * Time.deltaTime);
                playerOriginVector3.z = playerTransform.position.z;

                playerTransform.position = playerOriginVector3;
            } 
            */
            if (playerRigidbody.angularVelocity != Vector3.zero) // For reset the ship torque when the ship collder with something
            {
                currentFacingAngle = transform.rotation.eulerAngles.y; // 
                playerRigidbody.angularVelocity = Vector3.zero; // Reset the ship torque to all to be zero
            }

            facingAngle = Mathf.Atan2(joystickTouchController.InputDirection.x, joystickTouchController.InputDirection.y) * Mathf.Rad2Deg;
            if (facingAngle - currentFacingAngle > 180f) facingAngle -= 360f;
            else if (facingAngle - currentFacingAngle < -180f) facingAngle += 360f;

            currentFacingAngle = Mathf.Lerp(currentFacingAngle, facingAngle, (rotationSpeed / Mathf.Abs(currentFacingAngle - facingAngle) ) * Time.deltaTime);

            transform.rotation = Quaternion.Euler(0.0f, currentFacingAngle, 0.0f);

            /* other method, but have some probelm
            if (facingAngle - playerRigidbody.rotation.eulerAngles.y > 180f) facingAngle -= 360f;
            else if (facingAngle - playerRigidbody.rotation.eulerAngles.y < -180f) facingAngle += 360f;

            if (facingAngle < playerRigidbody.rotation.eulerAngles.y)
                playerRigidbody.angularVelocity = new Vector3(playerRigidbody.angularVelocity.x, -(50f * Time.deltaTime), playerRigidbody.angularVelocity.z);
            else if (facingAngle > playerRigidbody.rotation.eulerAngles.y)
                playerRigidbody.angularVelocity = new Vector3(playerRigidbody.angularVelocity.x, +(50f * Time.deltaTime), playerRigidbody.angularVelocity.z);
            */
        }

        // Old keyboard control
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //transform.rotation = Quaternion.Euler(0, -rotateSpeed * Time.deltaTime, 0); // Old one didn't use rotate speed just whole number
            //transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
            //transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0);
            transform.Rotate(0, -rotateSpeed, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Rotate(0, rotateSpeed, 0);
        }
    }

    public void Thrusting()
    {
        if (nitroSystem.GetNitro() > 0)
        {
            playerRigidbody.velocity += transform.forward * (thrustPower * Time.deltaTime);
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

    // Increase thrust power style for Weight–Thrust system
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
