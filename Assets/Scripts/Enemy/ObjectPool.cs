using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject boomerPrefab;
    [SerializeField] [Range(0,30)] int leftPoolSize = 5;
    [SerializeField][Range(0, 30)] int rightPoolSize = 8;
    [SerializeField] [Range(0.1f,20f)] float spawnTime = 3.0f;

    GameObject[] poolLeft;
    GameObject[] poolRight;
    GameObject boomer;

    private void Awake()
    {       
        PopulatePool();
    }

    private void PopulatePool()
    {
        poolLeft = new GameObject[leftPoolSize];
        poolRight = new GameObject[rightPoolSize];

        for (int i = 0; i < leftPoolSize; i++) 
        {
            poolLeft[i] = Instantiate(enemyPrefab, transform);
            poolLeft[i].GetComponent<EnemyMover>().IsLeft = true;
            poolLeft[i].SetActive(false);
        }

        for (int i = 0; i < rightPoolSize; i++)
        {
            poolRight[i] = Instantiate(enemyPrefab, transform);
            poolRight[i].GetComponent<EnemyMover>().IsLeft = false;
            poolRight[i].SetActive(false);
        }

        boomer = Instantiate(boomerPrefab, transform);
        boomer.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyLeft());
        StartCoroutine(SpawnEnemyRight());
    }

    private IEnumerator SpawnEnemyLeft() 
    { 
        while(true)
        {
            EnableEnemy(poolLeft,leftPoolSize);
            yield return new WaitForSeconds(spawnTime);
        }
    }

    private IEnumerator SpawnEnemyRight()
    {
        while (true)
        {
            EnableEnemy(poolRight, rightPoolSize);
            yield return new WaitForSeconds(spawnTime);
        }
    }

    private IEnumerator SpawnBoomer()
    {
        while (true)
        {
            EnableBoomer();
            yield return new WaitForSeconds(spawnTime);
        }
    }

    private void EnableEnemy(GameObject[] pool, int poolSize)
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

    void EnableBoomer()
    {
        if(boomer.activeInHierarchy == false)
        {
            boomer.SetActive(true);
            return;
        }
    }
}
