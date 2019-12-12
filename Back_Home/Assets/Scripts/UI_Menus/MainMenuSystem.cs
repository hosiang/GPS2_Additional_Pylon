using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.LWRP;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuSystem : MonoBehaviour
{
    [SerializeField] private float alphaValue = 0.5f;
    [SerializeField] private float durationFade = 0.8f;

    [SerializeField] private Image emergencyFlash;
    [SerializeField] private Image middleCircleflashEffect;

    [SerializeField] private LightweightRenderPipelineAsset qualityLow;
    [SerializeField] private LightweightRenderPipelineAsset qualityMedium;
    [SerializeField] private LightweightRenderPipelineAsset qualityHigh;

    private bool isFaded = false;

    private void Awake()
    {
        SetScreenSize();
        SetGameQuality(0);
    }

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

    private void SetScreenSize()
    {
        if (Global.screenSize_Height == 0 && Global.screenSize_Width == 0)
        {
            int eachScreen_Width = Screen.width / 8;
            int eachScreen_Height = Screen.height / 8;

            Global.screenSize_Width = Screen.width;
            Global.screenSize_Height = Screen.height;

            Global.screenResolution_Width_Low = eachScreen_Width * 3;
            Global.screenResolution_Height_Low = eachScreen_Height * 3;

            Global.screenResolution_Width_Medium = eachScreen_Width * 6;
            Global.screenResolution_Height_Medium = eachScreen_Height * 6;

            Global.screenResolution_Width_High = eachScreen_Width * 8;
            Global.screenResolution_Height_High = eachScreen_Height * 8;
        }
    }

    public void SetGameQuality(int qualityIndex)
    {
        switch (qualityIndex)
        {
            case 0:
                Debug.Log("Yeah");
                Screen.SetResolution(Global.screenResolution_Width_Low, Global.screenResolution_Height_Low, true);
                GraphicsSettings.renderPipelineAsset = qualityLow;
                break;
            case 1:
                Screen.SetResolution(Global.screenResolution_Width_Medium, Global.screenResolution_Height_Medium, true);
                GraphicsSettings.renderPipelineAsset = qualityMedium;
                break;
            case 2:
                Screen.SetResolution(Global.screenResolution_Width_High, Global.screenResolution_Height_High, true);
                GraphicsSettings.renderPipelineAsset = qualityHigh;
                break;
        }
    }

}