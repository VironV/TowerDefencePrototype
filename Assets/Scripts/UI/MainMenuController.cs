using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    public string firstLevel = "Level 1";
    public Transform cam;
    public float moveDist;
    public float speed;
    public string[] levels;

    public GameObject mainUI;
    public GameObject levelSelecUI;

    private Vector3 target;
    private Vector3 mainPosition;
    private Vector3 levelSelectPosition;

    private void Awake()
    {
        target = cam.position;
        mainPosition = cam.position;
        levelSelectPosition = cam.position + (new Vector3(moveDist, 0, 0));
    }

    private void Update()
    {
        if (target!=cam.position)
        {
            //Debug.Log("target!=current");
            cam.position = Vector3.Lerp(cam.position, target, Time.deltaTime * speed);
        }
    }

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
        MoveCamera(true);
        mainUI.SetActive(false);
        levelSelecUI.SetActive(true);
        //Debug.Log("Opening level selectScreen");
    }

    private void MoveCamera(bool toLevels)
    {
        if (toLevels)
            target = levelSelectPosition;
        else
            target = mainPosition;
    }

    public void LoadLevel(int id)
    {
        SceneManager.LoadScene(levels[id]);
    }

    public void ToMainMenu()
    {
        MoveCamera(false);
        mainUI.SetActive(true);
        levelSelecUI.SetActive(false);
    }

   
}
