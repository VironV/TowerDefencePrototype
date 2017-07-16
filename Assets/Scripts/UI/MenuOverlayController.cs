using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(MenuCameraMover))]
public class MenuOverlayController : MonoBehaviour {

    [Header("Settings")]
    public string firstLevel = "Level 1";

    [Header("Technical")]
    public string[] levels;
    public GameObject cameraGO;
    public GameObject mainUI;
    public GameObject levelSelecUI;
    public GameObject infoPanel;

    private MenuCameraMover mover;

    private void Start()
    {
        mover = cameraGO.GetComponent<MenuCameraMover>();
        infoPanel.SetActive(false);

        Resolution[] resolutions2 = Screen.resolutions;
        foreach (Resolution res in resolutions2)
        {
            if (res.width < 1023 || res.height < 768)
            {
                if (Screen.fullScreen == false)
                {
                    Screen.SetResolution(1280, 768, false);
                }

            }

        }
    }

    //
    // Buttons
    //
    public void StartLevel()
    {
        SceneManager.LoadScene(firstLevel);
    }

    public void Exit()
    {
        Debug.Log("Exiting...");
        Application.Quit();
    }

    public void LevelSelect()
    {
        mover.MoveCamera(true);
        mainUI.SetActive(false);
        levelSelecUI.SetActive(true);
    }

    public void LoadLevel(int id)
    {
        SceneManager.LoadScene(levels[id]);
    }

    public void ToMainMenu()
    {
        mover.MoveCamera(false);
        mainUI.SetActive(true);
        levelSelecUI.SetActive(false);
    }

    public void HelpInfo()
    {
        if (infoPanel.activeSelf)
            infoPanel.SetActive(false);
        else
            infoPanel.SetActive(true);
    }

   
}
