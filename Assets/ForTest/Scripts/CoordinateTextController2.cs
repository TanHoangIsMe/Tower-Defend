using System;
using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateTextController2 : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;
    [SerializeField] Color exploredColor = Color.red;
    [SerializeField] Color pathColor = Color.yellow;

    TextMeshPro coordLable;
    Vector2Int coordinate = new Vector2Int();

    GridManager gridManager;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
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
        if (gridManager == null) { return; }

        Node node = gridManager.GetNode(coordinate);
        if (node == null) { return; }

        if (!node.isWalkable)
        {
            coordLable.color = blockedColor;
        }
        else if (node.isPath)
        {
            coordLable.color = pathColor;
        }
        else if (node.isExplored)
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
        if (gridManager == null) { return; }

        coordinate.x = (int)Math.Round(transform.parent.position.x / gridManager.UnityGridSize);
        coordinate.y = (int)Math.Round(transform.parent.position.z / gridManager.UnityGridSize);
        coordLable.text = coordinate.x + "," + coordinate.y;
    }

    private void UpdateObjectName()
    {
        transform.parent.name = $"({coordinate.x},{coordinate.y})";
    }
}
