using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlaceable;
    // value to know a tile that cannot place tower but walkable
    [SerializeField] bool isCrossable; 

    public bool IsPlaceacle { get { return isPlaceable; } }

    GridManager gridManager;
    PathFinder pathFinder;
    Vector2Int coordinate;

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    void Start()
    {
        if(gridManager != null)
        {
            coordinate = gridManager.GetCoordinatesFromPosition(transform.position);
            if (!isPlaceable && !isCrossable)
                gridManager.BlockNode(coordinate);
        }
    }

    private void OnMouseDown()
    {
        if (isPlaceable)
        {
            bool isPlaced = towerPrefab.CreateTower(towerPrefab,transform.position);
            towerPrefab.Address = coordinate;
            isPlaceable = !isPlaced;
            //targetPathFinder.NotifyReceiver();
        }
    }
}
