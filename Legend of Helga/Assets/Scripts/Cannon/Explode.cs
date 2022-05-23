using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerControl;

public class Explode : MonoBehaviour
{
    public GameObject explosionEffect;
    public GameObject aoe;
    // private PlayerStatus player = null;
    public bool exploded = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision c)
    {
        if (c.collider.CompareTag("Player") || c.collider.CompareTag("Ground"))
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(gameObject);

            Vector3 collisionPoint = c.contacts[0].point;
            Collider[] objects = UnityEngine.Physics.OverlapSphere(collisionPoint, 5);
            foreach(Collider h in objects)
            {
                Rigidbody r = h.GetComponent<Rigidbody>();
                PlayerStatus player = null;
                if(h.tag == "Player")
                {
                    player = h.GetComponent<PlayerStatus>();
                }
                if(r != null)
                {
                    r.AddExplosionForce(50000.0f, collisionPoint, 5.0f);
                    if(player != null)
                    {
                        player.currHP -= 2;
                    }
                }
            }
        }
    }
}
