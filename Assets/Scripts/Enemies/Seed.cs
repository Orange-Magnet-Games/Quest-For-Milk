using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    Rigidbody rb;

    public float speed;
    public Vector3 dir;
    public GameObject Initiate(float speed, Vector3 direction)
    {
        this.speed = speed;
        this.dir = direction;
        return gameObject;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        transform.LookAt(transform.position + dir);
        rb.velocity = dir * speed;
    }
    void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Enemy")) 
            Destroy(gameObject);
    }
}
