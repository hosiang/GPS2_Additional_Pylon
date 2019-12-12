using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length;
    private float width;
    private float startPositionX;
    private float startPositionZ;
    [SerializeField] GameObject mainCamera;
    [SerializeField] private float horizontalParallaxEffect;
    [SerializeField] private float verticalParallaxEffect;

    private void Start()
    {
        startPositionX = transform.position.x;
        length = GetComponent<MeshRenderer>().bounds.size.x;

        startPositionZ = transform.position.z;
        width = GetComponent<MeshRenderer>().bounds.size.z;
    }

    private void FixedUpdate()
    {
        float temporaryX = mainCamera.transform.position.x * (1 - horizontalParallaxEffect);
        float temporaryZ = mainCamera.transform.position.z * (1 - verticalParallaxEffect);

        float distanceX = mainCamera.transform.position.x * horizontalParallaxEffect;
        float distanceZ = mainCamera.transform.position.z * verticalParallaxEffect;
        transform.position = new Vector3(startPositionX + distanceX, transform.position.y, startPositionZ + distanceZ);

        if (temporaryX > startPositionX + length)
        {
            startPositionX += length;
        }
        else if (temporaryX < startPositionX - length)
        {
            startPositionX -= length;
        }

        if (temporaryZ > startPositionZ + width)
        {
            startPositionZ += width;
        }
        else if (temporaryZ < startPositionZ - width)
        {
            startPositionZ -= width;
        }
    }
}