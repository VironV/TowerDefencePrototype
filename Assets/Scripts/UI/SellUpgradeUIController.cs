using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellUpgradeUIController : MonoBehaviour {

    private BuildManager buildmanager;
    private NodeUISetter nodeUI;

    void Start()
    {
        buildmanager = BuildManager.GetInstance;
        nodeUI = buildmanager.nodeUISetter;
    }

    //
    // Buttons
    //
    public void SellTower()
    {
        buildmanager.SellTower();
        nodeUI.Hide();
    }

    public void UpgradeTower()
    {
        buildmanager.UpgradeTower();
        nodeUI.Hide();
    }
}
