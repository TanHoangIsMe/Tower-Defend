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

    Vector2Int[] directions = {Vector2Int.left, Vector2Int.up, Vector2Int.right, Vector2Int.down};
    GridManager gridManager;

    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int,Node>();
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();
    Queue<Node> frontier = new Queue<Node>();

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager != null) 
        {
            grid = gridManager.Grid;
        }

        startNode = new Node(startCoordinate, true);
        endNode = new Node(endCoordinate, true);
    }

    // Start is called before the first frame update
    void Start()
    {
        BreadthFirstSearch();
    }

    private void ExploreNeighbor()
    {
        List<Node> neighbors = new List<Node>();
       
        foreach (Vector2Int direction in directions) 
        {
            Vector2Int neighborCoordinate = currentSearchNode.coordinates + direction;
            if(grid.ContainsKey(neighborCoordinate))
            {
                neighbors.Add(grid[neighborCoordinate]);
            }
        }
        
        foreach (Node neighbor in neighbors)
        {
            if (!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
            {
                reached.Add(neighbor.coordinates, neighbor);
                frontier.Enqueue(neighbor);
            }
        }
    }

    void BreadthFirstSearch()
    {
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
}
