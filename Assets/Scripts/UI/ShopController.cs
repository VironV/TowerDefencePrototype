using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour {

    BuildManager buildmanager;
    NodeUI nodeUI;

    private TowerBlueprint standartTower;
    private TowerBlueprint missleTower;

    void Start()
    {
        buildmanager = BuildManager.instance;
        nodeUI = NodeUI.instance;
        standartTower = Factory.GetTower("Standart");
        missleTower = Factory.GetTower("Missle");
    }

    /*
    public void SelectStandartTower()
    {
        buildmanager.SelectTowerToBuild(standartTower);
    }

    public void SelectMissleTower()
    {
        buildmanager.SelectTowerToBuild(missleTower);
    }
    */

    public void BuildStandartTower()
    {
        buildmanager.BuildTower(standartTower);
        nodeUI.Hide();
    }

    public void BuildMissleTower()
    {
        buildmanager.BuildTower(missleTower);
        nodeUI.Hide();
    }
}
