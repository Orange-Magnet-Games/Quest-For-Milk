using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPad : MonoBehaviour
{
    public float speed;
    void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out CharacterController3D player))
        {
            other.GetComponent<Rigidbody>().velocity += transform.forward * speed;
        }
    }
}
