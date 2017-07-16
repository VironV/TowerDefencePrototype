using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour, ISpawn {

    [Header("Time settings")]
    [Range(0,25)]
    public float startTime;
    [Range(0, 1f)]
    public float betweenSpawn;
    [Range(0, 25)]
    public float betweenWaves;

    [Header("Wave structure settings")]
    public string[] waves;

    private int wavesRemain;
    private int monstersRemain;
    private int graveyard;
    private int monsterCountOverall;
    private bool wavesEnded;

    public int GetWavesRemain { get { return wavesRemain; } }
    public int GetMonstersRemain { get { return monstersRemain; } }

    void Start()
    {
        wavesEnded = false;
        StartCoroutine(SpawnWaves());
        wavesRemain = waves.Length;
        monstersRemain = wavesRemain == 0 ? 0 : CalculateMonstersRemain(waves[0]);
        graveyard = 0;
        monsterCountOverall = 0;
    }

    private void Update()
    {
        if (graveyard == monsterCountOverall && wavesEnded)
            GameManager.SetWin();
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
        wavesEnded = true;
    }

    void spawnMonster(char type)
    {
        GameObject toSpawn = Bestiary.GetMonster(type);
        if (toSpawn!=null)
        {
            monsterCountOverall++;
            Vector3 spawnPosition = transform.position;
            Quaternion spawnRotation = Quaternion.Euler(new Vector3(0, 90, 0));
            GameObject go=Instantiate(toSpawn, spawnPosition, spawnRotation);
            go.GetComponent<MonsterBehaviour>().SetSpawner(this);
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
        graveyard++;
    }
}
