using UnityEngine;

[System.Serializable]
public class TreeNode
{
    Vector2Int coordinates;
    TreeNode connectedTo;

    bool isWalkable;
    bool isExplored;
    bool isPath;
    
    public Vector2Int Coordinates { get { return coordinates; } }
    public TreeNode ConnectedTo { get { return connectedTo; } set { connectedTo = value; } }
    public bool IsWalkable { get { return isWalkable; } set { isWalkable = value; } }
    public bool IsExplored { get { return isExplored; } set { isExplored = value; } }
    public bool IsPath { get { return isPath; } set { isPath = value; } }

    public TreeNode(Vector2Int coordinates, bool isWalkable)
    {
        this.coordinates = coordinates;
        this.isWalkable = isWalkable;
    }
}
