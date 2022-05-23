using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerControl.Actions;
using System;
using UnityEngine.SceneManagement;

namespace PlayerControl
{
    public class InputController : MonoBehaviour
    {
        public PlayerController playerController;
        public float cameraRotationSpeed = 2f;
        PlayerStatus pStatus;

        private float inputX;
        private float inputZ;
        private float inputCameraRotationX;
        private float inputCameraRotationY;
        private bool inputJump;
        private bool isJumpHeld;
        private bool inputDeath;
        private bool inputAttackL;
        private bool inputAttackR;
        private bool inputRoll;
        private bool inputAim;
        private bool inputUnarmed;
        private bool inputSword;
        private bool inputBow;
        private bool inputAimShoot;
        private bool inputAimRelease;
        private bool inputInteract;
        private bool inputPickUp;

        private Vector3 moveInput;
        private Vector3 jumpInput;

        private void Awake()
        {
            playerController = GetComponent<PlayerController>();
            pStatus = GetComponent<PlayerStatus>();
        }

        // Update is called once per frame
        private void Update()
        {
            InputCheck();
            WeaponSwitch();
            Moving();
            CameraRotation();
            Rolling();
            AimingShoot();
            //Attacking();
            Aiming();
            Interacting();
            PickingUp();
            if (!SceneManager.GetActiveScene().name.Contains("Wind"))
            {
                Attacking();
            }
        }

        private void InputCheck()
        {
            try
            {
                inputX = Input.GetAxisRaw("Horizontal");
                inputZ = Input.GetAxisRaw("Vertical");
                inputJump = Input.GetButtonDown("Jump");
                isJumpHeld = Input.GetButton("Jump");
                inputDeath = Input.GetButtonDown("Death");
                inputAttackL = Input.GetButtonDown("AttackL");
                inputAttackR = Input.GetButtonDown("AttackR");
                inputRoll = Input.GetButtonDown("L3");
                inputAim = Input.GetButtonDown("Aim");

                // weapon
                inputUnarmed = Input.GetKeyDown(KeyCode.Alpha1);
                inputSword = Input.GetKeyDown(KeyCode.Alpha2);
                inputBow = Input.GetKeyDown(KeyCode.Alpha3);
                inputAimShoot = Input.GetKey(KeyCode.Mouse0);
                inputAimRelease = Input.GetKeyUp(KeyCode.Mouse0);

                // Camera rotation
                inputCameraRotationX = Input.GetAxisRaw("Mouse X");
                inputCameraRotationY = Input.GetAxisRaw("Mouse Y");

                // Interact
                inputInteract = Input.GetButtonDown("Interact");
                inputPickUp = Input.GetButtonDown("PickUp");
            }
            catch (System.Exception)
            {
                Debug.LogError("Input not implemented");
            }
        }

        private void WeaponSwitch()
        {
            int weaponNbr = playerController.weaponNbr;

            if (inputUnarmed)
                weaponNbr = 0;
            else if (inputSword && pStatus.hasSword)
                weaponNbr = 1;
            else if (inputBow && pStatus.hasBow)
                weaponNbr = 2;
            else
                weaponNbr = playerController.weaponNbr;

            playerController.SetWeaponNbr(weaponNbr);
        }

        private void Moving()
        {
            // Debug.Log(inputX.ToString() + ", " + inputZ.ToString());
            float h = inputX * Mathf.Sqrt(1.0f - 0.5f * inputZ * inputZ);
            float v = inputZ * Mathf.Sqrt(1.0f - 0.5f * inputX * inputX);
            moveInput = new Vector3(h, 0f, v);
            playerController.SetMoveInput(moveInput);

            jumpInput = isJumpHeld ? Vector3.up : Vector3.zero;
            playerController.SetJumpInput(jumpInput);

            if (inputJump && playerController.CanStartAction("Jump"))
            {
                playerController.StartAction("Jump");
            }
        }

        private void CameraRotation()
        {
            Vector3 rot = new Vector3(
                inputCameraRotationX / cameraRotationSpeed,
                inputCameraRotationY / cameraRotationSpeed,
                0f);
            playerController.SetCameraRotation(rot);
        }

        private void Rolling()
        {
            if (!inputRoll || !playerController.CanStartAction("DiveRoll"))
            {
                return;
            }

            int rollNbr = 0;
            Vector3 dir = playerController.xzVelocity;
            // dir.Normalize();
            float xd = transform.InverseTransformDirection(dir).x;
            float zd = transform.InverseTransformDirection(dir).z;

            // Debug.Log(dir.ToString());
            if (Mathf.Abs(xd) < Mathf.Abs(zd))
            {
                if (zd > 0)
                    rollNbr = 2;
                else
                    rollNbr = 3;
            }
            else
            {
                if (xd > 0)
                    rollNbr = 5;
                else
                    rollNbr = 4;
            }

            playerController.StartAction("DiveRoll", rollNbr);
        }

        private void Attacking()
        {
            if (!playerController.CanStartAction("Attack")) { return; }

            if (inputAttackL)
            {
                playerController.StartAction(
                    "Attack",
                    new Actions.AttackContext("Attack", "Left", 3));
            }
            else if (inputAttackR)
            {
                playerController.StartAction(
                    "Attack",
                    new Actions.AttackContext("Attack", "Right", 5));
            }
        }

        private void AimingShoot()
        {
            if (inputAimShoot)
            {
                playerController.StartAction("ShootAim");
            }

            if (inputAimRelease)
            {
                if (playerController.IsActive("ShootAim"))
                {
                    // Debug.Log("Aim Release");
                    playerController.EndAction("ShootAim");
                    playerController.StartAction("ShootRelease");
                    playerController.EndAction("ShootRelease");
                }
            }
        }

        private void Aiming()
        {
            if (playerController.aimTarget != null &&
                !IsTargetInRange())
            {
                if (playerController.CanEndAction("Aim"))
                    playerController.EndAction("Aim");
                return;
            }

            if (!inputAim)
                return;

            if (playerController.IsActive("Aim"))
            {
                if (playerController.CanEndAction("Aim"))
                    playerController.EndAction("Aim");
            }
            else
            {
                if (playerController.CanStartAction("Aim"))
                    playerController.StartAction("Aim");
            }
        }

        private bool IsTargetInRange()
        {
            Vector3 distance = playerController.gameObject.transform.position -
                playerController.aimTarget.position;
            Vector3 planeDistance = Vector3.ProjectOnPlane(
                distance, Vector3.up);

            if (planeDistance.magnitude < 10f)
                return true;
            else
                return false;
        }

        private void Interacting()
        {
            if (inputInteract)
            {
                //Debug.Log("Interacting");
                playerController.StartAction("Interact");
            }
        }
        private void PickingUp()
        {
            if (inputPickUp)
            {
                //Debug.Log("PickingUpItem");
                playerController.StartAction("PickUpItem");
            }
        }
    }
}
