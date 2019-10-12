using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrill : MonoBehaviour {

    private enum DrillSpeed { slow, fast };

    [SerializeField] private float damage;
    [SerializeField] private float vibrationFrequency;

    [SerializeField] private float fastSpeedMultiplier;

    [SerializeField] private DrillSpeed drillSpeed;

    //private bool isDestroyed = false;

    //[SerializeField] private GameObject ores;


    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.tag == "Asteroid") {

            other.gameObject.GetComponent<Astroid>().Drill(drillSpeed == DrillSpeed.fast ? damage * fastSpeedMultiplier : damage,
                                                           drillSpeed == DrillSpeed.fast ? vibrationFrequency * fastSpeedMultiplier : vibrationFrequency);

            /*

            Destroy(other.gameObject);

            isDestroyed = true;

            if (isDestroyed) {

                for (int i = 0; i < Random.Range(1, 5); i++) {

                    Vector3 pos = other.transform.position + new Vector3(Random.Range(1, 5), Random.Range(0, 1), Random.Range(0, 1));

                    GameObject go = Instantiate(ores, pos, Quaternion.identity);

                }

                isDestroyed = false;

            }

            */

        }

    }

    public void SwitchDrillSpeed(int drillSpeed)
    {
        if ((DrillSpeed)drillSpeed > DrillSpeed.fast) drillSpeed = (int)DrillSpeed.fast;
        if ((DrillSpeed)drillSpeed < DrillSpeed.slow) drillSpeed = (int)DrillSpeed.slow;

        this.drillSpeed = (DrillSpeed)drillSpeed;
    }

}
