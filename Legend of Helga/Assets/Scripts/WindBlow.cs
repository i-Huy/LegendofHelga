using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WindBlow : MonoBehaviour
{
    public float force_x = 0f;
    public float force_y = 0f;

    public float force_z = 0f;

    Vector3 new_force;

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            Rigidbody rb = c.gameObject.GetComponent<Rigidbody>();

            new_force = new Vector3(force_x, force_y, force_z);
            //new_force = force_x * transform.right + force_y * transform.up + force_z * transform.forward;

            rb.AddForce(new_force, ForceMode.VelocityChange);
        }
    }
}