using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PlayerAnimatorMoveEvent : UnityEvent<Vector3, Quaternion> { }

namespace PlayerControl
{
    public class PlayerAnimationEvents : MonoBehaviour
    {
        public UnityEvent OnHit = new UnityEvent();
        public UnityEvent OnFootR = new UnityEvent();
        public UnityEvent OnFootL = new UnityEvent();
        public UnityEvent OnLand = new UnityEvent();
        public UnityEvent OnHitR = new UnityEvent();
        public UnityEvent OnHitL = new UnityEvent();
        public UnityEvent OnSwing1 = new UnityEvent();
        public UnityEvent OnSwing2 = new UnityEvent();
        public UnityEvent OnShoot = new UnityEvent();
        public UnityEvent OnJump = new UnityEvent();
        public UnityEvent OnDeath = new UnityEvent();
        public PlayerAnimatorMoveEvent OnMove = new PlayerAnimatorMoveEvent();

        PlayerController playerController;
        Animator animator;


        void Awake()
        {
            playerController = GetComponentInParent<PlayerController>();
            animator = GetComponent<Animator>();
        }
        public void HitR()
        {
            OnHitR.Invoke();
        }
        
        public void HitL()
        {
            OnHitL.Invoke();
        }

        public void HitBoxOn()
        {
            if (playerController.weaponNbr == 1)
            {
                playerController.currWeapon.gameObject.
                    GetComponent<BoxCollider>().enabled = true;
            }
        }

        public void HitBoxOff()
        {
            if (playerController.weaponNbr == 1)
            {
                playerController.currWeapon.gameObject.
                    GetComponent<BoxCollider>().enabled = false;
            }
        }

        public void FootR()
        {
            OnFootR.Invoke();
        }

        public void FootL()
        {
            OnFootL.Invoke();
        }

        public void Land()
        {
            OnLand.Invoke();
        }

        void OnAnimatorMove()
        {
            if (animator)
            {
                OnMove.Invoke(animator.deltaPosition, animator.rootRotation);
            }
        }

        void Shoot()
        {
            playerController.ReleaseArrow();
            OnShoot.Invoke();
        }

        public void Swing1()
        {
            OnSwing1.Invoke();
        }

        public void Swing2()
        {
            OnSwing2.Invoke();
        }
        public void Jump()
        {
            OnJump.Invoke();
        }

        public void Death()
        {
            OnDeath.Invoke();
        }
    }
}
