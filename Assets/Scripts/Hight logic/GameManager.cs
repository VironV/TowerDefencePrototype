using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public float checksInSec = 2;
    public GameObject castle;
    public GameObject levelOverlay;
    //public GameObject gameoverUI;
    //public GameObject winScreen;

    public static bool gameEnded;

    private LevelOverlay lvlOVerlay;
    private bool printed = false;
    private static bool waitTillDies;
    private float countdown = 0f;

    private void Start()
    {
        gameEnded = false;
        waitTillDies = false;
        lvlOVerlay = levelOverlay.GetComponent<LevelOverlay>();
    }

    void Update() {

        if (PlayerStats.Health <= 0)
        {
            GameOver();
            return;
        }

        if (waitTillDies)
        {
            if (countdown <= 0)
            {
                GameObject go = GameObject.FindGameObjectWithTag("Monster");
                if (go == null)
                {
                    Win();
                }

                countdown = 1 / checksInSec;
            }
            countdown -= Time.deltaTime;
        }
    }

    void GameOver()
    {
        if (!printed)
        {
            lvlOVerlay.SetLosePanel();
            //gameoverUI.SetActive(true);
            //Debug.Log("GAME OVER");
            printed = true;

            castle.GetComponent<CastleController>().Explode();
            
            gameEnded = true;
        }
    }

    public static void AlmostWin()
    {
        waitTillDies = true;
    }

    void Win()
    {
        if (!printed)
        {
            lvlOVerlay.SetWinPanel();
            //winScreen.SetActive(true);
            //Debug.Log("YOU WON!");
            printed = true;
            gameEnded = true;

        }
    }
}
