using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    [HideInInspector] public bool shaking;
    public IEnumerator StartShake(float intensity, float time, float speed)
    {
        shaking = true;
        Vector3 pos = transform.position;
        while(time > 0)
        {
            transform.position = pos + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * intensity;
            time -= speed;
            yield return new WaitForSeconds(speed);
        }
        transform.position = pos;
        shaking = false;
    }
}
