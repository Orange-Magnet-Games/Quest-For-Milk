using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteAlways]
public class LockOnTarget : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.transform.position + offset;
    }
}
