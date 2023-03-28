using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public Animation anim;
    public float jumpPower;
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterController3D player))
        {
            anim.Play();
            player.GetComponent<Rigidbody>().velocity = Vector3.up * jumpPower;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out CharacterController3D player)) player.isGrounded = false;
    }
}
