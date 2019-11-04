using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField] private Transform target;

    [SerializeField] private Vector3 offset;

    [SerializeField] private bool horizontalWall;
    [SerializeField] private bool verticalWall;

    [SerializeField] private float smoothSpeed;

    void FixedUpdate() {

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        //smoothedPosition.z = transform.position.z;
        //smoothedPosition.x = transform.position.x;

        transform.position = smoothedPosition;
    }

}
