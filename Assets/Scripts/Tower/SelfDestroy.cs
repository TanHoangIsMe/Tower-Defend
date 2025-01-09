using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    private ParticleSystem particleSystem;
    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (particleSystem != null && !particleSystem.isPlaying)
            Destroy(gameObject);
    }
}
