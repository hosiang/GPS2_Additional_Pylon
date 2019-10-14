using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAsteroidGenerator : MonoBehaviour
{
    [SerializeField] private GameObject asteroid;
    private float maxR = 30f; // spawn within the circle
    private float minR = 20f;

    private void Start()
    {
        for (int i = 0; i<20; i++)
        {
            float angle = Random.Range(0, Mathf.PI * 2); 
            //float angle = i * Mathf.PI * 2f / 20;
            Vector3 newPos = new Vector3(Mathf.Cos(angle) * maxR, 0, Random.Range(Mathf.Sin(angle) * minR, Mathf.Sin(angle) * maxR));
            GameObject go = Instantiate(asteroid, newPos, Quaternion.identity);
        }
    }
}
