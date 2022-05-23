using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordOnHitEvent : MonoBehaviour
{
    AudioSource audioSource;
    AudioClip hit_enemy;
    AudioClip hit_other;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Sword Instantiated?");
        audioSource = gameObject.AddComponent<AudioSource>();
        hit_enemy = Resources.Load<AudioClip>("SoundEffects/hit_enemy");
        hit_other = Resources.Load<AudioClip>("SoundEffects/hit_other");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "HitBox") {
            //audioSource.PlayOneShot(hit_enemy);
        }
    }

}
