using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickMovementDebug : MonoBehaviour {

    [SerializeField] private float speed;

    private Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start() {

        rigidBody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update() {

        int horizontal = 0, vertical = 0;

        if (Input.GetKey(KeyCode.W)) { vertical = 1; }
        else if (Input.GetKey(KeyCode.S)) { vertical = -1; }

        if (Input.GetKey(KeyCode.A)) { horizontal = -1; }
        else if (Input.GetKey(KeyCode.D)) { horizontal = 1; }

        rigidBody.velocity = new Vector3(horizontal * speed * Time.deltaTime, 0, vertical * speed * Time.deltaTime);

    }

}
