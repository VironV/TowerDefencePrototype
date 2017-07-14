using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour {

    public static NodeUI instance;

    private NodeController target;
    private bool hidden;
    private int sellCost;

    public GameObject buildUI;
    public GameObject sellUI;
    public GameObject nodeUI;
    public Text sellText;
    public Text upgradeText;
    public Text gunTowerText;
    public Text MissleTowerText;

    
    //public bool GetActive { get { return active; } }

    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Instance already exists!");
            return;
        }
        instance = this;
    }

    private void Start()
    {
        Hide();
        hidden = false;
    }

    private void SetSellText()
    {
        NodeController node = BuildManager.instance.GetSelectedNode;
        TowerBlueprint tb = Factory.GetTower(node.GetTowerTitle);
        if (tb!=null)
        {
            sellText.text = "Sell\n$" + tb.sellCost;
        }
    }

    private void SetUpgradeText()
    {
        NodeController node = BuildManager.instance.GetSelectedNode;
        TowerBlueprint tb = Factory.GetTower(node.GetTowerTitle);
        if (tb!=null)
        {
            if (tb.upgradeCost == 0)
                upgradeText.text = "Can't\nupgrade";
            else
                upgradeText.text = "Upgrade\n$" + tb.upgradeCost;
        }
    }

    private void SetShopText()
    {
        TowerBlueprint tbGun = Factory.GetTower("Standart");
        TowerBlueprint tbMissle = Factory.GetTower("Missle");
        if (tbGun != null && tbMissle!=null)
        {
            gunTowerText.text = "Gun tower\n$" + tbGun.cost;
            MissleTowerText.text = "Missle tower\n$" + tbMissle.cost;
        }
    }

    public void SetTarget(NodeController node)
    {
        target = node;
        nodeUI.transform.position = target.BuildPosition();
    }

    public void ShowSellUI(NodeController node)
    {
        if(node == target && !hidden)
        {
            Hide();
            return;
        }

        hidden = false;
        SetTarget(node);
        SetSellText();
        SetUpgradeText();
        sellUI.SetActive(true);
        buildUI.SetActive(false);

    }

    public void Hide()
    {
        //active = false;
        sellUI.SetActive(false);
        buildUI.SetActive(false);
        hidden = true;
    }

    public void ShowBuildUI(NodeController node)
    {
        if (node == target && !hidden)
        {
            Hide();
            return;
        }

        hidden = false;
        SetTarget(node);
        SetShopText();
        buildUI.SetActive(true);
        sellUI.SetActive(false);
    }
}
