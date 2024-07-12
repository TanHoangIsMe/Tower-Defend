using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

[System.Serializable]
public class TreeNode
{
    Vector2Int coordinates;
    bool isWalkable;
    bool isExplored;
    bool isPath;
    TreeNode connectedTo;

    public Vector2Int Coordinates { get { return coordinates; } }
    public bool IsWalkable { get { return isWalkable; } set { isWalkable = value; } }
    public bool IsExplored { get { return isExplored; } set { isExplored = value; } }
    public bool IsPath { get { return isPath; } set { isPath = value; } }
    public TreeNode ConnectedTo { get { return connectedTo; } set { connectedTo = value; } }

    public TreeNode(Vector2Int coordinates, bool isWalkable)
    {
        this.coordinates = coordinates;
        this.isWalkable = isWalkable;
    }
}
