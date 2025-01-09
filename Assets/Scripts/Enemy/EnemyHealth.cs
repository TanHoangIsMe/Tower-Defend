using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoint = 2;
    [Tooltip("Add enemy more health based on level difficulty")]
    [SerializeField] int difficultyLevel = 2;

    int currentHitPoint = 0;
    Enemy enemy;
    EnemyDeathCounter counter;

    // Start is called before the first frame update
    void OnEnable()
    {
        currentHitPoint = maxHitPoint;
    }

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        counter = FindObjectOfType<EnemyDeathCounter>();
    }

    private void OnParticleCollision(GameObject other)
    {
        currentHitPoint--;
        if (currentHitPoint < 0)
        {
            gameObject.SetActive(false);
            maxHitPoint += difficultyLevel;
            enemy.RewardGold();
            counter.Death += 1;
            counter.CheckBoomerSpawnCondition();
        }
    }
}
