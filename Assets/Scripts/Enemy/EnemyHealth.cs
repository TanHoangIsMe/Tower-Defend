using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoint = 5;
    [SerializeField] int currentHitPoint = 0;

    Enemy enemy;

    // Start is called before the first frame update
    void OnEnable()
    {
        currentHitPoint = maxHitPoint;
    }

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHitPoint < 0) 
        {
            gameObject.SetActive(false);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        currentHitPoint--;
        if (currentHitPoint < 0)
        {
            gameObject.SetActive(false);
            enemy.RewardGold();
        }
    }
}
