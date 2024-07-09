using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Tile2 : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } }

    GridManager gridManager;
    PathFinder pathFinder;
    Vector2Int coordinate;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    private void Start()
    {
        if (gridManager != null)
        {
            coordinate = gridManager.GetCoordinatesFromPosition(transform.position);
            if (!isPlaceable)
            {
                gridManager.BlockNode(coordinate);
            }
        }
    }

    private void OnMouseDown()
    {
        if (gridManager.GetNode(coordinate).isWalkable && !pathFinder.WillBlockPath(coordinate))
        {
            Instantiate(towerPrefab, transform.position, Quaternion.identity);
            isPlaceable = false;
            gridManager.BlockNode(coordinate);
        }
    }
}
