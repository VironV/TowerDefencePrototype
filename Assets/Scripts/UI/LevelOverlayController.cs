using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelOverlayController : MonoBehaviour {

    [Header("Settings")]
    public string nextLevel;

    [Header("Technical")]
    public string mainMenuScene;
    public GameObject pauseUI;
    public GameObject winUI;
    public GameObject loseUI;

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
            PauseToggle();
        }
    }

    //
    // Buttons
    //
    public void PauseToggle()
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
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        if (pauseUI.activeSelf)
            Time.timeScale = 1f;
        LoadScene(mainMenuScene);
    }

    public void NextLevel()
    {
        LoadScene(nextLevel);
    }


    //
    // Loading scenes
    //
    private void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
        PlayerStats.ResetStats();
    }

    private void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
        PlayerStats.ResetStats();
    }
}
