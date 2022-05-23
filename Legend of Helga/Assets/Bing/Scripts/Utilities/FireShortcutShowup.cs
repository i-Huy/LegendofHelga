using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShortcutShowup : MonoBehaviour
{
    PlayerControl.PlayerController controller;
    PlayerControl.PlayerStatus status;

    public GameObject shortcut;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "PlayerTest")
        {
            Debug.LogError("Here");
            controller = collision.gameObject.
                GetComponent<PlayerControl.PlayerController>();
            status = collision.gameObject.
                GetComponent<PlayerControl.PlayerStatus>();

            if (status.hasBow && controller.weaponNbr == 2)
            {
                shortcut.SetActive(true);
            }
        }
    }
}
