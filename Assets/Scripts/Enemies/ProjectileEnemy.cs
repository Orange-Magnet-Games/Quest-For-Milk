using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemy : Enemy
{
    Rigidbody rb;
    Animator anim;
    public float spin;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        if (seen)
        {
            Vector3 a = transform.eulerAngles;
            transform.LookAt(CharacterController3D.instance.transform.position);
            transform.eulerAngles = new Vector3(a.x, transform.eulerAngles.y, a.z);
            anim.SetBool("Attacking", true);
        }
        else
        {
            transform.eulerAngles += new Vector3(0, spin, 0);
        }
    }
}
