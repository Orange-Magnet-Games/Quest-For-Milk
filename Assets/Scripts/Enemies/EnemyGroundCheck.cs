using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroundCheck : MonoBehaviour
{
    public Enemy enemy;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Walkable")
        {
            enemy.isGrounded = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Walkable")
        {
            enemy.isGrounded = false;
        }
    }
}
