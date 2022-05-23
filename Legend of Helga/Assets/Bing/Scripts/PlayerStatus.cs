using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerControl
{
    public class PlayerStatus : MonoBehaviour
    {
        public int initialHP;
        [HideInInspector] public bool isDead;
        public int currHP;
        public bool hasSword;
        public bool hasBow;
        public int previousHP;
        GameObject audioEventManager_obj;
        AudioEventManager audioEventManager;

        private PlayerController controller;

        // Start is called before the first frame update
        void Start()
        {
            currHP = initialHP;
            hasSword = false;
            hasBow = false;
            if (GlobalControl.Instance != null)
            {
                currHP = GlobalControl.Instance.HP;
                initialHP = GlobalControl.Instance.InitialHP;
                hasSword = GlobalControl.Instance.hasSword;
                hasBow = GlobalControl.Instance.hasBow;
            }

            controller = GetComponent<PlayerController>();

            audioEventManager_obj = GameObject.Find("AudioEventManager");
            audioEventManager = audioEventManager_obj.GetComponent<AudioEventManager>();
        }

        // Update is called once per frame
        void Update()
        {
            if (currHP < previousHP)
            {
                audioEventManager.PlayerHitEvent();
            }
            previousHP = currHP;

            isDead = controller.isDead;
            SavePlayer();
        }

        public void RestoreHP()
        {
            currHP = initialHP;
        }

        void OnCollisionEnter(Collision c)
        {
            if (c.collider.CompareTag("Projectile"))
            {
                currHP -= 1;
            }
        }

        public void SavePlayer()
        {
            GlobalControl.Instance.HP = currHP;
            GlobalControl.Instance.hasSword = hasSword;
            GlobalControl.Instance.hasBow = hasBow;
        }
    }
}
