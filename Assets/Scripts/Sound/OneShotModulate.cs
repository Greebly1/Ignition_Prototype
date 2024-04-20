using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class OneShotModulate : MonoBehaviour, SoundPlayer
{
    [SerializeField] bool playOnAwake = false;
    [SerializeField] bool playOnEnable = true;
    public AudioClip sfx_resource;
    public AudioSource sfx_source;
    public bool modulateVolume = false;
    public bool modulatePitch = false;
    public float minVolume = 1;
    public float maxVolume = 1;
    public float minPitch = 1;
    public float maxPitch = 1;

    private void Awake()
    {
        sfx_source = GetComponent<AudioSource>();
        if (playOnAwake) PlaySound(); 
    }

    private void OnEnable()
    {
        if (playOnEnable) PlaySound(); 
    }

    public void PlaySound()
    {
        if (modulateVolume) sfx_source.volume = Random.Range(minVolume, maxVolume);
        if (modulatePitch) sfx_source.pitch = Random.Range(minPitch, maxPitch);
        //sfx_source.Play();
        sfx_source.PlayOneShot(sfx_resource);
    }
}
