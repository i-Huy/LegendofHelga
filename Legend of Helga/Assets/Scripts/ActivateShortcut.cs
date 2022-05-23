using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerControl;

public class ActivateShortcut : MonoBehaviour
{
    public GameObject shortCutPlatform;
    private GameObject player;
    private PlayerStatus pStatus;

    private GameObject globalControlObject;

    private GlobalControl globalControl;

    public GameObject redCrystal;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        pStatus = player.GetComponent<PlayerStatus>();
        globalControlObject = GameObject.Find("GlobalObject");
        globalControl = globalControlObject.GetComponent<GlobalControl>();

        shortCutPlatform.SetActive(false);
    }

    // Update is called once per frame
    // void Update()
    // {
    //     //if (pStatus.hasSword)
    //     if (globalControl.hasSword)
    //     {
    //         shortCutPlatform.SetActive(true);
    //     }
    //     else{
    //         shortCutPlatform.SetActive(false);
    //     }
    // }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.name.Contains("Sword"))
        {
            shortCutPlatform.SetActive(true);
            Animator redCrystalAnimator;
            redCrystalAnimator = redCrystal.GetComponent<Animator>();

            if (redCrystalAnimator != null)
            {
                redCrystalAnimator.SetBool("isTrigger", true);
            }
            shortCutPlatform.SetActive(true);
        }
    }
}
