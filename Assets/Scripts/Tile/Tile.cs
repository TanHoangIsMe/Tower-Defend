using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
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
        if(gridManager != null)
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
            bool isPlaced = towerPrefab.CreateTower(towerPrefab,transform.position);
            isPlaceable = !isPlaced;
            gridManager.BlockNode(coordinate);
            Debug.Log("place");
        }
        else
        {
            Debug.Log(gridManager.GetNode(coordinate).isWalkable);
            Debug.Log(pathFinder.WillBlockPath(coordinate));
        }
    }
}
