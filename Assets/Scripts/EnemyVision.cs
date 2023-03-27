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
            Vector3 displacement = (CharacterController3D.instance.transform.position - transform.position).normalized;
            //Debug.DrawRay(transform.position, displacement * 100, Color.red, .1f);

            RaycastHit ray;
            if (Physics.Raycast(transform.position, displacement, out ray, Mathf.Infinity))
            {
                if (ray.collider.gameObject == CharacterController3D.instance.gameObject) enemy.seen = true;
            }

        }
    }
}
