using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transport_wind : MonoBehaviour
{
    public GameObject coupleWind;

    public Vector3 standvector = new Vector3(0, 0, 0);

    private float stayTimer = 1.0f;

    private float timeRemaining;

    private GameObject player;

    private bool isTrigger = false;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isTrigger)
        {
            if (timeRemaining > 0){
                timeRemaining -= Time.deltaTime;
            }
            else{
                // transfer to the position of couple wind
                isTrigger = false;
                player.transform.position = coupleWind.transform.position + standvector;
                
            }
        }
    }

    void OnTriggerEnter(Collider c)
    {
        // start counting down the timer
        if (c.gameObject == player){
            isTrigger = true;
            timeRemaining = stayTimer;
        }
        
    }

    void OnTriggerExit(Collider c)
    {
        // stop counting down
        if (c.gameObject == player)
        {
            isTrigger = false;
        }
        
    }
}
