using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellUpgradeUIController : MonoBehaviour {

    private BuildManager buildmanager;
    private NodeUISetter nodeUI;

    void Start()
    {
        buildmanager = BuildManager.GetInstance;
        nodeUI = NodeUISetter.GetInstance;
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
