using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuSystem : MonoBehaviour
{
    [SerializeField] private Image middleCircleflashEffect;
    [SerializeField] private float alphaValue = 0.5f;
    [SerializeField] private float durationFade = 0.8f;
    private bool isFaded = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        MiddleCircleFlashEffect();
    }

    void MiddleCircleFlashEffect()
    {
        if (middleCircleflashEffect.color.a >= 1)
        {
            isFaded = false;
        }
        else if (middleCircleflashEffect.color.a <= alphaValue)
        {
            isFaded = true;
        }

        if (isFaded && middleCircleflashEffect.color.a <= 1f)
        {
            StartCoroutine("Increase");
        }
        else if (!isFaded && middleCircleflashEffect.color.a >= 1f)
        {
            StartCoroutine("Decrease");
        }
    }

    IEnumerator Decrease()
    {
        for (float i = 1f; i >= alphaValue; i -= 0.01f)
        {
            Color tempColor = middleCircleflashEffect.color;
            tempColor.a = i;
            middleCircleflashEffect.color = tempColor;

            yield return new WaitForSeconds(0.01f);

            if (isFaded && middleCircleflashEffect.color.a <= alphaValue) yield break;
        }
    }

    IEnumerator Increase()
    {
        for (float i = alphaValue; i <= 1f; i += 0.01f)
        {
            Color tempColor = middleCircleflashEffect.color;
            tempColor.a = i;
            middleCircleflashEffect.color = tempColor;

            yield return new WaitForSeconds(0.01f);

            if (!isFaded && middleCircleflashEffect.color.a >= 1f) yield break;
        }
    }
}