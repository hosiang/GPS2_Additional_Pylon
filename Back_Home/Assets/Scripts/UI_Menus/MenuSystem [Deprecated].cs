using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{
    private bool inGame = false;
    private bool isPause = true;

    [SerializeField] GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        CheckPause();
    }

    void CheckPause()
    {
        if (inGame)
        {
            if (!isPause && Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();
            }
            else if (isPause && Input.GetKeyDown(KeyCode.Escape))
            {
                ResumeGame();
            }
        }
    }

    public void NewGame()
    {
        Time.timeScale = 1f;
        inGame = true;
        isPause = false;
        //SceneManager.LoadScene("Kenze");
    }
    public void ContinueGame()
    {
        Time.timeScale = 1f;
        inGame = true;
        isPause = false;
        //SceneManager.LoadScene("Back Home");
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPause = true;
        pauseMenu.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPause = false;
        pauseMenu.SetActive(false);
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
    public void SetInGame(bool value)
    {
        inGame = value;
    }
}
