using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    public int damage;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out HealthHolder health))
        {
            if(CompareTag(other.tag)) return;
            health.TakeDamage(damage);
        }
    }
}
