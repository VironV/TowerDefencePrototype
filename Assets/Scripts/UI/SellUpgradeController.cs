using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellUpgradeController : MonoBehaviour {

    BuildManager buildmanager;
    NodeUI nodeUI;

    void Start()
    {
        buildmanager = BuildManager.instance;
        nodeUI = NodeUI.instance;
    }

    public void SellTower()
    {
        buildmanager.SellTower();
        nodeUI.Hide();
    }
}
