using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI
{
    public class WalkableEnemyDeath : MonoBehaviour
    {
        private WalkableEnemyController controller;
        public int HP;

        GameObject audioEventManager_obj;
        AudioEventManager audioEventManager;

        // Start is called before the first frame update
        void Start()
        {
            controller = GetComponent<WalkableEnemyController>();
            if (controller == null)
            {
                Debug.LogError("Cannot find enemy AI controller");
            }

            audioEventManager_obj = GameObject.Find("AudioEventManager");
            audioEventManager = audioEventManager_obj.GetComponent<AudioEventManager>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnTriggerEnterChild(Collider other)
        {
            if (other.gameObject.CompareTag("Arrow")) {
                HP--;
                audioEventManager.ArrowHitEvent();
            }            
            else if(other.gameObject.CompareTag("Hand"))
            {
                HP--;
                audioEventManager.HandHitEvent();
            }
            else if (other.gameObject.CompareTag("Sword"))
            {
                HP -= 2;
                audioEventManager.SwordHitEvent();
            }
            if (HP <= 0)
            {
                controller.isDead = true;
            }
        }
    }
}
