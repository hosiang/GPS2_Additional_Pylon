using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourseAmountShow : MonoBehaviour
{
    private BaseSystem baseSystem;
    //private ShipEntity shipEntity;

    //public Text shipOreNo1Text;
    //public Text shipOreNo2Text;
    public Text oreAmountText;
    public Text targetOreAmountText;
    public Text leftOreAmountText;

    //readonly string text_ShipOreNo1 = "Ore No 1 : ";
    //readonly string text_ShipOreNo2 = "Ore No 2 : ";
    readonly string text_OreAmount = "Ore Amount : ";
    readonly string text_TargetOreAmount = "Target Ore Amount : ";
    readonly string text_LeftOreAmount = "Left Ore Amount : ";

    private float LeftOreAmount = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        baseSystem = FindObjectOfType<BaseSystem>();
        //shipEntity = FindObjectOfType<ShipEntity>();
    }

    // Update is called once per frame
    void Update()
    {
        //shipOreNo1Text.text = text_ShipOreNo1 + shipEntity.GetShipOresAmount(Global.OresTypes.Ore_No1);
        //shipOreNo2Text.text = text_ShipOreNo2 + shipEntity.GetShipOresAmount(Global.OresTypes.Special_Ore);

        oreAmountText.text = text_OreAmount + baseSystem.GetBaseStorageOres(Global.OresTypes.Ore_No1);
        targetOreAmountText.text = text_TargetOreAmount + Global.targetOreToWinValue;

        LeftOreAmount = Global.targetOreToWinValue - baseSystem.GetBaseStorageOres(Global.OresTypes.Ore_No1);
        LeftOreAmount = (LeftOreAmount <= 0.0f) ? 0.0f : LeftOreAmount;
        leftOreAmountText.text = text_LeftOreAmount + LeftOreAmount;
    }
}
