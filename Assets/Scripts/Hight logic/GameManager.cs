using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public float checksInSec = 2;
    public GameObject castle;
    public GameObject gameoverUI;
    public GameObject winScreen;

    public static bool gameEnded;

    private bool printed = false;
    private static bool waitTillDies;
    private float countdown = 0f;

    private void Start()
    {
        gameEnded = false;
        waitTillDies = false;
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
            //Debug.Log("GAME OVER");
            printed = true;

            castle.GetComponent<CastleController>().Explode();
            gameoverUI.SetActive(true);
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
            //Debug.Log("YOU WON!");
            printed = true;

            winScreen.SetActive(true);
            gameEnded = true;

        }
    }
}
