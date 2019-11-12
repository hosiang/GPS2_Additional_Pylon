using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ores : MonoBehaviour
{
    [SerializeField] private Global.OresTypes oresType;
    private bool isCollectable = false;
    //private float[] AstroidOreProvide = { 3, 5, 4 };

    public void SetOresToColletable(Object requireObject)
    {
        if(requireObject.GetType().Name == nameof(Asteroid))
        {
            isCollectable = true;
        }
        else
        {
            Debug.LogError("Error! Unable to invoking " + nameof(SetOresToColletable) + " function!!!");
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == Global.tag_Player && isCollectable)
        {
            PickUpOre();
        }
    }
    private void PickUpOre()
    {
        FindObjectOfType<ShipEntity>().GainOres(this, oresType, 1);
        Destroy(gameObject);
    }
}
