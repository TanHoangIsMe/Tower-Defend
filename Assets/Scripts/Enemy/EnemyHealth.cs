using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoint = 2;
    [Tooltip("Add enemy more health based on level difficulty")]
    [SerializeField] int difficultyLevel = 2;

    int currentHitPoint = 0;
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
            maxHitPoint += difficultyLevel;
            enemy.RewardGold();
        }
    }
}
