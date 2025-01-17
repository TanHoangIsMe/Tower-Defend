using UnityEngine;

public class TargetLock : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] Transform target;
    [SerializeField] float range = 15f;
    [SerializeField] ParticleSystem towerArrowParticle;
  
    // Update is called once per frame
    void Update()
    {
        FindClosestEnemy();
        FocusOnEnemy();
    }

    private void FocusOnEnemy()
    {
        if (target == null) Attack(false); // if no enemy exist on map -> disable all tower attack action
        else
        {
            float enemyDistance = Vector3.Distance(transform.position, target.position);
            weapon.LookAt(target);
            if (enemyDistance > range)
                Attack(false);
            else
                Attack(true);
        }
    }

    private void FindClosestEnemy()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null; 
        float maxDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies) 
        {
            float enemyDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if(enemyDistance < maxDistance)
            {
                maxDistance = enemyDistance;
                closestTarget = enemy.transform;
            }
        }
        target = closestTarget;
    }

    private void Attack(bool isActive)
    {
        towerArrowParticle = GetComponentInChildren<ParticleSystem>();
        if (towerArrowParticle != null)
        {
            var emissionState = towerArrowParticle.emission;
            emissionState.enabled = isActive;
        }
    }
}
