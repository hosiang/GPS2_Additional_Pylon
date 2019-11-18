using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuSystem : MonoBehaviour
{
    [SerializeField] private float alphaValue = 0.5f;
    [SerializeField] private float durationFade = 0.8f;

    [SerializeField] private Image emergencyFlash;
    [SerializeField] private Image middleCircleflashEffect;

    private bool isFaded = false;

    void Update()
    {
        BoolControl();

        if (middleCircleflashEffect.color.a >= 1f || middleCircleflashEffect.color.a <= alphaValue)
        {
            StartCoroutine("FadeInOut");
        }
    }

    void BoolControl()
    {
        if (middleCircleflashEffect.color.a >= 1)
        {
            isFaded = false;
        }
        else if (middleCircleflashEffect.color.a <= alphaValue)
        {
            isFaded = true;
        }
    }

    IEnumerator FadeInOut()
    {
        if (!isFaded)
        {
            for (float i = 1f; i >= alphaValue; i -= 0.01f)
            {
                Color tempColor = middleCircleflashEffect.color;
                tempColor.a = i;
                middleCircleflashEffect.color = tempColor;

                Color temptempColor = emergencyFlash.color;
                temptempColor.a = i;
                emergencyFlash.color = temptempColor;

                yield return new WaitForSeconds(0.01f);
            }
        }
        else if (isFaded)
        {
            for (float i = alphaValue; i <= 1f; i += 0.01f)
            {
                Color tempColor = middleCircleflashEffect.color;
                tempColor.a = i;
                middleCircleflashEffect.color = tempColor;

                Color temptempColor = emergencyFlash.color;
                temptempColor.a = i;
                emergencyFlash.color = temptempColor;

                yield return new WaitForSeconds(0.01f);
            }
        }
    }

    public void NewGame()
    {
        SceneManager.LoadScene((int)Global.GameSceneIndex.Level_01);
    }

    public void Resume()
    {

    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log($"<color=red>Quit</color>");
    }
}