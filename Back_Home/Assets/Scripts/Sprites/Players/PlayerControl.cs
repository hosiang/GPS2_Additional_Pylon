using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShipEntity))]
public class PlayerControl : MonoBehaviour
{
    private float currentThrustPower;
    [SerializeField] private float thrustPower = 20f;
    [SerializeField] private float minimalThrustPower = 10f;
    [SerializeField]private float maximalThrustPower = 10f;
    private float eachWeightAffectThrustRate;
    public bool isThrust = false;
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
    [SerializeField] private ParticleSystem healing1;
    [SerializeField] private ParticleSystem healing2;

    private BoxCollider playerCollision;
    private Rigidbody playerRigidbody;
    private Transform playerTransform;
    private Vector3 moveDirection = Vector3.zero;
    private float originalDragRate;

    //Drill
    //private enum DrillSpeed { slow, fast };
    //[SerializeField] private DrillSpeed drillSpeed;

    [SerializeField] private Transform drillStart;
    [SerializeField] private Transform drillEnd;
    [SerializeField] private float drillRadius;
    private Collider[] drillCollide;
    Global.AstroidType drillWhichAstroidType;
    private bool onDrilling = false;

    [SerializeField] private float damage;
    [SerializeField] private float vibrationFrequency;
    //[SerializeField] private float drillFastSpeedMultiplier;

    //[SerializeField] HealthSystem healthSystem;
    //[SerializeField] NitroSystem nitroSystem;
    //[SerializeField] WeightSystem weightSystem;

    private ShipEntity shipEntity;
    private BaseSystem baseSystem;
    private InGameMenuManager inGameMenuManager;

    //[SerializeField] UnityEngine.UI.Toggle slowDrillToggle;

    private Vector3 playerOriginVector3 = Vector3.zero;
    private float rotationSpeed = 100f;
    private float facingAngle;
    private float currentFacingAngle;
    private JoystickTouchController joystickTouchController;

    private Vector3 basePosition = Vector3.zero;

    public GameObject damageIndicator;
    private float damageDuration = 1f;

    private bool onHealing = false;

    // Getter
    public bool IsThrust { get { return isThrust; } }
    public bool OnDrilling { get { return onDrilling; } }
    public bool OnHealing { get { return onHealing; } }

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

        VFX_Drill(false);
        VFX_Thrust(false);
        VFX_Healing(false);

        shipEntity = GetComponent<ShipEntity>();
        baseSystem = FindObjectOfType<BaseSystem>();
        inGameMenuManager = FindObjectOfType<InGameMenuManager>();

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

        originalDragRate = playerRigidbody.drag;
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

        if (!isThrust)
        {
            shipEntity.ReplenishNitroPoint(this);
        }

        if (baseSystem.IsPlayerInBase && shipEntity.CurrentHealth < shipEntity.MaximalHealth)
        {
            VFX_Healing(onHealing = true);
        }
        else
        {
            VFX_Healing(onHealing = false);
        }

        DrillCollideDetect();

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
        if( Vector3.Distance(basePosition, transform.position) > Global.gameManager.CurrentPermissiveZoneRadius)
        {
            playerRigidbody.velocity += (basePosition - playerTransform.position.normalized) * playerRigidbody.velocity.magnitude;
            //playerRigidbody.velocity = Vector3.zero;
            //transform.position = transform.position.normalized * Global.gameManager.CurrentPermissiveZoneRadius;
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
        if (!shipEntity.IsOverheat && !onDrilling)
        {
            VFX_Thrust(isThrust = true);
            DragRateSwitch(isThrust);

            playerRigidbody.velocity += transform.forward * (currentThrustPower * Time.deltaTime);

            if(playerRigidbody.velocity.magnitude > maximalThrustPower) // Limit the maximal speed of player
            {
                playerRigidbody.velocity = playerRigidbody.velocity.normalized * maximalThrustPower; 
            }

            shipEntity.NitroReduction();
        }
        else 
        {
            VFX_Thrust(isThrust = false);
        }
    }
    public void ThrustingRelease()
    {
        VFX_Thrust(isThrust = false);
        DragRateSwitch(isThrust);
    }

