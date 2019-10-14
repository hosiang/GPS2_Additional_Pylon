using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitPopUp : MonoBehaviour
{
    [SerializeField] private CanvasGroup quitPopUpCanvasGroup;
    [SerializeField] private CanvasGroup quitButtonCanvasGroup;

    [SerializeField] private GameObject quitConfirmationGameObject;

    private bool isActive = false;

    private void Awake()
    {
        // disable the confirmation pop up
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isActive)
        {
            QuitConfirmation(true);
        }
    }

    /*
    public void QuitConfirmationNo()
    {
        isActive = false;
        quitConfirmationGameObject.SetActive(isActive);
        
        // enable the normal UI
        quitPopUpCanvasGroup.alpha = 1;
        quitPopUpCanvasGroup.interactable = true;
        quitPopUpCanvasGroup.blocksRaycasts = true;
        // disable the quit confirmation UI
        quitPopUpCanvasGroup.alpha = 0;
        quitPopUpCanvasGroup.interactable = false;
        quitPopUpCanvasGroup.blocksRaycasts = false;
        
    }
    */

    public void QuitConfirmation(bool active)
    {
        isActive = active;
        quitConfirmationGameObject.SetActive(active);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    /*
    public void QuitConfirmationPOPUP()
    {
        //reduce the visibility of normal UI, and disable all interaction
        
        quitPopUpCanvasGroup.alpha = 1f;
        quitPopUpCanvasGroup.interactable = true;
        quitPopUpCanvasGroup.blocksRaycasts = true;
        
    }
    */
}
