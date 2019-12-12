using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenController : MonoBehaviour
{
    private Animator splashScreenAnimator;
    private float fixedSeeLogoTimer;
    private float fixedSeeLogoTime = 1.0f;

    private enum SplashState { KDU_Logo, ThreeA_Production_Logo };
    private SplashState splashState = SplashState.KDU_Logo;
    private void Start()
    {
        splashScreenAnimator = this.GetComponent<Animator>();
    }

    private void Update()
    {
        
        fixedSeeLogoTimer += 1.0f * Time.deltaTime;
        if (Input.anyKeyDown && splashState <= SplashState.ThreeA_Production_Logo && fixedSeeLogoTimer > fixedSeeLogoTime)
        {
            switch (splashState)
            {
                case SplashState.KDU_Logo:
                    splashScreenAnimator.SetTrigger(Global.nameAnimatorTrigger_SplashScreen_PassKDU_Logo);
                    break;
                case SplashState.ThreeA_Production_Logo:
                    splashScreenAnimator.SetTrigger(Global.nameAnimatorTrigger_SplashScreen_Pass3A_Production_Logo);
                    break;
            }
            splashState++;
        }
    }

    public void ResetFixedTimeToSeeLogo()
    {
        fixedSeeLogoTimer = 0.0f;
    }

    public void EndSplashScreen()
    {
        SceneManager.LoadSceneAsync((int)Global.GameSceneIndex.MainMenu);
    }

}
