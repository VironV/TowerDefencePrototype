using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

    //TODO: make script to find array of spawner and use them automatically
    public GameObject spawner;
    public float startTime;
    public float betweenSpawn;
    public float betweenWaves;
    //public int wavesCount;
    //public int monsterCount;

    public string[] waves;

	void Start () {
        StartCoroutine(SpawnWaves());
	}

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startTime);
        for (int i = 0; i < waves.Length; i++)
        {
            for (int j = 0; j < waves[i].Length; j++)
            {
                spawnMonster(waves[i][j]);
                yield return new WaitForSeconds(betweenSpawn);
            }
            yield return new WaitForSeconds(betweenWaves);
        }
        GameManager.AlmostWin();
    }

    void spawnMonster(char type)
    {
        GameObject toSpawn = Bestiary.GetMonster(type);
        if (toSpawn!=null)
        {
            Vector3 spawnPosition = spawner.transform.position;
            Quaternion spawnRotation = Quaternion.Euler(new Vector3(0, 90, 0));
            Instantiate(toSpawn, spawnPosition, spawnRotation);
        }
        else
            return;
    }

    /*
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startTime);
        for (int i = 0; i < wavesCount; i++)
        {
            for (int j = 0; j < monsterCount; j++)
            {
                Vector3 spawnPosition = spawner.transform.position;
                Quaternion spawnRotation = Quaternion.Euler(new Vector3(0,90,0));
                GameObject spawned=Instantiate(monster,spawnPosition,spawnRotation);
                yield return new WaitForSeconds(betweenSpawn);
            }
            yield return new WaitForSeconds(betweenWaves);
        }
        GameManager.AlmostWin();
    }
    */
}
