using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerControl
{
    public enum PlayerState
    {
        Idle = 0,
        Move = 1,
        Jump = 2,
        Fall = 3,
        DiveRoll = 4,
    }

    public class MovementController : StateMachine
    {
        private PlayerController playerController;
        private Rigidbody rb;
        private Animator animator;
        private CapsuleCollider capCollider;

        public float movementAnimationMultiplier = 1.0f;
        [HideInInspector] public Vector3 currVelocity;
        [HideInInspector] public Vector3 moveDirection;
        public float runSpeed = 1.0f;
        public float runAccl = 25f;
        public float walkSpeed = 0.5f;
        public float walkAccl = 10f;
        public float rotationSpeed = 1.0f;
        public float followRotationSpeed = 1.0f;
        [HideInInspector] public bool canJump;
        [HideInInspector] public bool highJump;
        public float jumpSpeed = 12f;
        public float jumpGravity = 24f;
        public float fallGravity = 32f;
        public float airXZSpeed = 8f;
        public float airXZAccl = 30f;
        public float groundFriction = 120f;

        /* private float forwardInput = 0f;
        private float turnInput = 0f;
        private float forwardFilter = 5f;
        private float turnFilter = 5f;
        private bool useCircular = true;
        private float speedLimit = 1.0f; */

        public float fallThreshold = 5f;
        private float fallTime;
        private PlayerStatus pStatus;



        void Awake()
        {
            playerController = GetComponent<PlayerController>();
            playerController.SetHandler("DiveRoll", new Actions.DiveRoll(this));
            playerController.SetHandler("Idle", new Actions.Idle(this));
            playerController.SetHandler("Move", new Actions.Move(this));
            playerController.SetHandler("Jump", new Actions.Jump(this));
            playerController.SetHandler("Fall", new Actions.Fall(this));
        }

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponentInChildren<Animator>();
            pStatus = GetComponent<PlayerStatus>();
            rb = GetComponent<Rigidbody>();
            capCollider = GetComponent<CapsuleCollider>();

            playerController.OnLockMovement += LockMovement;
            playerController.OnUnlockMovement += UnlockMovement;

            if (rb != null)
            {
                rb.constraints = RigidbodyConstraints.FreezeRotationX |
                    RigidbodyConstraints.FreezeRotationZ;
                rb.constraints = rb.constraints |
                    RigidbodyConstraints.FreezeRotationY;
            }

            PlayerAnimationEvents animationEvents =
                animator.GetComponent<PlayerAnimationEvents>();
            animationEvents.OnMove.AddListener(AnimatorMove);

            fallTime = 0f;
        }

        // Update is called once per frame
        void Update()
        {
            CallUpdate();
        }

        void FixedUpdate()
        {
            // transform.Translate(currVelocity * Time.deltaTime, Space.World);
            rb.MovePosition(rb.position + currVelocity * Time.deltaTime);
        }

        protected override void BeforeStateUpdate()
        {
            playerController.RotateFollowTarget();
        }

        protected override void AfterStateUpdate()
        {
            // Debug.Log(currVelocity.ToString());
            // this.transform.Translate(
            //     currVelocity * Time.deltaTime, Space.World);

            if (!playerController.isDead && playerController.canMove)
            {
                if (currVelocity.magnitude > 0f)
                {
                    /* animator.SetFloat("Velx", 0f);
                    animator.SetFloat(
                        "Velz",
                        transform.InverseTransformDirection(currVelocity).z * movementAnimationMultiplier); */
                    animator.SetBool("Moving", true);
                }
                else
                {
                    // animator.SetFloat("Velx", 0f);
                    // animator.SetFloat("Velz", 0f);
                    animator.SetBool("Moving", false);
                }
            }

            if (currState == null && playerController.CanStartAction("Idle"))
            {
                playerController.StartAction("Idle");
            }

            if (playerController.IsActive("ShootAim") ||
                playerController.IsActive("ShootRelease"))
            {
                RotateTowardsCamera();
            }
            else if (playerController.isAiming)
            {
                RotateTowardsTraget();
            }
            else if (playerController.canMove)
            {
                // RotateTowardsCamera();

                moveDirection = new Vector3(currVelocity.x, 0f, currVelocity.z);
                moveDirection.Normalize();

                if (moveDirection.magnitude > 0.01f)
                {
                    playerController.followTarget.parent = null;
                    Quaternion toRotation = Quaternion.LookRotation(
                        moveDirection);
                    Quaternion actualRotation = Quaternion.Slerp(
                        transform.rotation, toRotation,
                        rotationSpeed * Time.deltaTime);
                    this.transform.rotation = actualRotation;
                    playerController.followTarget.parent = this.transform;
                }
            }

            animator.SetFloat(
                "Velx",
                transform.InverseTransformDirection(currVelocity).x * movementAnimationMultiplier);
            animator.SetFloat(
                "Velz",
                transform.InverseTransformDirection(currVelocity).z * movementAnimationMultiplier);
        }

        // About states
        private void Idle_EnterState()
        {
            canJump = true;
        }

        private void Idle_DoUpdate()
        {
            currVelocity = Vector3.MoveTowards(
                currVelocity, Vector3.zero, groundFriction * Time.deltaTime);


            if (playerController.CanStartAction("Move"))
            {
                // Debug.Log("Here");
                playerController.StartAction("Move");
            }
        }

        private void Move_DoUpdate()
        {
            if (playerController.CanStartAction("Fall"))
            {
                playerController.StartAction("Fall");
                return;
            }

            if (playerController.canMove)
            {
                float conditionalSpeed = runSpeed;
                float conditionalAccl = runAccl;
                // Vector3 _h = playerController.moveInput.x * transform.right;
                // Vector3 _v = playerController.moveInput.z * 
                //    transform.forward;

                if (playerController.isAiming)
                {
                    conditionalSpeed = walkSpeed;
                    conditionalAccl = walkAccl;
                }

                currVelocity = Vector3.MoveTowards(
                    currVelocity,
                    playerController.xzVelocity * conditionalSpeed,
                    conditionalAccl * Time.deltaTime);
            }

            if (playerController.CanStartAction("Idle"))
            {
                playerController.StartAction("Idle");
            }
        }

        private void Jump_EnterState()
        {
            currVelocity = new Vector3(
                currVelocity.x, jumpSpeed, currVelocity.z);

            animator.SetInteger("JumpStyle", 1);
            playerController.SetAnimatorTrigger(AnimatorTrigger.JumpTrigger);
            canJump = false;
        }

        private void Jump_DoUpdate()
        {
            // Debug.Log(currVelocity.y.ToString());
            highJump = playerController.jumpInput.y > 0;

            if (!highJump && currVelocity.y > (jumpSpeed / 4f))
            {
                currVelocity = Vector3.MoveTowards(
                    currVelocity,
                    new Vector3(currVelocity.x, jumpSpeed / 4f, currVelocity.z),
                    fallGravity * Time.deltaTime);
            }

            Vector3 xzVelocity = Vector3.ProjectOnPlane(
                currVelocity, transform.up);
            Vector3 yVelocity = currVelocity - xzVelocity;

            if (currVelocity.y < 0f)
            {
                currVelocity = xzVelocity;
                currState = PlayerState.Fall;
                return;
            }

            xzVelocity = Vector3.MoveTowards(
                xzVelocity,
                playerController.xzVelocity * airXZSpeed,
                airXZAccl * Time.deltaTime);
            yVelocity -= transform.up * jumpGravity * Time.deltaTime;
            currVelocity = xzVelocity + yVelocity;
        }

        private void Fall_EnterState()
        {
            canJump = false;

            animator.SetInteger("JumpStyle", 2);
            playerController.SetAnimatorTrigger(AnimatorTrigger.JumpTrigger);
            fallTime = 0f;
        }

        private void Fall_DoUpdate()
        {
            if (playerController.CanStartAction("Idle"))
            {
                currVelocity = Vector3.ProjectOnPlane(
                    currVelocity, transform.up);
                playerController.StartAction("Idle");
                return;
            }
            currVelocity -= transform.up * fallGravity * Time.deltaTime;
            fallTime += Time.deltaTime;
        }

        private void Fall_ExitState()
        {
            animator.SetInteger("JumpStyle", 0);
            playerController.SetAnimatorTrigger(AnimatorTrigger.JumpTrigger);

            if (playerController.onGround)
            {
                Land();
                if (fallTime >= fallThreshold)
                {
                    playerController.isDead = true;
                    animator.SetBool("isDead", true);
                    pStatus.currHP = 0;
                }
            }
        }

        private void DiveRoll_EnterState()
        {
            playerController.OnUnlockMovement += IdleOnceAfterMoveUnlock;
        }

        private void DiveRoll_DoUpdate()
        {
            if (playerController.CanStartAction("Idle"))
            {
                playerController.StartAction("Idle");
            }
        }

        private void DiveRoll_ExitState()
        {
            playerController.burnEffect.enabled = false;
        }

        private void Land()
        {
            currVelocity = Vector3.zero;
        }

        private void RotateTowardsTraget()
        {
            Vector3 faceDirection = playerController.aimTarget.position -
                this.transform.position;
            faceDirection = Vector3.ProjectOnPlane(faceDirection, Vector3.up);
            faceDirection.Normalize();

            if (faceDirection.magnitude > 0.01f)
            {
                Quaternion toRotation = Quaternion.LookRotation(
                    faceDirection);
                Quaternion actualRotation = Quaternion.Slerp(
                    transform.rotation, toRotation,
                    rotationSpeed * Time.deltaTime);
                this.transform.rotation = actualRotation;
            }
        }

        private void RotateTowardsCamera()
        {
            /* Vector3 faceDirection = Camera.main.transform.TransformDirection(
                Vector3.forward);
            faceDirection = Vector3.ProjectOnPlane(faceDirection, Vector3.up);
            faceDirection.Normalize();

            if (faceDirection.magnitude > 0.01f)
            {
                Quaternion toRotation = Quaternion.LookRotation(
                    faceDirection);
                Quaternion actualRotation = Quaternion.Slerp(
                    transform.rotation, toRotation,
                    rotationSpeed * Time.deltaTime);
                this.transform.rotation = actualRotation;
            } */
            var angles = playerController.followTarget.transform.
                localEulerAngles;
            Quaternion target = Quaternion.Euler(
                0f,
                playerController.followTarget.transform.rotation.eulerAngles.y,
                0f);
            Quaternion smoothedRot = Quaternion.Slerp(
                transform.rotation, target,
                followRotationSpeed * Time.deltaTime);
            this.transform.rotation = smoothedRot;
            playerController.followTarget.localEulerAngles = new Vector3(
                angles.x, 0f, 0f);
        }

        // Some useful delegates
        public void IdleOnceAfterMoveUnlock()
        {
            playerController.StartAction("Idle");
            playerController.OnUnlockMovement -= IdleOnceAfterMoveUnlock;
        }

        public void LockMovement()
        {
            currVelocity = new Vector3(0, 0, 0);
            animator.SetBool("Moving", false);
            animator.applyRootMotion = true;

            if (playerController.IsActive("Attack"))
            {
                playerController.leftHandCollider.enabled = true;
                playerController.rightHandCollider.enabled = true;
                rb.isKinematic = true;
            }
        }

        public void UnlockMovement()
        {
            animator.applyRootMotion = false;

            playerController.leftHandCollider.enabled = false;
            playerController.rightHandCollider.enabled = false;
            rb.isKinematic = false;
        }

        public void AnimatorMove(Vector3 deltaPosition, Quaternion rootRotation)
        {
            transform.position += deltaPosition;
            transform.rotation = rootRotation;
        }
    }
}
