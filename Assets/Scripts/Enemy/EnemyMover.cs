using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Tile> path = new List<Tile>();
    [SerializeField] [Range(0f,5f)] float speed = 1f;

    Enemy enemy;

    // Start is called before the first frame update
    private void Start()
    {
        enemy = GetComponent<Enemy>();  
    }

    void OnEnable()
    {
        FindThePath();
        StartCoroutine(MoveOnPath());
    }

    private void OnDisable()
    {
        path.Clear();
    }

    private void FindThePath()
    {
        GameObject parent = GameObject.FindGameObjectWithTag("Path");

        foreach (Transform child in parent.transform)
        {
            Tile tile = child.GetComponent<Tile>();
            path.Add(tile.GetComponent<Tile>());
        }
        ReturnToStartPosition();
    }

    private void ReturnToStartPosition()
    {
        transform.position = path[0].transform.position;
    }

    private IEnumerator MoveOnPath()
    {
        foreach (Tile tile in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = tile.transform.position;
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
        enemy.StealGold();
    }

    //private void SortArray(GameObject[] waypoints)
    //{
    //    GameObject waypoint;
    //    int z = 0;
    //    for (int i = 0;i < waypoints.Length; i++)
    //    {
    //        z++;
    //        for (int j = 0; j < waypoints.Length - z; j++)
    //        {
    //            float firstCompareXValue = waypoints[i].transform.position.x
    //                / UnityEditor.EditorSnapSettings.move.x;
    //            float secondCompareXValue = waypoints[j + z].transform.position.x
    //                / UnityEditor.EditorSnapSettings.move.x;
    //            float firstCompareZValue = waypoints[i].transform.position.z
    //                / UnityEditor.EditorSnapSettings.move.z;
    //            float secondCompareZValue = waypoints[j + z].transform.position.z
    //                / UnityEditor.EditorSnapSettings.move.z;
    //            if (firstCompareXValue > secondCompareXValue)
    //            {
    //                waypoint = waypoints[i];
    //                waypoints[i] = waypoints[j + z];
    //                waypoints[j + z] = waypoint;
    //            }
    //            else if (firstCompareXValue == secondCompareXValue)
    //            {
    //                if (firstCompareZValue > secondCompareZValue)
    //                {
    //                    waypoint = waypoints[i];
    //                    waypoints[i] = waypoints[j + z];
    //                    waypoints[j + z] = waypoint;
    //                }                  
    //            }
    //        }
    //    }
    //}
}
