using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlaceable;

    public bool IsPlaceacle { get { return isPlaceable; } }

    GridNodeManager gridNodeManager;
    TargetPathFinder targetPathFinder;
    Vector2Int coordinate;

    void Awake()
    {
        gridNodeManager = FindObjectOfType<GridNodeManager>();
        targetPathFinder = FindObjectOfType<TargetPathFinder>();
    }

    void Start()
    {
        if(gridNodeManager != null)
        {
            coordinate = gridNodeManager.GetCoordinatesFromPosition(transform.position);
            if (!isPlaceable)
                gridNodeManager.BLockTreeNode(coordinate);
        }               
    }

    private void OnMouseDown()
    {
        if (isPlaceable)
        {
            bool isPlaced = towerPrefab.CreateTower(towerPrefab,transform.position);
            isPlaceable = !isPlaced;
            targetPathFinder.NotifyReceiver();
        }
    }
}
