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

    private MenuCameraMover mover;

    private void Start()
    {
        mover = cameraGO.GetComponent<MenuCameraMover>();
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

   
}
