using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private CharacterController3D player;

    void Start()
    {
        player = gameObject.GetComponentInParent<CharacterController3D>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Walkable") player.isGrounded = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Walkable") player.isGrounded = false;
    }
}
