using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skybox : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Seed>())
        {
            Destroy(other.gameObject);
        }
    }
}
