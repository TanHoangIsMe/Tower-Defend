using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;
    [SerializeField] int unityGridSize = 10;
    public int UnityGridSize { get { return unityGridSize; } }

    Dictionary<Vector2Int,Node> grid = new Dictionary<Vector2Int,Node>();
    //public Dictionary<Vector2Int, Node> Grid { get { return grid; } }

    private void Awake()
    {
        CreateGrid(); 
        foreach(KeyValuePair<Vector2Int,Node> a in grid)
        {
            Debug.Log(a.Key);
        }
    }

    public Node GetNode(Vector2Int coordinate)
    {
        if (grid.ContainsKey(coordinate))
        {
            return grid[coordinate];
        }
        return null;
    }

    //public void BlockNode(Vector2Int coordinate)
    //{
    //    if (grid.ContainsKey(coordinate))
    //    {
    //        grid[coordinate].isWalkable = false;
    //    }
    //}

    //public void ResetNode()
    //{
    //    foreach (KeyValuePair<Vector2Int, Node> entry in grid) 
    //    {
    //        entry.Value.isExplored = false;
    //        entry.Value.isPath = false;
    //        entry.Value.connectedTo = null;
    //    }
    //}

    //public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    //{
    //    Vector2Int coordinate = new Vector2Int();
    //    coordinate.x = (int)Math.Round(transform.position.x / unityGridSize);
    //    coordinate.y = (int)Math.Round(transform.position.z / unityGridSize);
    //    return coordinate;
    //}

    //public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
    //{
    //    Vector3 position = new Vector3();
    //    position.x = coordinates.x * unityGridSize;
    //    position.z = coordinates.y * unityGridSize;
    //    position.y = 0;
    //    return position;
    //}

    private void CreateGrid()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            { 
                Vector2Int coordinate = new Vector2Int(x, y);
                grid.Add(coordinate, new Node(coordinate, true));
            }
        }
    }
}
