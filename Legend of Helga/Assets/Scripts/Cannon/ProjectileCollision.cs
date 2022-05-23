using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
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
        if (c.collider.gameObject.CompareTag("Player") ||
            c.collider.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
