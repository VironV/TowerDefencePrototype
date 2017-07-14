using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TowerBlueprint{

    [Header("Settings")]
    public int cost;
    public int sellCost;
    public int upgradeCost;
    public string title;

    [Header("Techical")]
    public GameObject prefab;
}