    private void DragRateSwitch(bool withDrag)
    {
        if (withDrag)
        {
            playerRigidbody.drag = 0.0f;
        }
        else
        {
            playerRigidbody.drag = originalDragRate;
        }
        
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

    private void ControlThrustPowerByWeightRate() // The thrust power will follow current weight rate
    {
        currentThrustPower = thrustPower - (eachWeightAffectThrustRate * shipEntity.CurrentWeight);
    }


    /// <summary>
    /// Energy deplete point style for Weight–Thrust system
    /// </summary>
    //void WeightToNitroConsume()
    //{
    //    
    //    if (weightSystem.GetWeight() <= 0)
    //    {
    //        nitroConsume = 15f;
    //    }
    //    else if (weightSystem.GetWeight() > 0 && weightSystem.GetWeight() <= weightSystem.GetMaxWeight() / 3)
    //    {
    //nitroConsume = 17f;
    //}
    //   else if (weightSystem.GetWeight() > weightSystem.GetMaxWeight() / 3 && weightSystem.GetWeight() <= (weightSystem.GetMaxWeight() / 3) * 2)
    //  {
    // nitroConsume = 19f;
    //  }
    // else if (weightSystem.GetWeight() > (weightSystem.GetMaxWeight() / 3) * 2)
    //  {
    //      nitroConsume = 21f;
    //}

    //}


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

    private void DrillCollideDetect()
    {
        drillCollide = Physics.OverlapCapsule(drillStart.position, drillEnd.position, drillRadius, Global.layer_Astroid);

        if (drillCollide.Length > 0 && shipEntity.CurrentWeight <= shipEntity.MaximalWeight)
        {
            for (int i = 0; i < drillCollide.Length; ++i)
            {
                drillWhichAstroidType = drillCollide[i].gameObject.GetComponentInParent<Asteroid>().GetAstroidType();
                if (drillWhichAstroidType != Global.AstroidType.Ore) // Not allow drill the Broken_Asteroid
                {
                    inGameMenuManager.SetDrillButtonInteractable(true);
                    if (onDrilling)
                    {
                        VFX_Drill(onDrilling);

                        //if (drillerVibrationFrequencyParticleSystem.isStopped)
                        //{
                        //drillerVibrationFrequencyParticleSystem.Play();
                        //}

                        drillCollide[i].gameObject.GetComponentInParent<Asteroid>().Drill(damage, vibrationFrequency);
                    }
                }
            }
        }
        else
        {
            inGameMenuManager.SetDrillButtonInteractable(false);

            VFX_Drill(onDrilling = false);
        }
    }

    public void Drilling()
    {
        if (!onDrilling)
        {
            onDrilling = true;

            playerRigidbody.angularVelocity = Vector3.zero;
            playerRigidbody.velocity = Vector3.zero;
            playerTransform.position = playerTransform.position;
        }

    }

    public void DrillingRelease()
    {
        VFX_Drill(onDrilling = false);

        //drillerVibrationFrequencyParticleSystem.Stop();
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

    private void VFX_Drill(bool toPlay)
    {
        if (toPlay)
        {
            if (particleDrill.isStopped || blingDrill.isStopped || boomDrill.isStopped)
            {
                playerAnimator.SetTrigger("isDrillPress");

                particleDrill.Play();
                blingDrill.Play();
                boomDrill.Play();
            }
        }
        else
        {
            if (particleDrill.isPlaying || blingDrill.isPlaying || boomDrill.isPlaying)
            {
                playerAnimator.SetTrigger("isDrillRelease");

                particleDrill.Stop();
                blingDrill.Stop();
                boomDrill.Stop();
            }
        }
    }

    private void VFX_Thrust(bool toPlay)
    {
        if (toPlay)
        {
            if (mainThruster.isStopped || leftSideThruster.isStopped || rightSideThruster.isStopped)
            {
                playerAnimator.SetTrigger("isThrustPress");

                mainThruster.Play();
                leftSideThruster.Play();
                rightSideThruster.Play();
            }
        }
        else
        {
            if (mainThruster.isPlaying || leftSideThruster.isPlaying || rightSideThruster.isPlaying)
            {
                playerAnimator.SetTrigger("isThrustRelease");

                mainThruster.Stop();
                leftSideThruster.Stop();
                rightSideThruster.Stop();
            }
        }
    }

    private void VFX_Healing(bool toPlay)
    {
        if (toPlay)
        {
            if (healing1.isStopped || healing2.isStopped)
            {
                healing1.Play();
                healing2.Play();
            }
        }
        else
        {
            if (healing1.isPlaying || healing2.isPlaying)
            {
                healing1.Stop();
                healing2.Stop();
            }
        }
        
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ore")
        {
            Destroy(other.gameObject);
        }
    }
    */
}
