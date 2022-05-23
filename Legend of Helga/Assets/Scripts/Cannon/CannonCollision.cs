using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonCollision : MonoBehaviour
{
    public GameObject platform;
    public GameObject smoke;
    public GameObject explodeEffect;
    private GameObject smokeEffect;
    public Material bodyMat;
    public Material barrelMat;
    public int initialHP = 10;
    public int currHP;
    private float timeLastShot = 0f;
    private bool matChanged = false;

    GameObject audioEventManager_obj;
    AudioEventManager audioEventManager;

    void Start()
    {
        currHP = initialHP;

        audioEventManager_obj = GameObject.Find("AudioEventManager");
        audioEventManager = audioEventManager_obj.GetComponent<AudioEventManager>();
    }

    void Update()
    {
        if (smokeEffect != null)
        {
            smokeEffect.transform.position = this.transform.position;
        }
        if (smokeEffect == null && currHP <= 0)
        {
            Instantiate(explodeEffect, transform.position, transform.rotation);
            smokeEffect = Instantiate(smoke, this.transform.position, transform.rotation);
            platform.transform.rotation = Quaternion.Euler(70, 0, 0);
        }
    }

    void OnTriggerEnter(Collider c)
    {
        PlayerControl.PlayerController controller =
            FindObjectOfType<PlayerControl.PlayerController>();

        if (controller.weaponNbr == 1)
        {
            if (c.CompareTag("Sword") && Time.time > 0.5 + timeLastShot)
            {
                if (currHP != 0)
                {
                    currHP -= 2;
                    audioEventManager.SwordHitEvent();

                }
                if (currHP < 5 && !matChanged)
                {
                    GameObject.FindGameObjectsWithTag("CannonWheelBase")[0].GetComponent<MeshRenderer>().material = bodyMat;
                    GameObject.FindGameObjectsWithTag("CannonTopBase")[0].GetComponent<MeshRenderer>().material = bodyMat;
                    GameObject.FindGameObjectsWithTag("CannonBarrel")[0].GetComponent<MeshRenderer>().material = barrelMat;
                    matChanged = true;
                }
                if (currHP <= 0 && smokeEffect == null)
                {
                    // platform.SetActive(false)
                    Instantiate(explodeEffect, transform.position, transform.rotation);
                    smokeEffect = Instantiate(smoke, this.transform.position, transform.rotation);
                    platform.transform.rotation = Quaternion.Euler(70, 0, 0);
                }
                timeLastShot = Time.time;
            }
        }
        else if ((c.CompareTag("Hand") || c.CompareTag("Arrow")) && Time.time > 0.5 + timeLastShot)
        {
            if (currHP != 0)
            {
                currHP--;
                if (c.CompareTag("Arrow"))
                {
                    audioEventManager.ArrowHitEvent();
                }
                if (c.CompareTag("Hand")){
                    audioEventManager.HandHitEvent();
                }
            }
            if (currHP < 5 && !matChanged)
            {
                GameObject.FindGameObjectsWithTag("CannonWheelBase")[0].GetComponent<MeshRenderer>().material = bodyMat;
                GameObject.FindGameObjectsWithTag("CannonTopBase")[0].GetComponent<MeshRenderer>().material = bodyMat;
                GameObject.FindGameObjectsWithTag("CannonBarrel")[0].GetComponent<MeshRenderer>().material = barrelMat;
                matChanged = true;
            }
            if (currHP <= 0 && smokeEffect == null)
            {
                // platform.SetActive(false)
                Instantiate(explodeEffect, transform.position, transform.rotation);
                smokeEffect = Instantiate(smoke, this.transform.position, transform.rotation);
                platform.transform.rotation = Quaternion.Euler(70, 0, 0);
            }
            timeLastShot = Time.time;
        }
    }
}
