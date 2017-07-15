using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (LevelOverlayController)) ]
public class GameManager : MonoBehaviour {

    [Header("Technical")]
    public float endChecksInSec = 2;
    public GameObject castle;
    public GameObject levelOverlay;

    private static bool gameEnded;

    private LevelOverlayController lvlOVerlay;
    private bool workIsDone = false;
    private static bool win;
    private GameObject[] lastMonsters;

    public static bool IsGameEnded { get { return gameEnded; } }

    private void Start()
    {
        gameEnded = false;
        win = false;
        lvlOVerlay = levelOverlay.GetComponent<LevelOverlayController>();
    }

    public static void SetWin()
    {
        win = true;
    }

    void Update()
    {
        if (PlayerStats.Health <= 0)
        {
            GameOver();
            return;
        }

        if (win)
            Win();
    }

    void GameOver()
    {
        if (!workIsDone)
        {
            lvlOVerlay.SetLosePanel();
            workIsDone = true;
            castle.GetComponent<CastleController>().Explode();
            gameEnded = true;
        }
    }

    void Win()
    {
        if (!workIsDone)
        {
            lvlOVerlay.SetWinPanel();
            workIsDone = true;
            gameEnded = true;

        }
    }
}
