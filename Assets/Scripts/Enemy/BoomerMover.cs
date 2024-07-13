using System.Collections;
using System.Collections.Generic;
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
        Debug.Log("new");
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
        gameObject.SetActive(false);
    }
}
