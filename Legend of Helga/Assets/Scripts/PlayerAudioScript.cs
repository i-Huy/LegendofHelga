using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerControl;

[System.Serializable]
public class PlayerAudioScript : MonoBehaviour
{
    public PlayerAnimationEvents animEvents;
    public float globalVolume = 0.5f;
    AudioSource audioSource;
    AudioClip step_l;
    AudioClip step_r;
    AudioClip hit_l;
    AudioClip hit_r;
    AudioClip swing_1;
    AudioClip swing_2;
    AudioClip bow;
    AudioClip jump;
    AudioClip death;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        animEvents = gameObject.GetComponent<PlayerAnimationEvents>();

        step_l = Resources.Load<AudioClip>("PlayerSounds/Step Grass L");
        step_r = Resources.Load<AudioClip>("PlayerSounds/Step Grass R");
        hit_l = Resources.Load<AudioClip>("PlayerSounds/Whiff1");
        hit_r = Resources.Load<AudioClip>("PlayerSounds/Whiff2");
        swing_1 = Resources.Load<AudioClip>("PlayerSounds/Sword Swing 1");
        swing_2 = Resources.Load<AudioClip>("PlayerSounds/Sword Swing 2");
        bow = Resources.Load<AudioClip>("PlayerSounds/Bow Release");
        jump = Resources.Load<AudioClip>("PlayerSounds/Jump");
        death = Resources.Load<AudioClip>("PlayerSounds/Jump");

        animEvents.OnFootL.AddListener(OnFootLSound);
        animEvents.OnFootR.AddListener(OnFootRSound);
        animEvents.OnHitL.AddListener(OnHitLSound);
        animEvents.OnHitR.AddListener(OnHitRSound);
        animEvents.OnSwing1.AddListener(OnSwing1Sound);
        animEvents.OnSwing2.AddListener(OnSwing2Sound);
        animEvents.OnShoot.AddListener(OnShootSound);
        animEvents.OnJump.AddListener(OnJumpSound);
        animEvents.OnDeath.AddListener(OnDeathSound);
    }

    public void OnFootLSound()
    {
        audioSource.PlayOneShot(step_l, globalVolume);
    }

    public void OnFootRSound()
    {
        audioSource.PlayOneShot(step_r, globalVolume);
    }

    public void OnHitLSound()
    {
        audioSource.PlayOneShot(hit_l, globalVolume);
    }

    public void OnHitRSound()
    {
        audioSource.PlayOneShot(hit_r, globalVolume);
    }
    public void OnSwing1Sound()
    {
        audioSource.PlayOneShot(swing_1, globalVolume);
    }

    public void OnSwing2Sound()
    {
        audioSource.PlayOneShot(swing_2, globalVolume);
    }

    public void OnShootSound()
    {
        audioSource.PlayOneShot(bow, globalVolume);
    }
    public void OnJumpSound()
    {
        audioSource.PlayOneShot(jump, 0.3f);
    }
    public void OnDeathSound()
    {
        audioSource.PlayOneShot(death, globalVolume);
    }
}
