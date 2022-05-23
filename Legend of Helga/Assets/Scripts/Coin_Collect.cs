using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_Collect : MonoBehaviour
{
    void OnTriggerEnter(Collider c)
    {
 
        if (c.gameObject.name == "PlayerTest")
        {
            GetComponent<Renderer>().enabled = false;
            // TODO: add logic
        }
    }
}
