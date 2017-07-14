using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bestiary : MonoBehaviour {

    [Header("Technical")]
    public string monsterTag;
    public MonsterBlueprint[] monstersTypes;

    private static string monsterTagStatic;
    private static MonsterBlueprint[] monsters;   

    private void Start()
    {
        monsters = monstersTypes;
        monsterTagStatic = monsterTag;
    }

    public static string GetMonsterTag { get { return monsterTagStatic; } }

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
