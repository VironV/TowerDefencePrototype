using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUISetter : MonoBehaviour {

    [Header("Technical")]
    public GameObject buildUI;
    public GameObject sellUI;
    public Text sellText;
    public Text upgradeText;
    public Text gunTowerText;
    public Text MissleTowerText;

    private static NodeUISetter instance;

    private NodeController target;
    private BuildManager bm;
    private bool hidden;
    private int sellCost;

    public static NodeUISetter GetInstance { get { return instance; } }

    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Instance already exists!");
            return;
        }
        instance = this;
        bm = BuildManager.GetInstance;
    }

    private void Start()
    {
        Hide();
        hidden = false;
    }

    //
    // Setting text
    //
    private void SetSellText()
    {
        NodeController node = bm.GetSelectedNode;
        TowerBlueprint tb = TowersShop.GetTower(node.GetTowerTitle);
        if (tb!=null)
        {
            sellText.text = "Sell\n$" + tb.sellCost;
        }
    }

    private void SetUpgradeText()
    {
        NodeController node = bm.GetSelectedNode;
        TowerBlueprint tb = TowersShop.GetTower(node.GetTowerTitle);
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
        TowerBlueprint tbGun = TowersShop.GetTower("Standart");
        TowerBlueprint tbMissle = TowersShop.GetTower("Missle");
        if (tbGun != null && tbMissle!=null)
        {
            gunTowerText.text = "Gun tower\n$" + tbGun.cost;
            MissleTowerText.text = "Missle tower\n$" + tbMissle.cost;
        }
    }

    
    //
    // Moving
    //
    public void SetTarget(NodeController node)
    {
        target = node;
        gameObject.transform.position = target.BuildPosition();
    }


    //
    // Shoing and hiding
    //
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

    public void Hide()
    {
        //active = false;
        sellUI.SetActive(false);
        buildUI.SetActive(false);
        hidden = true;
    }

    
}
