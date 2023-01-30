using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DrugsMode : MonoBehaviour
{
    public bool active = false;
    Volume vol;
    private void Start()
    {
        vol = GetComponent<Volume>();
    }
    private void Update()
    {
        vol.profile.TryGet(out ColorCurves curves);
        vol.profile.TryGet(out ColorAdjustments adj);
        if (active)
        {
            curves.active = true;
            adj.hueShift.value += .1f;
            if (adj.hueShift.value >= 179) adj.hueShift.value = -180;
        }
        else
        {
            curves.active = false;
            adj.hueShift.value = 0;
        }
    }
}
