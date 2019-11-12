using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ores : MonoBehaviour
{
    [SerializeField] private Global.OresTypes oresType;
    private ShipEntity shipEntity;
    private bool isCollectable = false;

    [SerializeField] private GameObject gainEffect;
    //private float[] AstroidOreProvide = { 3, 5, 4 };

    private void Start()
    {
        shipEntity = FindObjectOfType<ShipEntity>();
    }


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
        if (other.gameObject.tag == Global.tag_Player && isCollectable && shipEntity.CurrentWeight < shipEntity.MaximalWeight)
        {
            Debug.Log("In");
            other.gameObject.transform.parent.GetComponentInParent<ShipEntity>().GainOres(this, oresType, 1);
            if(oresType == Global.OresTypes.Special_Ore)
            {
                Destroy(gameObject.transform.parent.parent.gameObject);
            }
            else
            {
                GameObject tempGameObject = Instantiate(gainEffect, transform.position, transform.rotation);
                Destroy(tempGameObject, 0.1f); // Destroy the boom effect after 10 second
                Destroy(gameObject);
            }
            
            //PickUpOre();
        }
    }
    private void PickUpOre()
    {
        FindObjectOfType<ShipEntity>().GainOres(this, oresType, 1);
        Destroy(gameObject);
    }
}
