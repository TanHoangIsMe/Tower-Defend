using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPathFinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoor;
    public Vector2Int StartCoor { get { return startCoor; } }
    [SerializeField] Vector2Int endCoor;
    TreeNode startNode;
    TreeNode endNode;

    GridNodeManager gridNodeManager;
    TreeNode currentNode;

    Vector2Int[] neighborDirections = {Vector2Int.left, Vector2Int.up, Vector2Int.right, Vector2Int.down};

    Dictionary<Vector2Int,TreeNode> reachedNodes = new Dictionary<Vector2Int,TreeNode>();
    Dictionary<Vector2Int, TreeNode> grid = new Dictionary<Vector2Int,TreeNode>();

    Queue<TreeNode> queue = new Queue<TreeNode>();

    void Awake()
    {
        gridNodeManager = FindObjectOfType<GridNodeManager>();
        if(gridNodeManager != null)
        {
            grid = gridNodeManager.LeftGrid;
            startNode = grid[startCoor];
            endNode = grid[endCoor];
            startNode.IsWalkable = true;
            endNode.IsWalkable = true;
        }
    }

    void Start()
    {
        FindNewPath();
    }

    public List<TreeNode> FindNewPath()
    {
        BreadthFirstSearch();
        return BuildPath();
    }

    void ExploreNeighbors()
    {
        List<TreeNode>  neighbors = new List<TreeNode>();

        foreach(Vector2Int direction in neighborDirections)
        { 
            Vector2Int neighborDirection = currentNode.Coordinates + direction;
            if (grid.ContainsKey(neighborDirection))
            { 
                neighbors.Add(grid[neighborDirection]);
            }
        }

        foreach(TreeNode treeNode in neighbors)
        {
            if(!reachedNodes.ContainsKey(treeNode.Coordinates) && treeNode.IsWalkable)
            {
                treeNode.ConnectedTo = currentNode;
                reachedNodes.Add(treeNode.Coordinates,treeNode);
                queue.Enqueue(treeNode);
            }
        }
    }

    void BreadthFirstSearch()
    {
        queue.Clear();
        reachedNodes.Clear();

        bool isRunning = true;
        queue.Enqueue(startNode);
        reachedNodes.Add(startCoor, startNode);

        while (queue.Count > 0 && isRunning) 
        {
            currentNode = queue.Dequeue();
            currentNode.IsExplored = true;
            ExploreNeighbors();
            if (currentNode == endNode)
                isRunning = false;
        }
    }

    List<TreeNode> BuildPath()
    {
        List<TreeNode> path = new List<TreeNode>();
        TreeNode currentNode = endNode;
        path.Add(currentNode);
        currentNode.IsPath = true;

        while (currentNode.ConnectedTo != null)
        {
            currentNode = currentNode.ConnectedTo;
            path.Add(currentNode);
            currentNode.IsPath = true;
        }
        path.Reverse();
        return path;
    }
}
