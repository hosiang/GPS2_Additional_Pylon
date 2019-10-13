using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] Transform[] backgrounds;
    [SerializeField] float smoothing = 1f;
    private float[] parallaxScales;

    private Transform mainCamera;
    private Vector3 previousCameraPosition;

    private void Awake()
    {
        mainCamera = Camera.main.transform;
    }

    private void Start()
    {
        previousCameraPosition = mainCamera.position;

        parallaxScales = new float[backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1; ;
        }
    }

    private void Update()
    {
        for ( int i = 0; i < backgrounds.Length; i++)
        {
            float parallax = (previousCameraPosition.x - mainCamera.position.x) * parallaxScales[i];

            float backgroundTargetPositionX = backgrounds[i].position.x + parallax;

            Vector3 backgroundTargetPosition = new Vector3(backgroundTargetPositionX, backgrounds[i].position.y, backgrounds[i].position.z);

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPosition, smoothing * Time.deltaTime);
        }

        previousCameraPosition = mainCamera.position;
    }
}