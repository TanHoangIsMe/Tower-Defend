using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerMover : MonoBehaviour
{
    [SerializeField][Range(0f, 5f)] private float speed = 1.5f;

    private List<Node> path = new List<Node>();
    private GridManager gridManager;
    private PathFinder pathFinder;
    private Coroutine coroutine;

    private bool isLeft;
    public bool IsLeft { get { return isLeft; } set { isLeft = value; } }

    private bool canStart;
    public bool CanStart { get { return canStart; } set { canStart = value; } }

    // Start is called before the first frame update
    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = GetComponent<PathFinder>();
    }

    void OnEnable()
    {
        if (canStart)
        {
            ReturnToStartPosition();
            Move();
        }
    }

    public void Move()
    {
        if(coroutine != null) StopCoroutine(coroutine);
        transform.position = transform.position;
        FindThePath();
        coroutine = StartCoroutine(MoveOnPath());
    }

    private void FindThePath()
    {
        path.Clear();
        pathFinder.StartCoordinate = gridManager.GetCoordinatesFromPosition(transform.position);
        pathFinder.EndCoordinate = GetEndCoordinates();
        pathFinder.SetUpJourney();
        path = pathFinder.FindNewPath();       
    }

    private void ReturnToStartPosition()
    {
        transform.position = gridManager
            .GetPositionFromCoordinates(GetStartCoordinates());
    }

    private Vector2Int GetStartCoordinates()
    {
        if (isLeft)
            return new Vector2Int(0, 5);
        else
            return new Vector2Int(16, -3);
    }

    private Vector2Int GetEndCoordinates()
    {
        Vector2Int closestCoordinates = GetStartCoordinates();
        Vector2Int towerCoordinate;
        Tower[] towers = FindObjectsOfType<Tower>();
        float closestDistance = 50f;

        for (int i = 0; i < towers.Length; i++) 
        {
            towerCoordinate = gridManager.GetCoordinatesFromPosition(towers[i].transform.position);
            float distance =
                Mathf.Sqrt(
                    Mathf.Pow(GetStartCoordinates().x - towerCoordinate.x, 2) +
                    Mathf.Pow(GetStartCoordinates().y - towerCoordinate.y, 2)
                );

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestCoordinates = towerCoordinate;
            }
        }

        return closestCoordinates;
    }

    private IEnumerator MoveOnPath()
    {
        foreach (Node node in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(node.coordinates);
            float travelPercent = 0f;

            transform.LookAt(endPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
        FinishPath();
    }

    private void FinishPath()
    {
        gameObject.SetActive(false);
        DestroyTower();
    }

    void DestroyTower()
    {
        Vector3[] addresses = { 
            transform.position,
            transform.position + (Vector3.left * 10),
            transform.position + (Vector3.back * 10),
            transform.position + (Vector3.right * 10),
            transform.position + (Vector3.forward * 10),};

        Tower[] towers = FindObjectsOfType<Tower>();

        foreach (Vector3 address in addresses)
        {
            foreach (Tower tower in towers)
            {
                if (tower.transform.position == address)
                {
                    tower.SpawnParticle();
                    Destroy(tower.gameObject);
                }
            }
        }
    }
}
