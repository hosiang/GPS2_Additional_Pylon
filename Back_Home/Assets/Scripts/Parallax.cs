using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] float parallaxEffect;
    private float length;
    private float startPosition;
    [SerializeField] GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position.z;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float temporary = (camera.transform.position.x * (1 - parallaxEffect));
        float distance = (camera.transform.position.x * parallaxEffect);

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
