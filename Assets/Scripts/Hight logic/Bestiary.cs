using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bestiary : MonoBehaviour {

    public MonsterBlueprint[] monstersTypes;
    public string tag;

    public static MonsterBlueprint[] monsters;
    public static string monsterTag;

    private void Start()
    {
        monsters = monstersTypes;
        monsterTag = tag;
    }

    public static GameObject GetMonster(char type)
    {
        for (int i=0;i<monsters.Length;i++)
        {
            if (monsters[i].code == type)
            {
                return monsters[i].prefab;
            }
        }
        return null;
    }
}
