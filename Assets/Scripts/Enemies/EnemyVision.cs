using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public Enemy enemy;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Vector3 displacement = (CharacterController3D.instance.transform.position - transform.position);
            //Debug.DrawRay(transform.position, displacement * 100, Color.red, .1f);

            RaycastHit ray;
            if (Physics.Raycast(transform.position, displacement.normalized, out ray, displacement.magnitude, LayerMask.GetMask("Player", "Default")))
            {
                //Debug.Log(ray.collider.gameObject.name);
                //Debug.DrawRay(transform.position, displacement * 100, Color.red, 1);
                if (ray.collider.gameObject.CompareTag("Player")) enemy.seen = true;
            }

        }
    }
}
