using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLock : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = FindAnyObjectByType<EnemyMover>().transform;
        Debug.Log(target);
    }

    // Update is called once per frame
    void Update()
    {
        weapon.LookAt(target);
    }
}
