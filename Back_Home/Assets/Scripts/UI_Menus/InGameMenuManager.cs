using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup pauseMenu;
    [SerializeField] private CanvasGroup optionsMenu;
    [SerializeField] private CanvasGroup returnToMainMenuConfirmation;
    [SerializeField] private CanvasGroup quitConfirmation;

    [SerializeField] private CustomButton drillButton;
    [SerializeField] private CustomButton thrustButton;

    private bool isPause = false;
    private bool optionsMenuBool = false;
    private bool returnMainMenuPromptBool = false;
    private bool quitPromptBool = false;

    public bool drillButtonInteractable => drillButton.interactable;
    public bool thrustButtonInteractable => thrustButton.interactable;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        //QuitConfirmation(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitConfirmation(true);
        }
    }

    public void NewGame()
    {
        Time.timeScale = 1f;
        isPause = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //SceneManager.LoadScene("Kenze");
    }
    public void ContinueGame()
    {
        Time.timeScale = 1f;
        isPause = false;
        //SceneManager.LoadScene("Back Home");
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        isPause = false;

        SceneManager.LoadScene((int)Global.GameSceneIndex.MainMenu);
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
    public void ReturnToMainMenuConfirmation(bool active)
    {
        returnMainMenuPromptBool = active;
        returnToMainMenuConfirmation.alpha = returnMainMenuPromptBool ? 1 : 0;
        returnToMainMenuConfirmation.interactable = returnMainMenuPromptBool;
        returnToMainMenuConfirmation.blocksRaycasts = returnMainMenuPromptBool;
    }
    public void QuitConfirmation(bool active)
    {
        quitPromptBool = active;
        quitConfirmation.alpha = quitPromptBool ? 1 : 0;
        quitConfirmation.interactable = quitPromptBool;
        quitConfirmation.blocksRaycasts = quitPromptBool;
    }

    public void SetDrillButtonInteractable(bool interactable)
    {
        drillButton.interactable = interactable;
    }

    public void SetThrustButtonInteractable(bool interactable)
    {
        thrustButton.interactable = interactable;
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
