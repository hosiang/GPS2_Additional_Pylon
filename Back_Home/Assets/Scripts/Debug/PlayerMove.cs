using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Vector3 direction = Vector3.zero;
    public float speed;
    public GameObject damageIndicator;
    private float damageDuration = 1f;
    private void Start()
    {
        HideDamageIndicator();
    }
    public void ShowDamageIndicator()
    {
        damageIndicator.SetActive(true);
    }
    public void HideDamageIndicator()
    {
        damageIndicator.SetActive(false);
    }

    private void Update()
    {
        direction = Vector3.zero;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            direction.x += speed;
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            direction.x -= speed;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            direction.z += speed;
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            direction.z -= speed;

        transform.Translate(direction * Time.deltaTime);
    }
    private void PlayerDamaged()
    {
        ShowDamageIndicator();
        CancelInvoke("HideDamageIndicator");
        Invoke("HideDamageIndicator", damageDuration);
    }
}
