using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashSound : MonoBehaviour
{
    private AudioSource source;
    public AudioClip soundVariant1;
    public AudioClip soundVariant2;
  
    public float lowPitchRange = 0.75f;
    public float highPitchRange = 1.25f;
  
    public float influenceOfMagnitude = .01f;
    public float velocityLimit = 15f;

    void Start()
    {
       source = GetComponent<AudioSource>(); 
    }

    void OnCollisionEnter (Collision collide)
    {
        source.pitch = Random.Range(lowPitchRange, highPitchRange);
        
        float hitVol = collide.relativeVelocity.magnitude * influenceOfMagnitude;
        if (collide.relativeVelocity.magnitude < velocityLimit)
            source.PlayOneShot(soundVariant1, hitVol);
        else
            source.PlayOneShot(soundVariant2, hitVol);
    }
}
