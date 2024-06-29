using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint> ();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PrintWayPointName());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator PrintWayPointName()

    {
        foreach (Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(1f);
        }
    }
}
