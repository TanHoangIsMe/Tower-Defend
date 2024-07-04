using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
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
        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Path");
        SortArray(waypoints);
        foreach (GameObject waypoint in waypoints)
        {
            path.Add(waypoint.GetComponent<Waypoint>());
        }
        transform.position = waypoints[0].transform.position;
    }

    private IEnumerator MoveOnPath()
    {
        foreach (Waypoint waypoint in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
            float travelPercent = 0f;

            transform.LookAt(endPosition);

            while (travelPercent < 1f)
            { 
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }          
        }
        gameObject.SetActive(false);
        enemy.StealGold();
    }
    
    private void SortArray(GameObject[] waypoints)
    {
        GameObject waypoint;
        int z = 0;
        for (int i = 0;i < waypoints.Length; i++)
        {
            z++;
            for (int j = 0; j < waypoints.Length - z; j++)
            {
                float firstCompareXValue = waypoints[i].transform.position.x
                    / UnityEditor.EditorSnapSettings.move.x;
                float secondCompareXValue = waypoints[j + z].transform.position.x
                    / UnityEditor.EditorSnapSettings.move.x;
                float firstCompareZValue = waypoints[i].transform.position.z
                    / UnityEditor.EditorSnapSettings.move.z;
                float secondCompareZValue = waypoints[j + z].transform.position.z
                    / UnityEditor.EditorSnapSettings.move.z;
                if (firstCompareXValue > secondCompareXValue)
                {
                    waypoint = waypoints[i];
                    waypoints[i] = waypoints[j + z];
                    waypoints[j + z] = waypoint;
                }
                else if (firstCompareXValue == secondCompareXValue)
                {
                    if (firstCompareZValue > secondCompareZValue)
                    {
                        waypoint = waypoints[i];
                        waypoints[i] = waypoints[j + z];
                        waypoints[j + z] = waypoint;
                    }                  
                }
            }
        }
    }
}
