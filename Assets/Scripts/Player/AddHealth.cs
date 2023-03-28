using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHealth : MonoBehaviour
{
    private void Update()
    {
        transform.eulerAngles += new Vector3(0, 1, 0);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<CharacterController3D>())
        {
            collision.gameObject.GetComponent<HealthHolder>().health += 1;
            Destroy(gameObject);
        }
    }
}
