using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodeController : MonoBehaviour {

    public Color hoverColor;
    public Color notEnoughColor;
    public Vector3 offset;

    [Header("Optional")]
    public GameObject tower;

    private string towerTitle;
    private Color startColor;
    private Renderer rend;
    private BuildManager buildManager;
    private NodeUI nodeUI;

    public Vector3 BuildPosition()
    {
        return (transform.position + offset);
    }

    public string GetTowerTitle { get { return towerTitle; } }

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
        nodeUI = NodeUI.instance;
    }

    public void SetTower(GameObject tower, string title)
    {
        this.tower = tower;
        towerTitle = title;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (tower != null)
            nodeUI.ShowSellUI(this);
        else
            nodeUI.ShowBuildUI(this);
        
        buildManager.SelectNode(this);
    }

    void OnMouseEnter()
    {

        if (EventSystem.current.IsPointerOverGameObject())
            return;
        rend.material.color = hoverColor;
    }

    
    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
    
}
