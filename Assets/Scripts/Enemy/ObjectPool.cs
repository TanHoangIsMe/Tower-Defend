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
    [SerializeField] [Range(0.1f, 20f)] float boomerSpawnTime = 6.0f;

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

        leftBoomer = Instantiate(boomerPrefab, transform);
        leftBoomer.GetComponent<BoomerMover>().IsLeft = true;
        leftBoomer.GetComponent <BoomerMover>().CanStart = true;
        leftBoomer.SetActive(false);

        rightBoomer = Instantiate(boomerPrefab, transform);
        rightBoomer.GetComponent<BoomerMover>().IsLeft = false;
        rightBoomer.GetComponent<BoomerMover>().CanStart = true;
        rightBoomer.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRam(leftRamPool, leftRamPoolSize));
        StartCoroutine(SpawnRam(rightRamPool, rightRamPoolSize));
    }

    public void StartBoomerPhase()
    {
        StartCoroutine(SpawnBoomer(leftBoomer));
        StartCoroutine(SpawnBoomer(rightBoomer));
    }

    private IEnumerator SpawnRam(GameObject[] ramPool, int ramPoolSize) 
    { 
        while(true)
        {
            EnableRam(ramPool, ramPoolSize);
            yield return new WaitForSeconds(ramSpawnTime);
        }
    }

    private IEnumerator SpawnBoomer(GameObject boomer)
    {
        while (true)
        {
            EnableBoomer(boomer);
            yield return new WaitForSeconds(boomerSpawnTime);
        }
    }

    private void EnableRam(GameObject[] pool, int poolSize)
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

    private void EnableBoomer(GameObject boomer)
    {
        if (boomer.activeInHierarchy == false && FindObjectOfType<Tower>() != null)
        {
            boomer.SetActive(true);
            return;
        }
    }
}
