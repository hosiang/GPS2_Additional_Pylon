using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ores : MonoBehaviour
{
    [SerializeField] private Global.OresTypes oresType;
    private float[] AstroidOreProvide = { 3, 5, 4 };

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PickUpOre();
        }
    }
    private void PickUpOre()
    {
        switch (oresType)
        {
            case Global.OresTypes.Ore_No1:
                FindObjectOfType<ShipEntity>().GainOresFromAsteroid(this, Global.OresTypes.Ore_No1, AstroidOreProvide[(int)Global.AstroidType.small]);
                break;
            case Global.OresTypes.Special_Ore:
                FindObjectOfType<ShipEntity>().GainOresFromAsteroid(this, Global.OresTypes.Special_Ore, AstroidOreProvide[(int)Global.AstroidType.big]);
                break;
            case Global.OresTypes.Length:
                FindObjectOfType<ShipEntity>().GainOresFromAsteroid(this, Global.OresTypes.Length, AstroidOreProvide[(int)Global.AstroidType.special]);
                break;
        }
        Destroy(gameObject);
        
    }
}
