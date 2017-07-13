using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;

    private NodeController selectedNode;

    public GameObject standartTower;
    public GameObject missleTower;

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
        if (node.tower!=null)
        {

        }
        selectedNode = node;
    }

    public void BuildTower(TowerBlueprint _tower)
    {
        if (_tower!=null && selectedNode!=null && PlayerStats.ChangeCurrency(-_tower.cost))
        {
            GameObject tower = (GameObject)Instantiate(_tower.prefab, selectedNode.BuildPosition(), Quaternion.identity);
            selectedNode.SetTower(tower,_tower.title);
        }
    }

    public void SellTower()
    {
        if (selectedNode!=null)
        {
            TowerBlueprint towerBlueprint = Factory.GetTower(selectedNode.GetTowerTitle);
            if (towerBlueprint!=null)
            {
                Destroy(selectedNode.tower);
                selectedNode.tower = null;
                PlayerStats.ChangeCurrency(towerBlueprint.sellCost);
            }
            
        }
    }
}
