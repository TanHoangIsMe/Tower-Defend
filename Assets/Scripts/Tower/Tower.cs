using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] int cost = 50;
    [SerializeField] float buildTime = 1;

    private void Start()
    {
        StartCoroutine(Build());
    }

    private IEnumerator Build()
    {
        foreach (Transform child in transform) 
        { 
            child.gameObject.SetActive(false);
            foreach(Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(false);
            }
        }

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildTime);
            foreach (Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(true);
            }
        }
    }

    public bool CreateTower(Tower tower, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();

        if (bank == null)
        {
            return false;
        }

        if (bank.CurrentBalance >= cost)
        {
            Instantiate(tower.gameObject, position, Quaternion.identity);
            bank.Withdraw(cost);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SpawnParticle()
    {
        Instantiate(particleSystem.gameObject, transform.position, Quaternion.identity);
    }
}
