using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlaceable;
    // value to know a tile that cannot place tower but walkable
    [SerializeField] bool isCrossable; 

    public bool IsPlaceacle { get { return isPlaceable; } }

    GridManager gridManager;
    Vector2Int coordinate;

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
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
            isPlaceable = !isPlaced;
            BoomerMover[] boomer = FindObjectsOfType<BoomerMover>();
            for(int i = 0; i<boomer.Length; i++)
            {
                boomer[i].Move();
            }
        }
    }
}
