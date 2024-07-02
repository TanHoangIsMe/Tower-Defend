using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoint = 5;
    private int currentHitPoint;
    // Start is called before the first frame update
    void OnEnable()
    {
        currentHitPoint = maxHitPoint;
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
            gameObject.SetActive(false);
    }
}
