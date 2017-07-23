using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUIController : MonoBehaviour {

    private BuildManager buildmanager;
    private NodeUISetter nodeUI;

    private TowerBlueprint standartTower;
    private TowerBlueprint missleTower;
    private TowerBlueprint fireTower;

    void Start()
    {
        buildmanager = BuildManager.GetInstance;
        nodeUI = buildmanager.nodeUISetter;
        standartTower = TowersShop.GetTower("Standart");
        missleTower = TowersShop.GetTower("Missle");
        fireTower = TowersShop.GetTower("Fire");
    }

    //
    // Buttons
    //
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

    public void BuildFireTower()
    {
        buildmanager.BuildTower(fireTower);
        nodeUI.Hide();
    }
}
