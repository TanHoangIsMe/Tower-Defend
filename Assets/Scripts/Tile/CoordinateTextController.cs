using System;
using TMPro;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateTextController : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;
    [SerializeField] Color exploredColor = Color.red;
    [SerializeField] Color pathColor = Color.yellow;

    TextMeshPro coordLable;
    Vector2Int coordinate = new Vector2Int();

    GridNodeManager gridNodeManager;

    private void Awake()
    {
        gridNodeManager = FindObjectOfType<GridNodeManager>();
        coordLable = GetComponent<TextMeshPro>();
        coordLable.enabled = false;
        DisplayCoordinate();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinate();
            UpdateObjectName();
        }
       UpdateColor();
       ControlLable();
    }

    private void ControlLable()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            coordLable.enabled = !coordLable.IsActive();
        }
    }

    private void UpdateColor()
    {
        if(gridNodeManager == null) { return; }
        
        TreeNode treeNode = gridNodeManager.GetTreeNode(coordinate);
        
        if (treeNode == null) { return; }

        if (!treeNode.IsWalkable)
        {
            coordLable.color = blockedColor;
        }
        else if (treeNode.IsPath)
        {
            coordLable.color = pathColor;
        }
        else if (treeNode.IsExplored)
        {
            coordLable.color = exploredColor;
        }
        else 
        {
            coordLable.color = defaultColor;
        }
    }

    private void DisplayCoordinate()
    {
        coordinate.x = (int)Math.Round(transform.parent.position.x / gridNodeManager.UnityGridSize);
        coordinate.y = (int)Math.Round(transform.parent.position.z / gridNodeManager.UnityGridSize);
        coordLable.text = coordinate.x + "," + coordinate.y;
    }

    private void UpdateObjectName()
    {
        transform.parent.name = $"({coordinate.x},{coordinate.y})";
    }
}
