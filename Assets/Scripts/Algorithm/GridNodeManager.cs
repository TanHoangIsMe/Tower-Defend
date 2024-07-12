using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridNodeManager : MonoBehaviour
{
    [SerializeField] int unityGridSize = 10;
    public int UnityGridSize {  get { return unityGridSize; } }

    Dictionary<Vector2Int,TreeNode> leftGrid = new Dictionary<Vector2Int,TreeNode>();
    public Dictionary<Vector2Int,TreeNode> LeftGrid { get { return leftGrid; } }

    void Awake()
    {
        CreateLeftGrid();   
    }

    void CreateLeftGrid()
    {
        for (int x = -1; x <= 4; x++)
        { 
            for(int y = 1; y <= 7; y++)
            {
                Vector2Int coordinates = new Vector2Int(x, y);
                leftGrid.Add(coordinates, new TreeNode(coordinates, true));
            }
        }
    }

    public TreeNode GetTreeNode(Vector2Int coordinates)
    {
        if (leftGrid.ContainsKey(coordinates))
        {
            return leftGrid[coordinates]; 
        }
        return null;
    }

    public void BLockTreeNode(Vector2Int coordinates)
    {
        if (leftGrid.ContainsKey(coordinates))
        {
            leftGrid[coordinates].IsWalkable = false;
        }
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinate = new Vector2Int();
        coordinate.x = (int)Math.Round(position.x / unityGridSize);
        coordinate.y = (int)Math.Round(position.z / unityGridSize);
        return coordinate;
    }

    public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
    {
        Vector3 position = new Vector3();
        position.x = coordinates.x * unityGridSize;
        position.z = coordinates.y * unityGridSize;
        return position;
    }
}
