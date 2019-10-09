using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] float parallaxEffect;
    private float length;
    private float startPosition;
    [SerializeField] GameObject mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float temporary = (mainCamera.transform.position.x * (1 - parallaxEffect));
        float distance = (mainCamera.transform.position.x * parallaxEffect);

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
