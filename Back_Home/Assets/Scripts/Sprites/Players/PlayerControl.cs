using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private float currentThrustPower;
    [SerializeField] private float thrustPower = 20f;
    [SerializeField] private float minimalThrustPower = 10f;
    private float eachWeightAffectThrustRate;
    [SerializeField] private float rotateSpeed = 90f;
    [SerializeField] private float nitroConsume = 15f;
    //[SerializeField] private ParticleSystem drillerVibrationFrequencyParticleSystem;

    // Particle systems
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private ParticleSystem mainThruster;
    [SerializeField] private ParticleSystem leftSideThruster;
    [SerializeField] private ParticleSystem rightSideThruster;
    [SerializeField] private ParticleSystem particleDrill;
    [SerializeField] private ParticleSystem blingDrill;
    [SerializeField] private ParticleSystem boomDrill;

    private BoxCollider playerCollision;
    private Rigidbody playerRigidbody;
    private Transform playerTransform;
    private Vector3 moveDirection = Vector3.zero;

    //Drill
    //private enum DrillSpeed { slow, fast };
    //[SerializeField] private DrillSpeed drillSpeed;

    [SerializeField] private Transform drillStart;
    [SerializeField] private Transform drillEnd;
    [SerializeField] private float drillRadius;
    private bool onDrilling = false;
    private Collider[] drillCollide;

    [SerializeField] private float damage;
    [SerializeField] private float vibrationFrequency;
    [SerializeField] private float drillFastSpeedMultiplier;

    //[SerializeField] HealthSystem healthSystem;
    //[SerializeField] NitroSystem nitroSystem;
    //[SerializeField] WeightSystem weightSystem;

    private ShipEntity shipEntity;

    //[SerializeField] UnityEngine.UI.Toggle slowDrillToggle;

    private Vector3 playerOriginVector3 = Vector3.zero;
    private float rotationSpeed = 100f;
    private float facingAngle;
    private float currentFacingAngle;
    private JoystickTouchController joystickTouchController;

    private Vector3 basePosition = Vector3.zero;

    public GameObject damageIndicator;
    private float damageDuration = 1f;
    public bool isThrust = false;

    private void Awake()
    {
        if(!(joystickTouchController = FindObjectOfType<JoystickTouchController>()))
        {
            joystickTouchController = new JoystickTouchController();
            Debug.LogError("Error! Joystick are undefine now! Please comfirm to put in the JoystickTouchController.cs in you scene!");
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        HideDamageIndicator();

        mainThruster.Stop();
        //leftSideThruster.Stop();
        //rightSideThruster.Stop();
        particleDrill.Stop();
        blingDrill.Stop();
        boomDrill.Stop();

        shipEntity = GetComponent<ShipEntity>();

        playerCollision = GetComponent<BoxCollider>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerTransform = GetComponent<Transform>();

        basePosition = FindObjectOfType<BaseSystem>().transform.position;

        //slowDrillToggle.isOn = (drillSpeed == DrillSpeed.slow) ? true : false;

        //drillerVibrationFrequencyParticleSystem.Stop();

        eachWeightAffectThrustRate = (thrustPower - minimalThrustPower) / shipEntity.MaximalWeight;
        ControlThrustPowerByWeightRate(); // The thrust power will follow current weight rate

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
            //WeightToNitroConsume();
        }

        CircularEdgeWallEffect();
        //ControlThrustPowerByWeightRate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            PlayerDamaged();
            shipEntity.TakeDamage(50f);
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
        if (!shipEntity.IsOverheat && !isThrust)
        {
            playerAnimator.SetTrigger("isThrustPress");
            mainThruster.Play();
            leftSideThruster.Play();
            rightSideThruster.Play();

            playerRigidbody.velocity += transform.forward * (thrustPower * Time.deltaTime);

            playerRigidbody.velocity += transform.forward * (currentThrustPower * Time.deltaTime);
            //playerRigidbody.AddForce(transform.forward * thrustPower);
            shipEntity.NitroReduction();
            isThrust = true;
        }
        else
        {
            if (mainThruster.isPlaying || leftSideThruster.isPlaying || rightSideThruster.isPlaying)
            {
                mainThruster.Stop();
                leftSideThruster.Stop();
                rightSideThruster.Stop();
                isThrust = false;
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
            if (shipEntity.CurrentWeight > 0)
            {
                playerRigidbody.AddForce(transform.right * currentThrustPower);
                shipEntity.NitroReduction();
            }

            //if (heatSystem.GetHeatAmount() < heatSystem.GetMaxHeatAmount())
            //{
            //    playerRigidbody.AddForce(transform.right * thrustPower);
            //    heatSystem.AddHeatAmount(heatCollisionThrustRate);
            //}
            //else {}
        }
    }

    private void ControlThrustPowerByWeightRate()
    {
        currentThrustPower = thrustPower - (eachWeightAffectThrustRate * shipEntity.CurrentWeight); // The thrust power will follow current weight rate
    }

    /*
    /// <summary>
    /// Energy deplete point style for Weight–Thrust system
    /// </summary>
    void WeightToNitroConsume()
    {
        /*
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
    */

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

    //public void SwitchDrillSpeed(int whichDrillSpeed)
    //{
    //    drillSpeed = (DrillSpeed)whichDrillSpeed;
    //}

    public void DrillCollideDetect()
    {

        drillCollide = Physics.OverlapCapsule(drillStart.position, drillEnd.position, drillRadius, Global.layer_Astroid);

        if (drillCollide.Length > 0)
        {
            for (int i = 0; i < drillCollide.Length; ++i)
            {
                Global.AstroidType tempAstroidType = drillCollide[i].gameObject.GetComponentInParent<Asteroid>().GetAstroidType();
                if (tempAstroidType != Global.AstroidType.Special && tempAstroidType != Global.AstroidType.Ore)
                {
                    if (particleDrill.isStopped || blingDrill.isStopped || boomDrill.isStopped)
                    {
                        particleDrill.Play();
                        blingDrill.Play();
                        boomDrill.Play();

                        playerRigidbody.angularVelocity = Vector3.zero;
                        playerRigidbody.velocity = Vector3.zero;
                        playerTransform.position = playerTransform.position;
                    }

                    onDrilling = true;

                    

                    //if (drillerVibrationFrequencyParticleSystem.isStopped)
                    {
                        //drillerVibrationFrequencyParticleSystem.Play();
                    }

                    drillCollide[i].gameObject.GetComponentInParent<Asteroid>().Drill(damage, vibrationFrequency);
                }

            }

        }
        else
        {
            if (particleDrill.isPlaying || blingDrill.isPlaying || boomDrill.isPlaying)
            {
                particleDrill.Stop();
                blingDrill.Stop();
                boomDrill.Stop();
            }

            //if (drillerVibrationFrequencyParticleSystem.isPlaying)
            {
                //drillerVibrationFrequencyParticleSystem.Stop();
            }
            onDrilling = false;
        }

    }

    public void StopDrilling()
    {
        playerAnimator.SetTrigger("isDrillRelease");

        if (particleDrill.isPlaying || blingDrill.isPlaying || boomDrill.isPlaying)
        {
            particleDrill.Stop();
            blingDrill.Stop();
            boomDrill.Stop();
        }

        //drillerVibrationFrequencyParticleSystem.Stop();
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
    private void ShowDamageIndicator()
    {
        damageIndicator.SetActive(true);
    }
    private void HideDamageIndicator()
    {
        damageIndicator.SetActive(false);
    }
    private void PlayerDamaged()
    {
        ShowDamageIndicator();
        CancelInvoke("HideDamageIndicator");
        Invoke("HideDamageIndicator", damageDuration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ore")
        {
            Destroy(other.gameObject);
        }
    }

}
