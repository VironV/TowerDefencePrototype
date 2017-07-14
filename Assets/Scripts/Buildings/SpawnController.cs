using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

    [Header("Time settings")]
    public float startTime;
    public float betweenSpawn;
    public float betweenWaves;

    [Header("Wave structure settings")]
    public string[] waves;

    private int wavesRemain;
    private int monstersRemain;
    private int waveGraveyard;
    private int waveStartCount;
    private bool wavesEnded;

    public int GetWavesRemain { get { return wavesRemain; } }
    public int GetMonstersRemain { get { return monstersRemain; } }

    void Start()
    {
        wavesEnded = false;
        StartCoroutine(SpawnWaves());
        wavesRemain = waves.Length;
        monstersRemain = wavesRemain == 0 ? 0 : CalculateMonstersRemain(waves[0]);
    }

    private void Update()
    {
        if (waveGraveyard == waveStartCount && wavesEnded)
            GameManager.SetWin();
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startTime);
        wavesRemain = waves.Length;
        for (int i = 0; i < waves.Length; i++)
        {
            monstersRemain = CalculateMonstersRemain(waves[i]);
            waveStartCount = (waveStartCount-waveGraveyard)+ monstersRemain;
            waveGraveyard = 0;
            for (int j = 0; j < waves[i].Length; j++)
            {
                spawnMonster(waves[i][j]);
                yield return new WaitForSeconds(betweenSpawn);
                if (waves[i][j] != '-' && waves[i][j] != '0')
                    monstersRemain--;
            }
            wavesRemain--;
            if (wavesRemain<=0)
                wavesEnded = true;
            yield return new WaitForSeconds(betweenWaves);
            
        }
        wavesEnded = true;
    }

    void spawnMonster(char type)
    {
        GameObject toSpawn = Bestiary.GetMonster(type);
        if (toSpawn!=null)
        {
            Vector3 spawnPosition = transform.position;
            Quaternion spawnRotation = Quaternion.Euler(new Vector3(0, 90, 0));
            GameObject go=Instantiate(toSpawn, spawnPosition, spawnRotation);
            go.GetComponent<MonsterController>().SetSpawner(this);
        }
        else
            return;
    }

    private int CalculateMonstersRemain(string wave)
    {
        int remain = 0;
        for (int i = 0; i < wave.Length; i++)
        {
            if (wave[i] == '-' || wave[i] == '0')
                continue;
            remain++;
        }
        return remain;
    }

    public void AddToGraveyard()
    {
        waveGraveyard++;
    }
}
