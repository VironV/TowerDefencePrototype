using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodeController : MonoBehaviour {

    [Header("Settings")]
    public Color hoverColor;

    [Header("Technical")]
    public Vector3 offset;
    public GameObject tower;

    private string towerTitle;
    private Color startColor;
    private Renderer rend;
    private BuildManager buildManager;
    private NodeUISetter nodeUI;

    public Vector3 BuildPosition()
    {
        return (transform.position + offset);
    }

    public string GetTowerTitle { get { return towerTitle; } }

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.GetInstance;
        nodeUI = buildManager.nodeUISetter;
    }

    // Remebering tower
    public void SetTower(GameObject tower, string title)
    {
        this.tower = tower;
        towerTitle = title;
    }

    // Calling NodeUI and buildManager
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        buildManager.SelectNode(this);
        if (tower != null)
            nodeUI.ShowSellUI(this);
            
        else
            nodeUI.ShowBuildUI(this);  
    }

    //
    // Coloring nodes with mouse
    //
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
