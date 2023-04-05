using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWinScreen : MonoBehaviour
{
    public Dialogue winDialogue;
    public GameObject watchers;
    public VolumeControl vol;
    Shake shake;
    float intensity = .01f;
    bool watching = false;
    int watchCount = 0;
    private void Start()
    {
        DialogueManager.instance.StartDialogue(winDialogue);

        DialogueManager.OnSpecialSentenceStart += Watchers;
        DialogueManager.OnFinishDialogue += Crash;

        shake = DialogueManager.instance.GetComponentInChildren<Shake>();
    }
    void Watchers()
    {
        GetComponent<AudioSource>().Stop();
        watchers.SetActive(true);
        watching = true;

    }
    void Crash() 
    { 
        Application.Quit();
        Debug.Log("Crash() has run");
    }

    private void FixedUpdate()
    {
        if (watching)
        {
            watchCount++;
            if(vol.chroma < .5f) vol.chroma += .01f;
            if(vol.lens > -.5f) vol.lens -= .01f;

            if (!shake.shaking) 
            { 
                StartCoroutine(shake.StartShake(intensity, .03f, .01f));
                intensity += .01f;
            }
            if(watchCount > 3 * 60)
            {
                Crash();
            }
        }
    }
}
