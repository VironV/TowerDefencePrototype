using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelOverlay : MonoBehaviour {

    public GameObject pauseUI;
    public GameObject winUI;
    public GameObject loseUI;

    public string nextLevel;
    public string mainMenuScene;

    private bool gameEnded;

    private void Start()
    {
        gameEnded = false;
    }

    public void SetWinPanel()
    {
        gameEnded = true;
        pauseUI.SetActive(false);
        loseUI.SetActive(false);
        winUI.SetActive(true);
    }

    public void SetLosePanel()
    {
        gameEnded = true;
        pauseUI.SetActive(false);
        winUI.SetActive(false);
        loseUI.SetActive(true);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameEnded)
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        pauseUI.SetActive(!pauseUI.activeSelf);

        if (pauseUI.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void Retry()
    {
        if (pauseUI.activeSelf)
            Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        if (pauseUI.activeSelf)
            Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }
}
