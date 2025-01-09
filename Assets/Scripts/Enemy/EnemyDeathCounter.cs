using UnityEngine;

public class EnemyDeathCounter : MonoBehaviour
{
    private ObjectPool objectPool;
    private int death;
    public int Death { get { return death; } set { death = value; } }

    private void Awake()
    {
        objectPool = GetComponent<ObjectPool>();
    }

    public void CheckBoomerSpawnCondition()
    {
        if(death == 10 && objectPool != null) 
            objectPool.StartBoomerPhase();
    }
}
