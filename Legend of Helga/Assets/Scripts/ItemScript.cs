using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerControl;

public class ItemScript : MonoBehaviour
{
    private GameObject Chest;
    // Start is called before the first frame update
    private void Start()
    {
        Chest = this.transform.parent.gameObject;

    }

    private void OnTriggerEnter(Collider c)
    {

        if (c.CompareTag("Player"))
        {
            PlayerController p = c.GetComponent<PlayerController>();
            p.chest = Chest;
            Debug.Log("Setting Item");
        }
    }
    private void OnTriggerExit(Collider c)
    {

        if (c.CompareTag("Player"))
        {
            PlayerController p = c.GetComponent<PlayerController>();
            p.chest = null;
            Debug.Log("Clearing Item");
        }
    }
}
