using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] [Range(0,50)] int leftPoolSize = 5;
    [SerializeField][Range(0, 50)] int rightPoolSize = 5;
    [SerializeField] [Range(0.1f,20f)] float spawnTime = 1.0f;

    GameObject[] poolLeft;
    GameObject[] poolRight;

    Transform firstTileLeft;
    Transform firstTileRight;

    private void Awake()
    {       
        PopulatePool();
    }

    private void PopulatePool()
    {
        firstTileLeft = GameObject.FindWithTag("PathLeft").transform.GetChild(0);
        firstTileRight = GameObject.FindWithTag("PathRight").transform.GetChild(0);

        poolLeft = new GameObject[leftPoolSize];
        poolRight = new GameObject[rightPoolSize];

        for (int i = 0; i < leftPoolSize; i++) 
        {
            poolLeft[i] = Instantiate(enemyPrefab, firstTileLeft);
            poolLeft[i].GetComponent<EnemyMover>().IsLeft = true;
            poolLeft[i].SetActive(false);
        }

        for (int i = 0; i < rightPoolSize; i++)
        {
            poolRight[i] = Instantiate(enemyPrefab, firstTileRight);
            poolRight[i].GetComponent<EnemyMover>().IsLeft = false;
            poolRight[i].SetActive(false);
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
        for (int i = 0;i < leftPoolSize;i++)
        {
            if (poolLeft[i].activeInHierarchy == false)
            {
                poolLeft[i].SetActive(true);
                return;
            }
        }

        for (int i = 0; i < rightPoolSize; i++)
        {
            if (poolRight[i].activeInHierarchy == false)
            {
                poolRight[i].SetActive(true);
                return;
            }
        }
    }
}
