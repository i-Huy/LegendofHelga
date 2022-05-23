using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyEnemyHitEffect : MonoBehaviour
{
    private Rigidbody rb;
    public float forceUnit = 10.0f;
    GameObject audioEventManager_obj;
    AudioEventManager audioEventManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioEventManager_obj = GameObject.Find("AudioEventManager");
        audioEventManager = audioEventManager_obj.GetComponent<AudioEventManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision c)
    {
        if (c.collider.CompareTag("Hand"))
        {
            ContactPoint cp = c.GetContact(0);
            rb.AddForceAtPosition(
                forceUnit * cp.normal, cp.point, ForceMode.Impulse);

            audioEventManager.HandHitEvent();
        }

        else if (c.gameObject.CompareTag("Arrow"))
        {
            audioEventManager.ArrowHitEvent();
        }
        else if (c.gameObject.CompareTag("Sword"))
        {
            audioEventManager.SwordHitEvent();
        }
    }
}
