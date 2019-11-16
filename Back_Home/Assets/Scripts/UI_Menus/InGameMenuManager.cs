using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup pauseMenu;
    [SerializeField] private CanvasGroup optionsMenu;
    [SerializeField] private CanvasGroup quitConfirmationCanvasGroup;

    private bool isPause = false;
    private bool optionsMenuBool = false;
    private bool quitPromptBool = false;

    // Start is called before the first frame update
    void Start()
    {
        QuitConfirmation(quitPromptBool);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !quitPromptBool)
        {
            QuitConfirmation(true);
        }
    }

    public void NewGame()
    {
        Time.timeScale = 1f;
        isPause = false;
        //SceneManager.LoadScene("Kenze");
    }
    public void ContinueGame()
    {
        Time.timeScale = 1f;
        isPause = false;
        //SceneManager.LoadScene("Back Home");
    }

    public void OptionsMenu(bool active)
    {
        optionsMenuBool = active;
        optionsMenu.alpha = optionsMenuBool ? 1 : 0;
        optionsMenu.interactable = optionsMenuBool;
        optionsMenu.blocksRaycasts = optionsMenuBool;
    }
    public void PauseMenu(bool active)
    {
        isPause = active;
        Time.timeScale = isPause ? 0f : 1f;
        pauseMenu.alpha = isPause ? 1 : 0;
        pauseMenu.interactable = isPause;
        pauseMenu.blocksRaycasts = isPause;
    }
    public void QuitConfirmation(bool active)
    {
        quitPromptBool = active;
        quitConfirmationCanvasGroup.alpha = quitPromptBool ? 1 : 0;
        quitConfirmationCanvasGroup.interactable = quitPromptBool;
        quitConfirmationCanvasGroup.blocksRaycasts = quitPromptBool;
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log($"<color=red>Quit</color>");
    }
}
