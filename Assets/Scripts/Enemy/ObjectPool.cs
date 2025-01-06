using System;
using System.Collections;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject boomerPrefab;
    [SerializeField] [Range(0,30)] int leftRamPoolSize = 5;
    [SerializeField] [Range(0, 30)] int rightRamPoolSize = 8;
    [SerializeField] [Range(0.1f,20f)] float ramSpawnTime = 3.0f;

    GameObject[] leftRamPool;
    GameObject[] rightRamPool;
    GameObject leftBoomer;
    GameObject rightBoomer;

    private void Awake()
    {       
        PopulatePool();
    }

    private void PopulatePool()
    {
        leftRamPool = new GameObject[leftRamPoolSize];
        rightRamPool = new GameObject[rightRamPoolSize];

        for (int i = 0; i < leftRamPoolSize; i++) 
        {
            leftRamPool[i] = Instantiate(enemyPrefab, transform);
            leftRamPool[i].GetComponent<EnemyMover>().IsLeft = true;
            leftRamPool[i].SetActive(false);
        }

        for (int i = 0; i < rightRamPoolSize; i++)
        {
            rightRamPool[i] = Instantiate(enemyPrefab, transform);
            rightRamPool[i].GetComponent<EnemyMover>().IsLeft = false;
            rightRamPool[i].SetActive(false);
        }

        //boomer = Instantiate(boomerPrefab, transform);
        //boomer.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyLeft());
        StartCoroutine(SpawnEnemyRight());
        //StartCoroutine(SpawnBoomer());
    }

    private IEnumerator SpawnEnemyLeft() 
    { 
        while(true)
        {
            EnableEnemy(leftRamPool, leftRamPoolSize);
            yield return new WaitForSeconds(ramSpawnTime);
        }
    }

    private IEnumerator SpawnEnemyRight()
    {
        while (true)
        {
            EnableEnemy(rightRamPool, rightRamPoolSize);
            yield return new WaitForSeconds(ramSpawnTime);
        }
    }

    private IEnumerator SpawnBoomer()
    {
        while (true)
        {
            EnableBoomer();
            yield return new WaitForSeconds(ramSpawnTime);
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
        //if(boomer.activeInHierarchy == false)
        //{
        //    boomer.SetActive(true);
        //    return;
        //}
    }
}
