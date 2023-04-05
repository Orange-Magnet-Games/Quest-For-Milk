using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    public int damage;
    public Transform container;
    public float knockback;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out HealthHolder health))
        {
            if(CompareTag(other.tag)) return;
            if (other.TryGetComponent(out Rigidbody rb))
            {
                rb.velocity = (transform.position - container.position).normalized * knockback;
                
            }
            CameraManager.instance.soundMan.Hit(1);
            health.TakeDamage(damage);
        }
    }
}
