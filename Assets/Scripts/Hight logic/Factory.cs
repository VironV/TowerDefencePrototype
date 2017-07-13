using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour {

    public TowerBlueprint[] towersTypes;

    public static TowerBlueprint[] towers;

    private void Start()
    {
        towers = towersTypes;
    }

    public static TowerBlueprint GetTower(string title)
    {
        for (int i=0; i<towers.Length;i++)
        {
            if (towers[i].title == title)
                return towers[i];
        }
        return null;
    }

}
