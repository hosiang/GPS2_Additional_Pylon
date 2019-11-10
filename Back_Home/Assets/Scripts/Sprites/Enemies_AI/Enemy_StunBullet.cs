using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_StunBullet : MonoBehaviour
{
    Vector3 direction = Vector3.zero;
    [SerializeField] float speed = 10f;
    [SerializeField] float timer;
    [SerializeField] float timerSet = 2f;

    public void InitializeBullet(Vector3 dir, string newTag)
    {
        this.direction = dir;
        this.tag = newTag;
    }

    private void Awake()
    {
        timer = 0f;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        if (timer >= timerSet)
        {
            GameObject.Destroy(gameObject);
        }
        timer += 1.0f * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider collision)
    {
        //Debug.Log($"<color=red>{other.tag}</color>");
        if (this.CompareTag(collision.tag) == false)
        {
            Debug.Log($"<color=red>{collision.tag}</color>");
            GameObject.Destroy(gameObject);
            //collision.GetComponent<Player>().SetStun(true); // Placeholder
        }
    }
}
