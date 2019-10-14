using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenuManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup quitConfirmationCanvasGroup;

    private bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        QuitConfirmation(isActive);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isActive)
        {
            QuitConfirmation(true);
        }
    }

    public void QuitConfirmation(bool active)
    {
        isActive = active;
        quitConfirmationCanvasGroup.alpha = isActive ? 1 : 0;
        quitConfirmationCanvasGroup.interactable = isActive;
        quitConfirmationCanvasGroup.blocksRaycasts = isActive;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
