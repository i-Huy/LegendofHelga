using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerControl
{
    public class WeaponIK : MonoBehaviour
    {
        private PlayerController controller;
        private Animator animator;
        private Transform arrowRightHandSpot;

        // Start is called before the first frame update
        void Start()
        {
            controller = this.GetComponentInParent<PlayerController>();
            if (controller == null)
            {
                Debug.LogError("Player Controller cannot be found");
            }

            animator = this.GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError("Animator cannot be found");
            }
        }

        // Update is called once per frame
        void OnAnimatorIK()
        {
            // Debug.Log("here");
            // if weapon is bow
            if (controller.weaponNbr == 2 &&
                (controller.IsActive("ShootAim") ||
                controller.IsActive("ShootRelease")))
            {
                if (animator == null)
                    return;
                arrowRightHandSpot = controller.currArrow.gameObject.transform.Find("RightHandHoldSpot");
                if (arrowRightHandSpot == null)
                {
                    Debug.LogError("Cannot find left hand hold spot for arrow");
                }

                if (arrowRightHandSpot != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKPosition(
                        AvatarIKGoal.RightHand, arrowRightHandSpot.position);
                    animator.SetIKRotation(
                        AvatarIKGoal.RightHand, arrowRightHandSpot.rotation);
                }
                else
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                }
            }
        }
    }
}
