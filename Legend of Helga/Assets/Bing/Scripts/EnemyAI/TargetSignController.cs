using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI
{
    public class TargetSignController : MonoBehaviour
    {
        private Transform sign;
        private bool signOn;
        private GameObject target;

        // Start is called before the first frame update
        void Start()
        {
            sign = this.transform.Find("Sign");
            sign.gameObject.SetActive(false);
            signOn = false;
            target = null;
        }

        // Update is called once per frame
        void Update()
        {
            if (GetComponentInParent<WalkableEnemyController>() != null)
            {
                if (GetComponentInParent<WalkableEnemyController>().isDead)
                {
                    signOn = false;
                    target = null;
                }
            }
            if (target != null &&
                target.GetComponent<PlayerControl.PlayerController>().isDead)
            {
                signOn = false;
                target = null;
            }

            if (!signOn)
            {
                sign.gameObject.SetActive(false);
            }
            else
            {
                sign.gameObject.SetActive(true);
                if (target == null) return;

                PlayerControl.PlayerController otherController =
                    target.GetComponent<PlayerControl.PlayerController>();
                GameObject camera;
                if (otherController.followCamera.activeSelf)
                    camera = otherController.followCamera;
                else
                    camera = otherController.aimCamera;

                Vector3 dir = camera.transform.position - transform.position;
                dir.Normalize();
                transform.forward = dir;
            }
        }

        public void TurnSignOn(GameObject other)
        {
            target = other;
            signOn = true;
        }

        public void TurnSignOff()
        {
            target = null;
            signOn = false;
        }
    }
}
