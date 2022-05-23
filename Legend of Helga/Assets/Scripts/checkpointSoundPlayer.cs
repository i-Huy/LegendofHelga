using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpointSoundPlayer : MonoBehaviour
{
    AudioSource animationSoundPlayer;
    void Start()
    {
        animationSoundPlayer = GetComponent<AudioSource>();
    }
    
    private void CheckPointSound()
    {
        animationSoundPlayer.Play();
    }
}
