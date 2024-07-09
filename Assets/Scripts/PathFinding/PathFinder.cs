using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinate;
    [SerializeField] Vector2Int endCoordinate;

    Node currentSearchNode;
    Node startNode;
    Node endNode;

    Vector2Int[] directions = { Vector2Int.left, Vector2Int.up, Vector2Int.right, Vector2Int.down };
    GridManager gridManager;

    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();
    Queue<Node> frontier = new Queue<Node>();

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager != null)
        {
            grid = gridManager.Grid;
        }
    }


    //// Start is called before the first frame update
    void Start()
    {
        startNode = gridManager.Grid[startCoordinate];
        endNode = gridManager.Grid[endCoordinate];
        FindNewPath();
    }

    private List<Node> FindNewPath()
    {
        gridManager.ResetNode();
        BreadthFirstSearch();
        return BuildPath();
    }

    private void ExploreNeighbor()
    {
        List<Node> neighbors = new List<Node>();

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighborCoordinate = currentSearchNode.coordinates + direction;
            if (grid.ContainsKey(neighborCoordinate))
            {
                neighbors.Add(grid[neighborCoordinate]);
            }            
        }

        foreach (Node neighbor in neighbors)
        {
            if (!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
            {
                neighbor.connectedTo = currentSearchNode;
                reached.Add(neighbor.coordinates, neighbor);
                frontier.Enqueue(neighbor);
            }
        }
    }

    void BreadthFirstSearch()
    {
        frontier.Clear();
        reached.Clear();

        bool isRunning = true;
        frontier.Enqueue(startNode);
        reached.Add(startCoordinate, startNode);

        while (frontier.Count > 0 && isRunning)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbor();
            if (currentSearchNode.coordinates == endCoordinate)
            {
                isRunning = false;
            }
        }
    }

    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;
        path.Add(currentNode);
        currentNode.isPath = true;

        while (currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
        }
        path.Reverse();
        return path;
    }

    public bool WillBlockPath(Vector2Int coordinate)
    {
        if (grid.ContainsKey(coordinate))
        {
            bool previousPath = grid[coordinate].isWalkable;

            grid[coordinate].isWalkable = false;
            List<Node> newPath = FindNewPath();
            grid[coordinate].isWalkable = previousPath;

            if (newPath.Count <= 1)
            {
                FindNewPath();
                return true;
            }
        }
        return false;
    }
}
