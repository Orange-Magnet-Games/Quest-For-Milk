using System.Collections;
using System.Collections.Generic;
using System.Media;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource stepSound; public void Step(float vol)
    {
        stepSound.volume = vol * Volume;
        stepSound.pitch = Random.Range(0.5f, 1.5f);
        stepSound.Play();
    }

    public AudioSource swingSound; public void Swing(float vol)
    {
        swingSound.volume = vol * Volume;
        swingSound.pitch = Random.Range(0.5f, 1.5f);
        swingSound.Play();
    }
    public AudioSource jumpSound; public void Jump(float vol)
    {
        jumpSound.volume = vol * Volume;
        jumpSound.pitch = Random.Range(0.5f, 1.5f);
        jumpSound.Play();
    }
    public AudioSource springSound; public void Spring(float vol)
    {
        springSound.volume = vol * Volume;
        springSound.pitch = Random.Range(0.5f, 1.5f);
        springSound.Play();
    }
    public AudioSource landSound; public void Land(float vol)
    {
        landSound.volume = vol * Volume;
        landSound.pitch = Random.Range(0.5f, 1.5f);
        landSound.Play();
    }
    public AudioSource hitSound; public void Hit(float vol)
    {
        hitSound.volume = vol * Volume;
        hitSound.pitch = Random.Range(0.5f, 1.5f);
        hitSound.Play();
    }
    public AudioSource deathSound; public void Death(float vol)
    {
        deathSound.volume = vol * Volume;
        deathSound.pitch = Random.Range(0.5f, 1.5f);
        deathSound.Play();
    }
    public AudioSource heartSound; public void Heart(float vol)
    {
        heartSound.volume = vol * Volume;
        heartSound.pitch = Random.Range(0.5f, 1.5f);
        heartSound.Play();
    }

    public AudioSource music;
    // ---------------------- VOLUME CONTROL --------------------------------

    private List<AudioSource> allSounds = new();

    [SerializeField] private float _vol; public float Volume
    {
        get { return _vol; }
        set
        {
            if (_vol != value)
            {
                _vol = value;

                foreach(AudioSource sound in allSounds)
                    sound.volume = _vol;
                
            }
        }
    }


    private void Start()
    {
        allSounds.Add(stepSound);
        allSounds.Add(swingSound);
        allSounds.Add(jumpSound);
        allSounds.Add(springSound);
        allSounds.Add(landSound);
        allSounds.Add(hitSound);
        allSounds.Add(music);
    }

    
}
