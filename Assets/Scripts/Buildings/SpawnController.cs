using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

    //TODO: make script to find array of spawner and use them automatically
    public float startTime;
    public float betweenSpawn;
    public float betweenWaves;
    //public int wavesCount;
    //public int monsterCount;

    public string[] waves;
    private int wavesRemain;
    private int monstersRemain;

    public int GetWavesRemain { get { return wavesRemain; } }
    public int GetMonstersRemain { get { return monstersRemain; } }

    void Start()
    {
        StartCoroutine(SpawnWaves());
        wavesRemain = waves.Length;
        monstersRemain = wavesRemain == 0 ? 0 : CalculateMonstersRemain(waves[0]);
    }

    private int CalculateMonstersRemain(string wave)
    {
        int remain = 0;
        for (int i=0; i<wave.Length;i++)
        {
            if (wave[i] == '-' || wave[i] == '0')
                continue;
            remain++;
        }
        return remain;
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startTime);
        wavesRemain = waves.Length;
        for (int i = 0; i < waves.Length; i++)
        {
            monstersRemain = CalculateMonstersRemain(waves[i]);
            for (int j = 0; j < waves[i].Length; j++)
            {
                spawnMonster(waves[i][j]);
                yield return new WaitForSeconds(betweenSpawn);
                if (waves[i][j] != '-' && waves[i][j] != '0')
                    monstersRemain--;
            }
            yield return new WaitForSeconds(betweenWaves);
            wavesRemain--;
        }
        GameManager.AlmostWin();
    }

    void spawnMonster(char type)
    {
        GameObject toSpawn = Bestiary.GetMonster(type);
        if (toSpawn!=null)
        {
            Vector3 spawnPosition = transform.position;
            Quaternion spawnRotation = Quaternion.Euler(new Vector3(0, 90, 0));
            Instantiate(toSpawn, spawnPosition, spawnRotation);
        }
        else
            return;
    }
}
