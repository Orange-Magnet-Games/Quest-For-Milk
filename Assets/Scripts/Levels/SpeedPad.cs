using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPad : MonoBehaviour
{
    public float speed;
    Rigidbody player;
    bool playerHere;
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterController3D player))
        {
            this.player = player.GetComponent<Rigidbody>();
            playerHere = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out CharacterController3D player))
        {
            this.player = null;
            playerHere = false;
        }
    }
    private void Update()
    {
        if(playerHere && player != null)
            player.velocity += transform.forward * speed * Time.deltaTime * 10;
    }
    
}
