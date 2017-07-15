using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpawnController))]
public class SpawnerInfoPanel : MonoBehaviour {

    [Header("Technical")]
    public Text monsterText;
    public Text waveText;
    public GameObject spawnerGO;

    private int monstersRemain;
    private int waveRemain;
    private SpawnController spawner;

    private void Start()
    {
        spawner = spawnerGO.GetComponent<SpawnController>();
    }

    void Update()
    {
        if (spawner==null)
        {
            Debug.Log("No spawner!");
            return;
        }
        monsterText.text = spawner.GetMonstersRemain.ToString();
        waveText.text = spawner.GetWavesRemain.ToString();
    }
}
