using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCannon : MonoBehaviour
{
    // public GameObject cannonBoss;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        // if (other.collider.CompareTag("Hand"))
        // {
        //    ContactPoint cp = other.GetContact(0);
            //rb.AddForceAtPosition(
                //forceUnit * cp.normal, cp.point, ForceMode.Impulse);
        // }
        if(other.gameObject.tag == "BossCannon")
        {
            Debug.Log("I hit the enemy!");
            // cannonBoss.GetComponent<CannonController>().Damage();
        }
    }
}
