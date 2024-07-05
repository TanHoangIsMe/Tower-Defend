using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } }

    GridManager gridManager;
    Vector2Int coordinate;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
    }

    private void Start()
    {
        if(gridManager != null)
        {
            coordinate = gridManager.GetCoordinatesFromPosition(transform.position);
            if (!isPlaceable) 
            { 
                gridManager.BlockNode(coordinate);
                Debug.Log(gridManager.GetNode(coordinate).isWalkable);
            }
        }
    }

    private void OnMouseDown()
    {
        if (isPlaceable)
        {
            bool isPlaced = towerPrefab.CreateTower(towerPrefab,transform.position);
            isPlaceable = !isPlaced;
        }        
    }
}
