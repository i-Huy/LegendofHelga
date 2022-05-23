using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerControl
{
    public class GroundUtilities : MonoBehaviour
    {
        private int _onGroundCount;
        private PlayerController playerController;
        private bool placeholder;
        private float center = 1.25f;
        private float delta = 0.05f;

        void Awake()
        {
            _onGroundCount = 0;

            playerController = GetComponent<PlayerController>();
            if (playerController == null)
            {
                Debug.LogError("GroundUtilities cannot find main controller");
            }

            playerController.SetHandler(
                "OnGround",
                new Actions.SimpleActionHandler(() => { }, () => { }));
        }

        void Update()
        {
            if (_onGroundCount > 0 || PlayerUtilities.GroundNearChecker(
                this.transform.position, 45f, 1.25f, 0.1f, out placeholder))
            {
                playerController.StartAction("OnGround");
            }
            else
            {
                playerController.EndAction("OnGround");
            }

            DecideGroundType();
        }

        void OnCollisionEnter(Collision c)
        {
            if (c.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                ++_onGroundCount;
            }
        }

        void OnCollisionExit(Collision c)
        {
            if (c.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                --_onGroundCount;
            }
        }

        public void DecideGroundType()
        {
            string _type = "normal";

            Ray ray = new Ray(
                this.transform.position + 1.25f * Vector3.up, Vector3.down);
            int layerMask = 1 << 6;
            RaycastHit[] allHits = Physics.RaycastAll(
                ray, center + delta, layerMask);

            foreach (RaycastHit hit in allHits)
            {
                if (hit.collider.gameObject.CompareTag("Lava"))
                {
                    _type = "Lava";
                }
            }

            playerController.SetGroundType(_type);
        }
    }
}
