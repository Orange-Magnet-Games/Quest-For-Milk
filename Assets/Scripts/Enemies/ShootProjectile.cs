using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public Seed seed;
    public float speed;
    public Transform cone;
    void Shoot()
    {
        Instantiate(seed, cone.position,new Quaternion())
            .Initiate(speed, (CharacterController3D.instance.transform.position - cone.position).normalized);
    }
}
