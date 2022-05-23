using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerControl
{
    public class PlayerHitboxCollider : MonoBehaviour
    {
        private PlayerController controller;
        public float lastAttackLimit = 0.6f;
        private float lastAttackTimer;

        // Start is called before the first frame update
        void Start()
        {
            controller = GetComponentInParent<PlayerController>();
        }

        // Update is called once per frame
        void Update()
        {
            // lastAttackTimer += Time.deltaTime;
        }

        void OnTriggerEnter(Collider other)
        {
            controller.OnTriggerEnterHitbox(other);
        }
    }
}
