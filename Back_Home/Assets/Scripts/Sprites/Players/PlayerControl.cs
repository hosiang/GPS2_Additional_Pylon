﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float thrustPower = 20f;
    [SerializeField] private float rotateSpeed = 90f;
    [SerializeField] private float nitroConsume = 15f;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private ParticleSystem mainThruster;
    [SerializeField] private ParticleSystem leftSideThruster;
    [SerializeField] private ParticleSystem rightSideThruster;
    [SerializeField] private ParticleSystem drillerVibrationFrequencyParticleSystem;

    private BoxCollider playerCollision;
    private Rigidbody playerRigidbody;
    private Transform playerTransform;
    private Vector3 moveDirection = Vector3.zero;

    //Drill
    private enum DrillSpeed { slow, fast };

    [SerializeField] private DrillSpeed drillSpeed;

    [SerializeField] private Transform drillStart;
    [SerializeField] private Transform drillEnd;
    [SerializeField] private float drillRadius;
    private bool onDrilling = false;
    private Collider[] drillCollide;

    [SerializeField] private float damage;
    [SerializeField] private float vibrationFrequency;
    [SerializeField] private float drillFastSpeedMultiplier;

    [SerializeField] HealthSystem healthSystem;
    [SerializeField] NitroSystem nitroSystem;
    [SerializeField] WeightSystem weightSystem;

    [SerializeField] UnityEngine.UI.Toggle slowDrillToggle;

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

        slowDrillToggle.isOn = (drillSpeed == DrillSpeed.slow) ? true : false;

        drillerVibrationFrequencyParticleSystem.Stop();

        //heatAmount = GetComponent<HeatSystem>().getHeatAmount();
        //maxHeatAmount = GetComponent<HeatSystem>().getMaxHeatAmount();
    }

    // Update is called once per frame
    void Update()
    {
        if (!onDrilling)
        {
            Rotation();
            Thrust();
            WeightToNitroConsume();
        }

        CircularEdgeWallEffect();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerAnimator.SetTrigger("isHurt");

            CameraShaker.Instance.ShakeOnce(4f, 4f, 0.1f, 2f);
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
            playerAnimator.SetTrigger("isThrustPress");
            mainThruster.Play();
            leftSideThruster.Play();
            rightSideThruster.Play();

            playerRigidbody.velocity += transform.forward * (thrustPower * Time.deltaTime);
            //playerRigidbody.AddForce(transform.forward * thrustPower);
            nitroSystem.NitroReduction(nitroConsume);
        }
        else
        {
            if (mainThruster.isPlaying || leftSideThruster.isPlaying || rightSideThruster.isPlaying)
            {
                mainThruster.Stop();
                leftSideThruster.Stop();
                rightSideThruster.Stop();
            }
        }
    }
    public void ThrustingRelease()
    {
        playerAnimator.SetTrigger("isThrustRelease");
        mainThruster.Stop();
        leftSideThruster.Stop();
        rightSideThruster.Stop();
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

    public void SwitchDrillSpeed(int whichDrillSpeed)
    {
        drillSpeed = (DrillSpeed)whichDrillSpeed;
    }

    public void DrillCollideDetect()
    {
        drillCollide = Physics.OverlapCapsule(drillStart.position, drillEnd.position, drillRadius, Global.layer_Astroid);

        if (drillCollide.Length > 0)
        {
            for (int i = 0; i < drillCollide.Length; ++i)
            {
                if (!(drillCollide[i].gameObject.GetComponentInParent<Asteroid>().GetAstroidType() == Global.AstroidType.big && drillSpeed == DrillSpeed.slow))
                {
                    playerAnimator.SetTrigger("isDrillPress");

                    onDrilling = true;

                    playerRigidbody.angularVelocity = Vector3.zero;
                    playerRigidbody.velocity = Vector3.zero;
                    playerTransform.position = playerTransform.position;

                    if (drillerVibrationFrequencyParticleSystem.isStopped)
                    {
                        drillerVibrationFrequencyParticleSystem.Play();
                    }

                    drillCollide[i].gameObject.GetComponentInParent<Asteroid>().Drill(drillSpeed == DrillSpeed.fast ? damage * drillFastSpeedMultiplier : damage,
                                                                             drillSpeed == DrillSpeed.fast ? vibrationFrequency * drillFastSpeedMultiplier : vibrationFrequency);
                }

            }

        }
        else
        {
            if (drillerVibrationFrequencyParticleSystem.isPlaying)
            {
                drillerVibrationFrequencyParticleSystem.Stop();
            }
            onDrilling = false;
        }

    }

    public void StopDrilling()
    {
        playerAnimator.SetTrigger("isDrillRelease");

        drillerVibrationFrequencyParticleSystem.Stop();
        onDrilling = false;
        /*
        if (drillCollide.Length > 0)
        {
            for (int i = 0; i < drillCollide.Length; ++i)
            {
                drillCollide[i].gameObject.GetComponent<Material>().color = Color.white;
            }
        }
        */
    }
}
