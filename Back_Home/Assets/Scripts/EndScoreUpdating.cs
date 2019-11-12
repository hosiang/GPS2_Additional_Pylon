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


    // Start is called before the first frame update
    void Start()
    {
        baseSystem = FindObjectOfType<BaseSystem>();
        endScoreUpdateText.text = PlayerPrefs.GetFloat("SCORE: ", 0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        EndGameResult();
    }
    public void EndGameResult()
    {
        float numberOres = baseSystem.GetBaseStorageOres(Global.OresTypes.Ore_No1);
        currentPlayerScore.text = numberOres.ToString();

        if (numberOres > PlayerPrefs.GetFloat("SCORE: ", 0))
        {
            PlayerPrefs.SetFloat("SCORE: ", numberOres);
            endScoreUpdateText.text = numberOres.ToString();
        }
    }
}
