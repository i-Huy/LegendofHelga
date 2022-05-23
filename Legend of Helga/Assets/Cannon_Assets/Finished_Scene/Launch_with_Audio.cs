using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch_with_Audio : MonoBehaviour
{

    public GameObject projectile;
    public float launchVelocity = 700f;
    public AudioClip shootsound;
    public float lowVolumeRange = .5f;
    public float highVolumeRange = 1.0f;
    private AudioSource source;


    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            float vol = Random.Range(lowVolumeRange, highVolumeRange);
            source.PlayOneShot(shootsound, vol);

            GameObject launchThis = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
            launchThis.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, launchVelocity, 0));
        }
    }

}
