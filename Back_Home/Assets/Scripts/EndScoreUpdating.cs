using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScoreUpdating : MonoBehaviour
{
    private ResourseAmountShow resourceAmount;
    private BaseSystem baseSystem;
    private ShipEntity shipEntity;

    public Text currentPlayerScore;
    public Text endScoreUpdateText;

    private readonly string text_Score = "SCORE : ";
    private readonly string text_NewScore = "NEW SCORE : ";
    private readonly string text_OldScore = "OLD SCORE : ";

    // Start is called before the first frame update
    void Start()
    {
        baseSystem = FindObjectOfType<BaseSystem>();
        endScoreUpdateText.text = PlayerPrefs.GetFloat("SCORE: ", 0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //EndGameResult();
    }
    public void EndGameResult()
    {
        Global.userInterfaceActiveManager.SetMenuVisibilitySmoothly(Global.MenusType.TaskCompletedContainer, true);

        float numberOresCurrent = baseSystem.GetFinalStorageOresAmount(Global.OresTypes.Ore_No1);
        float numberOresLast = PlayerPrefs.GetFloat("SCORE: ", 0.0f);
        currentPlayerScore.text = text_Score + numberOresCurrent.ToString();

        if (numberOresCurrent > numberOresLast)
        {
            PlayerPrefs.SetFloat("SCORE: ", numberOresCurrent);
            endScoreUpdateText.text = text_NewScore + numberOresCurrent.ToString();
        }
        else
        {
            
            endScoreUpdateText.text = text_OldScore + numberOresLast.ToString();
        }
    }
}
