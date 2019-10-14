using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //private Camera cameraMain;
    private Transform cameraTransform;
    [SerializeField] private Transform playerTransform;
    //private Rigidbody2D playerRigidbody2D;

    private Vector3 cameraOffset = Vector3.zero;
    private float distanceY = 0.0f;

    private float cameraFollowingSpeed = 3f;

    private Vector3 targetPosition;
    //Debug.Log("#Testing || It work !!!"); // For easy to take it again

    void Start()
    {
        cameraTransform = GetComponent<Transform>();
        //playerRigidbody2D = GetComponent<Rigidbody2D>();
        cameraOffset.y = cameraTransform.position.y;
        distanceY = cameraOffset.y - playerTransform.position.y;
    }

    void Update()
    {

        if (Vector3.Distance(cameraTransform.position, playerTransform.position) > distanceY)
        {
            targetPosition = playerTransform.position + cameraOffset;
            targetPosition.y -= playerTransform.position.y;

            cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetPosition, (cameraFollowingSpeed * Mathf.Abs(cameraTransform.position.magnitude - targetPosition.magnitude)) * Time.deltaTime);
        }

    }
}
