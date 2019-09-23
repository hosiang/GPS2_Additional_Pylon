using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float thrustPower;
    [SerializeField] private float rotateSpeed;

    private BoxCollider playerCollision;
    private Rigidbody playerRigidbody;
    private Vector3 moveDirection = Vector3.zero;

    [SerializeField] private float heatAmount;
    [SerializeField] private float maxHeatAmount;

    // Start is called before the first frame update
    void Start()
    {
        playerCollision = GetComponent<BoxCollider>();
        playerRigidbody = GetComponent<Rigidbody>();

        heatAmount = GetComponent<HeatSystem>().getHeatAmount();
        maxHeatAmount = GetComponent<HeatSystem>().getMaxHeatAmount();
    }

    // Update is called once per frame
    void Update()
    {
        Rotation();
    }

    private void OnCollisionEnter(Collision collision)
    {
        heatAmount += 1;
    }

    void Rotation()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (transform.rotation.y > 90)
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
                //transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
            }
            else if (transform.rotation.y < 90)
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
            }
            else
            {

            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (transform.rotation.y > 90)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else if (transform.rotation.y < 90)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else
            {

            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (transform.rotation.y > 180)
            {
                transform.rotation = Quaternion.Euler(0, -180, 0);
            }
            else if (transform.rotation.y < 180)
            {
                transform.rotation = Quaternion.Euler(0, -180, 0);
            }
            else
            {

            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (transform.rotation.y > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (transform.rotation.y < 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {

            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (heatAmount < maxHeatAmount)
            {
                playerRigidbody.AddForce(transform.right * thrustPower);
                heatAmount += 10;
            }
            else
            {

            }
        }
    }
}
