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

        if (other.gameObject.tag == Global.tag_Player && isCollectable && shipEntity.CurrentWeight < shipEntity.MaximalWeight)
        {

            shipEntity.GainOres(this, oresType, 1); // Pick up the ore to the ship

            if (oresType == Global.OresTypes.Special_Ore)
            {
                Destroy(gameObject.transform.parent.parent.gameObject);
            }
            else
            {
                float rotation = Random.Range(0.0f, 360.0f);

                GameObject tempGameObject = Instantiate(gainEffect, transform.position, new Quaternion(0.0f, rotation, 0.0f, 0.0f)); // Create a boom effect
                Destroy(tempGameObject, 0.1f); // Destroy the boom effect after 10 second
                Destroy(gameObject);
            }

        }

    }

}
