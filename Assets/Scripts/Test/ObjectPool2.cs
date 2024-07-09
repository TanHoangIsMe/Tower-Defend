using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool2 : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] [Range(0,30)] int poolSize = 5;
    [SerializeField] [Range(0.1f,20f)] float spawnTime = 3.0f;

    GameObject[] pool;

    private void Awake()
    {       
        PopulatePool();
    }

    private void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            pool[i] = Instantiate(enemyPrefab, transform);
            pool[i].SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy() 
    { 
        while(true)
        {
            EnableEnemy();
            yield return new WaitForSeconds(spawnTime);
        }
    }

    private void EnableEnemy()
    {
        for (int i = 0;i < poolSize;i++)
        {
            if (pool[i].activeInHierarchy == false)
            {
                pool[i].SetActive(true);
                return;
            }
        }
    }
}
