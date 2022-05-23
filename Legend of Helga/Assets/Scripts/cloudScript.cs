using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloudScript : MonoBehaviour
{
    private Rigidbody rb;

    private void OnTriggerEnter(Collider other) {
        if (other.transform.tag == "Player")
            other.transform.parent = transform;
            rb = other.GetComponent<Rigidbody>();
            rb.velocity = new Vector3(0, 0, 0);
    }
    private void OnTriggerExit(Collider other) {
        if (other.transform.tag == "Player")
        other.transform.parent = null;
    }
}
