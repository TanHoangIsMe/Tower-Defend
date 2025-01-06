using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoomerMover : MonoBehaviour
{
    List<TreeNode> path = new List<TreeNode>();

    [SerializeField][Range(0f, 5f)] float speed = 1.5f;

    GridNodeManager gridNodeManager;
    TargetPathFinder targetPathFinder;


    // Start is called before the first frame update
    private void Awake()
    {
        gridNodeManager = FindObjectOfType<GridNodeManager>();
        targetPathFinder = FindObjectOfType<TargetPathFinder>();
    }

    void OnEnable()
    {
        ReturnToStartPosition();
        FindThePath();
        StartCoroutine(MoveOnPath());
    }

    private void FindThePath()
    {
        path.Clear();
        path = targetPathFinder.FindNewPath();       
    }

    private void ReturnToStartPosition()
    {
        transform.position = gridNodeManager.GetPositionFromCoordinates(targetPathFinder.StartCoor);
    }

    private IEnumerator MoveOnPath()
    {
        for (int i = 1; i < path.Count; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridNodeManager.GetPositionFromCoordinates(path[i].Coordinates);
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
        targetPathFinder.NotifyReceiver();
        gameObject.SetActive(false);
        DestroyTower();
        
    }

    void DestroyTower()
    {
        Vector2Int[] addresses = { path.Last().Coordinates + Vector2Int.left,
                                 path.Last().Coordinates + Vector2Int.up,
                                 path.Last().Coordinates + Vector2Int.right,
                                 path.Last().Coordinates + Vector2Int.down,
                                 path.Last().Coordinates + Vector2Int.up + Vector2Int.left,
                                 path.Last().Coordinates + Vector2Int.up + Vector2Int.right,
                                 path.Last().Coordinates + Vector2Int.down + Vector2Int.left,
                                 path.Last().Coordinates + Vector2Int.down + Vector2Int.right};
        //Debug.Log(path.Last().Coordinates);
        Tower[] towers = FindObjectsOfType<Tower>();

        foreach (Vector2Int address in addresses)
        {
            //Debug.Log(address);
            foreach (Tower tower in towers)
            {
                Debug.Log(tower.Address);
                if (tower.Address == address) Destroy(tower.gameObject);
            }    
        }

        Array.Clear(towers, 0, towers.Length);
        Array.Clear(addresses, 0, addresses.Length);
    }
}
