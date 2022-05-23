using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioEventManager : MonoBehaviour
{
    public UnityEvent GotReward = new UnityEvent();
    public UnityEvent SwordHit = new UnityEvent();
    public UnityEvent ArrowHit = new UnityEvent();
    public UnityEvent HandHit = new UnityEvent();
    public UnityEvent PlayerHit = new UnityEvent();
    public UnityEvent CannonLaunch = new UnityEvent();
    public UnityEvent Explosion = new UnityEvent();
    AudioSource audioSource;
    AudioClip reward;
    AudioClip hit_enemy_sword;
    AudioClip hit_other;
    AudioClip hit_enemy_arrow;
    AudioClip hit_enemy_hand;
    AudioClip hit_player;
    AudioClip cannon_launch;
    AudioClip explosion;

    public float volume_reward = 0.5f;
    public float volume_hit_sword = 0.5f;
    public float volume_hit_arrow = 0.5f;
    public float volume_hit_hand = 0.5f;
    public float volume_hit_player = 0.5f;
    public float volume_cannon_launch = 0.5f;
    public float volume_explosion = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        reward = Resources.Load<AudioClip>("SoundEffects/Reward");
        hit_enemy_sword = Resources.Load<AudioClip>("SoundEffects/hit_enemy");
        hit_other = Resources.Load<AudioClip>("SoundEffects/hit_other");
        hit_enemy_arrow = Resources.Load<AudioClip>("SoundEffects/Arrow Hit");
        hit_enemy_hand = Resources.Load<AudioClip>("PlayerSounds/Punch 2");
        hit_player = Resources.Load<AudioClip>("PlayerSounds/oww");
        cannon_launch = Resources.Load<AudioClip>("SoundEffects/Cannon Launch");
        explosion = Resources.Load<AudioClip>("SoundEffects/Explosion");

        GotReward.AddListener(PlayRewardSound);
        SwordHit.AddListener(PlaySwordHitSound);
        ArrowHit.AddListener(PlayArrowHitSound);
        HandHit.AddListener(PlayHandHitSound);
        PlayerHit.AddListener(PlayPlayerHitSound);
        CannonLaunch.AddListener(PlayCannonLaunchSound);
        Explosion.AddListener(PlayExplosionSound);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RewardEvent()
    {
        GotReward.Invoke();
    }

    public void SwordHitEvent()
    {
        SwordHit.Invoke();
    }
    public void ArrowHitEvent()
    {
        ArrowHit.Invoke();
    }
    public void HandHitEvent()
    {
        HandHit.Invoke();
    }
    public void PlayerHitEvent()
    {
        PlayerHit.Invoke();
    }
    public void CannonLaunchEvent()
    {
        CannonLaunch.Invoke();
    }
    public void ExplosionEvent()
    {
        Explosion.Invoke();
    }

    private void PlayRewardSound()
    {
        audioSource.PlayOneShot(reward, volume_reward);
    }

    private void PlaySwordHitSound()
    {
        audioSource.PlayOneShot(hit_enemy_sword, volume_hit_sword);
    }
    private void PlayArrowHitSound()
    {
        audioSource.PlayOneShot(hit_enemy_arrow, volume_hit_arrow);
    }
    private void PlayHandHitSound()
    {
        audioSource.PlayOneShot(hit_enemy_hand, volume_hit_hand);
    }
    private void PlayPlayerHitSound()
    {
        audioSource.PlayOneShot(hit_player, volume_hit_player);
    }
    private void PlayCannonLaunchSound()
    {
        audioSource.PlayOneShot(cannon_launch, volume_cannon_launch);
    }
    private void PlayExplosionSound()
    {
        audioSource.PlayOneShot(explosion, volume_explosion);
    }
}
