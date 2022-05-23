using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    bool isTriggered;

    private void Awake()
    {
        isTriggered = false;
    }

    // called whenever another collider enters our zone (if layers match)
    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggered)
        {
            if (other.gameObject.layer
                == LayerMask.NameToLayer("Player"))
            {
                Trigger();
            }
        }
    }

    void Trigger()
    {
        // Tell the animation controller about our recent triggering
        GetComponent<Animator>().SetTrigger("Triggered");
        isTriggered = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
