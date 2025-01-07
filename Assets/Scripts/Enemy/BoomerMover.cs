using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoomerMover : MonoBehaviour
{
    [SerializeField][Range(0f, 5f)] private float speed = 1.5f;

    private List<Node> path = new List<Node>();
    private GridManager gridManager;
    private PathFinder pathFinder;

    private bool isLeft;
    public bool IsLeft { set { isLeft = value; } }

    // Start is called before the first frame update
    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    void OnEnable()
    {
        ReturnToStartPosition();
        //FindThePath();
        //StartCoroutine(MoveOnPath());
    }

    private void FindThePath()
    {
        path.Clear();
        path = pathFinder.FindNewPath();       
    }

    private void ReturnToStartPosition()
    {
        Vector2Int coordinate = new Vector2Int();
        if (isLeft)
        {
            coordinate.x = 0;
            coordinate.y = 5;
        }
        else
        {
            coordinate.x = 16;
            coordinate.y = -3;
        }
        transform.position = gridManager.GetPositionFromCoordinates(coordinate);
    }

    private IEnumerator MoveOnPath()
    {
        for (int i = 1; i < path.Count; i++)
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
        pathFinder.NotifyReceiver();
        gameObject.SetActive(false);
        //DestroyTower();      
    }

    //void DestroyTower()
    //{
    //    Vector2Int[] addresses = { path.Last().Coordinates + Vector2Int.left,
    //                             path.Last().Coordinates + Vector2Int.up,
    //                             path.Last().Coordinates + Vector2Int.right,
    //                             path.Last().Coordinates + Vector2Int.down,
    //                             path.Last().Coordinates + Vector2Int.up + Vector2Int.left,
    //                             path.Last().Coordinates + Vector2Int.up + Vector2Int.right,
    //                             path.Last().Coordinates + Vector2Int.down + Vector2Int.left,
    //                             path.Last().Coordinates + Vector2Int.down + Vector2Int.right};
    //    //Debug.Log(path.Last().Coordinates);
    //    Tower[] towers = FindObjectsOfType<Tower>();

    //    foreach (Vector2Int address in addresses)
    //    {
    //        //Debug.Log(address);
    //        foreach (Tower tower in towers)
    //        {
    //            Debug.Log(tower.Address);
    //            if (tower.Address == address) Destroy(tower.gameObject);
    //        }    
    //    }

    //    Array.Clear(towers, 0, towers.Length);
    //    Array.Clear(addresses, 0, addresses.Length);
    //}
}
