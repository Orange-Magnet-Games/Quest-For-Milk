using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeControl : MonoBehaviour
{
    Volume vol;
    ChromaticAberration chr;
    LensDistortion lns;
    public float chroma = 0;
    public float lens = 0;
    private void Start()
    {
        vol = GetComponent<Volume>();
        for (int i = 0; i < vol.profile.components.Count; i++)
        {
            if (vol.profile.components[i].name == "ChromaticAberration(Clone)")
            {
                chr = (ChromaticAberration)vol.profile.components[i];
            }
            if (vol.profile.components[i].name == "LensDistortion(Clone)")
            {
                lns = (LensDistortion)vol.profile.components[i];
            }
        }
    }
    private void Update()
    {
        lns.intensity.value = lens;
        chr.intensity.value = chroma;
    }
}
