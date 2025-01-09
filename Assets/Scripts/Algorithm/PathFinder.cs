using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    private Vector2Int startCoordinate;
    private Vector2Int endCoordinate;

    public Vector2Int StartCoordinate { get { return startCoordinate; } set { startCoordinate = value; } }
    public Vector2Int EndCoordinate { get { return endCoordinate; } set { endCoordinate = value; } }

    Node currentSearchNode;
    Node startNode;
    Node endNode;

    Vector2Int[] directions = { Vector2Int.left, Vector2Int.up, Vector2Int.right, Vector2Int.down };
    GridManager gridManager;

    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();
    Queue<Node> frontier = new Queue<Node>();

    public void SetUpJourney()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager != null)
        {
            grid = gridManager.Grid;
            startNode = grid[startCoordinate];
            endNode = grid[endCoordinate];
            startNode.isWalkable = true;
            endNode.isWalkable = true;
        }
    }

    public List<Node> FindNewPath()
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
        frontier.Enqueue(grid[startCoordinate]);
        reached.Add(startCoordinate, grid[startCoordinate]);

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
