using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrill : MonoBehaviour
{
    private bool isDestroyed = false;

    [SerializeField] private GameObject ores;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Asteroid")
        {
            Destroy(other.gameObject);
            isDestroyed = true;
            if (isDestroyed)
            {
                for (int i = 0; i < Random.Range(1,5); i++)
                {
                    Vector3 pos = other.transform.position + new Vector3(Random.Range(1, 5), Random.Range(0, 1), Random.Range(0, 1));
                    GameObject go = Instantiate(ores, pos, Quaternion.identity);
                }
                isDestroyed = false;
            }
        }
    }
}
