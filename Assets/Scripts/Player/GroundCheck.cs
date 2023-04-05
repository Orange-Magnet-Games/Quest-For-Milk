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
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Walkable")
        {
            Vector3 pos = other.transform.position;
            pos = new Vector3(pos.x, pos.y + (other.transform.parent ? other.transform.parent.lossyScale.y : other.transform.localScale.y) * 2.5f, pos.z);
            player.lastWalked = pos;
            player.IsGrounded = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Walkable")
        {
            player.IsGrounded = false;
        }
    }
}
