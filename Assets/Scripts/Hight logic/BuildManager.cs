using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

    [Header("Technical")]
    public GameObject standartTower;
    public GameObject missleTower;

    private static BuildManager instance;

    private NodeController selectedNode;

    public static BuildManager GetInstance { get { return instance; } }

    void Awake() {
        if (instance != null)
        {
            Debug.Log("Instance already exists!");
            return;
        }
        instance = this;
    }

    public NodeController GetSelectedNode { get { return selectedNode; } }

    public void SelectNode(NodeController node)
    {
        selectedNode = node;
    }

    public void BuildTower(TowerBlueprint towerBP)
    {
        if (towerBP!=null && selectedNode!=null && PlayerStats.ChangeCurrency(-towerBP.cost))
        {
            SetTower(towerBP);
        }
    }

    public void UpgradeTower()
    {
        if (selectedNode!=null)
        {
            TowerBlueprint towerBlueprint = TowersShop.GetTower("Upgraded " + selectedNode.GetTowerTitle);
            if (towerBlueprint!=null)
            {
                if (towerBlueprint != null && selectedNode != null && PlayerStats.ChangeCurrency(-towerBlueprint.cost))
                {
                    Destroy(selectedNode.tower);
                    SetTower(towerBlueprint);
                }
            }
        }
    }

    public void SellTower()
    {
        if (selectedNode!=null)
        {
            TowerBlueprint towerBlueprint = TowersShop.GetTower(selectedNode.GetTowerTitle);
            if (towerBlueprint!=null)
            {
                Destroy(selectedNode.tower);
                selectedNode.tower = null;
                PlayerStats.ChangeCurrency(towerBlueprint.sellCost);
            }
            
        }
    }

    private void SetTower(TowerBlueprint towerBlueprint)
    {
        GameObject tower = (GameObject)Instantiate(towerBlueprint.prefab, selectedNode.BuildPosition(), Quaternion.identity);
        selectedNode.SetTower(tower, towerBlueprint.title);
    }
}
