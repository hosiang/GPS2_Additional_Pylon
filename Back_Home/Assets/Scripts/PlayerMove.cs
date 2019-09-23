using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Vector3 direction = Vector3.zero;
    public float speed;

    private void Update()
    {
        direction = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.D))
            direction.x += speed;
        else if (Input.GetKeyDown(KeyCode.A))
            direction.x -= speed;
        else if (Input.GetKeyDown(KeyCode.W))
            direction.y += speed;
        else if (Input.GetKeyDown(KeyCode.S))
            direction.y -= speed;

        transform.Translate(direction);
    }
}
