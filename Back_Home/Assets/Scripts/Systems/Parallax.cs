using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length;
    private float startPosition;
    [SerializeField] GameObject mainCamera;
    [SerializeField] private float parallaxEffect;

    private void Start()
    {
        startPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float temporary = mainCamera.transform.position.x * (1 - parallaxEffect);

        float distance = mainCamera.transform.position.x * parallaxEffect;
        transform.position = new Vector3(startPosition + distance, transform.position.y, transform.position.z);

        if (temporary > startPosition + length)
        {
            startPosition += length;
        }
        else if (temporary < startPosition - length)
        {
            startPosition -= length;
        }
    }
}