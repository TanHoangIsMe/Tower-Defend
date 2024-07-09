using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEnemyMover : MonoBehaviour
{
    List<Node> path = new List<Node>();

    [SerializeField][Range(0f, 5f)] float speed = 0.5f;

    GridManager gridManager;
    PathFinder pathFinder;
    

    // Start is called before the first frame update
    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    void OnEnable()
    {
        speed += 0.05f;
        ReturnToStartPosition();
        FindThePath(false);
    }

    private void FindThePath(bool isReset)
    {
        Vector2Int coordinate = new Vector2Int();
        if (isReset)
        {
            coordinate = gridManager.GetCoordinatesFromPosition(transform.position);
        }
        else
        {
            coordinate = pathFinder.StartCoordinate;
        }
        StopAllCoroutines();
        path.Clear();
        path = pathFinder.FindNewPath(coordinate);
        StartCoroutine(MoveOnPath());
    }

    private void ReturnToStartPosition()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathFinder.StartCoordinate);
    }

    private IEnumerator MoveOnPath()
    {
        for(int i = 1; i < path.Count; i++) 
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
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
    }
}

