using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourseAmountShow : MonoBehaviour
{
    private BaseSystem baseSystem;
    private ShipEntity shipEntity;

    public Text shipOreNo1Text;
    public Text shipOreNo2Text;
    public Text baseOreNo1Text;
    public Text baseOreNo2Text;

    readonly string text_ShipOreNo1 = "Ore No 1 : ";
    readonly string text_ShipOreNo2 = "Ore No 2 : ";
    readonly string text_BaseOreNo1 = "Ore No 1 : ";
    readonly string text_BaseOreNo2 = "Ore No 2 : ";

    // Start is called before the first frame update
    void Start()
    {
        baseSystem = FindObjectOfType<BaseSystem>();
        shipEntity = FindObjectOfType<ShipEntity>();
    }

    // Update is called once per frame
    void Update()
    {
        shipOreNo1Text.text = text_ShipOreNo1 + shipEntity.GetShipOresAmount(Global.OresTypes.Iron);
        shipOreNo2Text.text = text_ShipOreNo2 + shipEntity.GetShipOresAmount(Global.OresTypes.no2_Ores);
        baseOreNo1Text.text = text_BaseOreNo1 + baseSystem.GetBaseStorageOres(Global.OresTypes.Iron);
        baseOreNo2Text.text = text_BaseOreNo2 + baseSystem.GetBaseStorageOres(Global.OresTypes.no2_Ores);
    }
}
