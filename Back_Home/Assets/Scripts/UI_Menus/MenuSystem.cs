using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{
    private bool InGame = false;
    private bool IsPause = true;

    [SerializeField] GameObject panel;
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
        if (InGame)
        {
            if (!IsPause && Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();
            }
            else if (IsPause && Input.GetKeyDown(KeyCode.Escape))
            {
                ResumeGame();
            }
        }
    }

    public void NewGame()
    {
        Time.timeScale = 1f;
        InGame = true;
        IsPause = false;
        //SceneManager.LoadScene("Kenze");
    }
    public void ContinueGame()
    {
        Time.timeScale = 1f;
        InGame = true;
        IsPause = false;
        //SceneManager.LoadScene("Back Home");
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log($"<color=red>Quit</color>");
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        IsPause = true;
        panel.SetActive(true);
        pauseMenu.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        IsPause = false;
        panel.SetActive(false);
        pauseMenu.SetActive(false);
    }

    public void SetInGame(bool value)
    {
        InGame = value;
    }
}
