using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerControl.Actions;
using UnityEngine.VFX;
using UnityEngine.SceneManagement;

namespace PlayerControl
{
    public class PlayerController : MonoBehaviour
    {
        private Dictionary<string, PlayerActionHandler> actionHandlers =
            new Dictionary<string, PlayerActionHandler>();

        public Animator animator;
        public float animationSpeed = 1.0f;
        public float cameraRotationSpeed = 2f;
        public float arrowHitMissDistance = 250f;

        public event Action OnLockActions = delegate { };
        public event Action OnLockMovement = delegate { };
        public event Action OnUnlockMovement = delegate { };
        public event Action OnUnlockActions = delegate { };

        private bool _canAction;
        public bool canAction { get { return _canAction && !isDead; } }

        private bool _canMove;
        public bool canMove { get { return _canMove && !isDead; } }

        public Rigidbody swordPrefab;
        public Rigidbody bowPrefab;
        public Rigidbody arrowPrefab;
        public Collider swordCollider;
        [HideInInspector] public Rigidbody currWeapon;
        [HideInInspector] public Rigidbody currArrow;
        private Transform swordHoldSpot;
        private Transform bowHoldSpot;
        private Transform arrowHoldSpot;
        [HideInInspector] public Transform followTarget;
        public GameObject followCamera;
        public GameObject aimCamera;
        private PlayerStatus pStatus;

        [HideInInspector] public GameObject chest;
        [HideInInspector] public GameObject interact_item;

        private TargetingSystem targetingSys;

        [HideInInspector] public bool isDead;
        public bool isFall { get { return IsActive("Fall"); } }
        public bool isIdle { get { return IsActive("Idle"); } }
        public bool isMoving { get { return IsActive("Move"); } }
        public bool isRolling { get { return IsActive("DiveRoll"); } }
        public bool isAiming { get { return IsActive("Aim"); } }
        public bool onGround
        {
            get
            {
                if (IsHandlerReady("OnGround"))
                {
                    return IsActive("OnGround");
                }
                return true;
            }
        }
        public bool onLava { get { return _groundType == "Lava"; } }

        private string _groundType = "normal";

        private Vector3 _moveInput;
        public Vector3 moveInput { get => _moveInput; }

        private Vector3 _jumpInput;
        public Vector3 jumpInput { get => _jumpInput; }

        private Vector3 _velocity;
        public Vector3 xzVelocity { get => _velocity; }

        private Vector3 _cameraRotation;
        public Vector3 cameraRotation { get => _cameraRotation; }

        private int _weaponNbr;
        public int weaponNbr { get => _weaponNbr; }

        public Transform aimTarget;

        public CapsuleCollider leftHandCollider;
        public CapsuleCollider rightHandCollider;

        [HideInInspector]
        public VisualEffect burnEffect;
        [HideInInspector]
        public GlobalControl globalControl;


        GameObject audioEventManager_obj;
        AudioEventManager audioEventManager;

        // Unity related functions
        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            pStatus = GetComponent<PlayerStatus>();
            targetingSys = GetComponentInChildren<TargetingSystem>();

            animator.updateMode = AnimatorUpdateMode.AnimatePhysics;
            animator.cullingMode = AnimatorCullingMode.CullUpdateTransforms;
            animator.gameObject.AddComponent<PlayerAnimationEvents>();
            animator.gameObject.AddComponent<PlayerAudioScript>();


            SetHandler("Attack", new Actions.Attack());
            SetHandler("ShootAim", new Actions.ShootAim());
            SetHandler("ShootRelease", new Actions.ShootRelease());
            SetHandler("Death", new SimpleActionHandler(Death, Revive));
            SetHandler("Null", new Actions.Null());
            SetHandler("Aim", new SimpleActionHandler(StartAiming, EndAiming));

            Unlock(true, true);
            isDead = false;

            leftHandCollider.enabled = false;
            rightHandCollider.enabled = false;

            burnEffect = GetComponentInChildren<VisualEffect>();
            burnEffect.enabled = false;

            _weaponNbr = 0;
            swordHoldSpot = this.transform.Find("RPG-Character/Motion/B_Pelvis/B_Spine/B_Spine1/B_Spine2/B_R_Clavicle/B_R_UpperArm/B_R_Forearm/B_R_Hand/SwordHoldSpot");
            if (swordHoldSpot == null)
            {
                Debug.LogError("Cannot find sword hold spot");
            }
            bowHoldSpot = this.transform.Find("RPG-Character/Motion/B_Pelvis/B_Spine/B_Spine1/B_Spine2/B_L_Clavicle/B_L_UpperArm/B_L_Forearm/B_L_Hand/BowHoldSpot");
            if (bowHoldSpot == null)
            {
                Debug.LogError("Cannot find bow hold spot");
            }
            arrowHoldSpot = this.transform.Find("RPG-Character/Motion/B_Pelvis/B_Spine/B_Spine1/B_Spine2/B_L_Clavicle/B_L_UpperArm/B_L_Forearm/B_L_Hand/ArrowHoldSpot");
            if (arrowHoldSpot == null)
            {
                Debug.LogError("Cannot find arrow hold spot");
            }
            followTarget = this.transform.Find("FollowTarget");
            if (followTarget == null)
            {
                Debug.LogError("Cannot find follow target for camera");
            }

            followCamera.SetActive(true);
            aimCamera.SetActive(false);

            SetHandler("PickUpItem", new Actions.PickUp());
            SetHandler("Interact", new Actions.Interact());

            globalControl = GameObject.Find("GlobalObject").GetComponent<GlobalControl>();

            audioEventManager_obj = GameObject.Find("AudioEventManager");
            audioEventManager = audioEventManager_obj.GetComponent<AudioEventManager>();
        }

        void Start()
        {
            if (globalControl.checkpointScene != null)
            {
                if (SceneManager.GetActiveScene().name ==
                    globalControl.checkpointScene)
                {
                    this.transform.position = globalControl.globalCheckpoint;
                }
            }
        }

        private void Update()
        {
            if (onLava)
            {
                burnEffect.enabled = true;
            }

            if (isDead || aimTarget == null ||
                !aimTarget.gameObject.activeInHierarchy)
            {
                aimTarget = null;
                EndAction("Aim");
            }
            else
            {
                var enemyCtrller = aimTarget.GetComponent<
                    EnemyAI.WalkableEnemyController>();
                if (enemyCtrller != null)
                {
                    if (enemyCtrller.isDead)
                    {
                        aimTarget = null;
                        EndAction("Aim");
                    }
                }

            }

            if (isDead && Input.GetKeyDown(KeyCode.R))
            {
                animator.SetBool("isDead", false);
                animator.SetTrigger("isRevived");
                isDead = false;
                pStatus.RestoreHP();
                if (CanStartAction("Idle"))
                {
                    StartAction("Idle");
                    _weaponNbr = 0;
                }
            }
            if (_canAction && _canMove)
            {
                animator.SetInteger("HoldWeapon", weaponNbr);
                DoWeaponSwitch();
            }
        }

        private void LateUpdate()
        {
            // RotateFollowTarget();
            animator.speed = animationSpeed;
        }

        // Action handler related
        public void SetHandler(string actionName, PlayerActionHandler hdl)
        {
            actionHandlers[actionName] = hdl;
        }

        public bool IsHandlerReady(string actionName)
        {
            return actionHandlers.ContainsKey(actionName);
        }

        public PlayerActionHandler GetHandler(string actionName)
        {
            if (IsHandlerReady(actionName))
            {
                return actionHandlers[actionName];
            }
            return actionHandlers["Null"];
        }

        public bool IsActive(string actionName)
        {
            return GetHandler(actionName).IsActive();
        }

        public bool CanStartAction(string actionName)
        {
            return GetHandler(actionName).CanStartAction(this);
        }

        public void StartAction(string actionName, object ctx = null)
        {
            GetHandler(actionName).StartAction(this, ctx);
        }

        public bool CanEndAction(string actionName)
        {
            return GetHandler(actionName).CanEndAction(this);
        }

        public void EndAction(string actionName)
        {
            GetHandler(actionName).EndAction(this);
        }

        public void SetMoveInput(Vector3 moveInput)
        {
            this._moveInput = moveInput;

            Vector3 forwardDirection = Camera.main.transform.
                TransformDirection(Vector3.forward);
            forwardDirection.y = 0f;
            Vector3 rightDirection = new Vector3(
                forwardDirection.z, 0f, -forwardDirection.x);

            _velocity = _moveInput.z * forwardDirection +
                _moveInput.x * rightDirection;
            /* Vector3 forwardDirection = transform.forward;
            Vector3 rightDirection = transform.right;
            _velocity = _moveInput.z * forwardDirection +
                _moveInput.x * rightDirection;

            if (_velocity.magnitude > 1)
            {
                _velocity.Normalize();
            } */
        }

        public void SetCameraRotation(Vector3 rotation)
        {
            _cameraRotation = rotation;
        }

        public void SetJumpInput(Vector3 jumpInput)
        {
            this._jumpInput = jumpInput;
        }

        public void SetAimTarget(Transform _target)
        {
            aimTarget = _target;
        }

        public void SetGroundType(string _type)
        {
            _groundType = _type;
        }

        public void SetWeaponNbr(int weaponNbr)
        {
            _weaponNbr = weaponNbr;
        }

        // Actual actions
        public void DiveRoll(int ctx)
        {
            animator.SetInteger("Action", ctx);
            SetAnimatorTrigger(AnimatorTrigger.DiveRollTrigger);
            // burnEffect.enabled = false;
            Lock(true, true, true, 0f, 0.4f); // need to be adjusted
        }

        public void Attack(int attackNbr, float duration)
        {
            animator.SetInteger("Action", attackNbr);
            SetAnimatorTrigger(AnimatorTrigger.AttackTrigger);
            if (weaponNbr == 2)
            {
                if (currArrow == null)
                {
                    currArrow = Instantiate(
                        arrowPrefab,
                        arrowHoldSpot.transform.position,
                        arrowHoldSpot.transform.rotation);
                    currArrow.isKinematic = true;
                    currArrow.gameObject.transform.parent = arrowHoldSpot;
                    currArrow.transform.localPosition = Vector3.zero;
                    currArrow.gameObject.SetActive(true);
                }
            }
            Lock(true, true, true, 0, duration);
        }

        public void MoveAttack(int attackNbr, float duration)
        {
            animator.SetInteger("Action", attackNbr);
            SetAnimatorTrigger(AnimatorTrigger.AttackTrigger);
            Lock(true, true, true, 0, duration);
        }

        public void ShootAim()
        {
            if (currArrow == null)
            {
                currArrow = Instantiate(
                    arrowPrefab,
                    arrowHoldSpot.transform.position,
                    arrowHoldSpot.transform.rotation);
                currArrow.isKinematic = true;
                currArrow.gameObject.transform.parent = arrowHoldSpot;
                currArrow.transform.localPosition = Vector3.zero;
                currArrow.gameObject.SetActive(true);
            }
            animator.SetBool("ShootAim", true);
            animator.SetInteger("AimStep", 1);
            _canAction = false;
            _canMove = false;

            aimCamera.SetActive(true);
            followCamera.SetActive(false);

            // set the aim direction
        }

        public void ShootRelease()
        {
            // Set the arrow direction based on camera
            RaycastHit hit;
            // LayerMask lmask = 1 << 11;
            if (Physics.Raycast(
                aimCamera.transform.position,
                aimCamera.transform.forward,
                out hit,
                Mathf.Infinity,
                ~0,
                QueryTriggerInteraction.Ignore))
            {
                ArrowProjectile arr = currArrow.gameObject.GetComponent<ArrowProjectile>();
                arr.SetDest(hit.point);
            }
            else
            {
                ArrowProjectile arr = currArrow.gameObject.GetComponent<ArrowProjectile>();
                arr.SetDest(
                    aimCamera.transform.position +
                    aimCamera.transform.forward * arrowHitMissDistance);
            }


            animator.SetBool("ShootAim", true);
            animator.SetInteger("AimStep", 2);
            _canAction = true;
            _canMove = true;

            aimCamera.SetActive(false);
            followCamera.SetActive(true);
            // Lock(true, true, true, 0.1f, 0.8f);
            // animator.SetBool("ShootAim", false);
        }

        public void Death()
        {
            SetAnimatorTrigger(AnimatorTrigger.DeathTrigger);
            Lock(true, true, false, 0.1f, 0f);
        }

        public void Revive()
        {
            SetAnimatorTrigger(AnimatorTrigger.ReviveTrigger);
            Lock(true, true, true, 0f, 1f);
        }

        public void StartAiming()
        {
            targetingSys.DoTargeting();
            if (aimTarget == null)
            {
                EndAction("Aim");
            }
            else
            {
                aimTarget.gameObject.GetComponentInChildren<EnemyAI.TargetSignController>().TurnSignOn(gameObject);
            }
        }

        public void EndAiming()
        {
            if (aimTarget != null)
            {
                aimTarget.gameObject.GetComponentInChildren<EnemyAI.TargetSignController>().TurnSignOff();
            }
            aimTarget = null;
            if (CanStartAction("Idle"))
            {
                StartAction("Idle");
            }
        }

        public void DoWeaponSwitch()
        {
            if (weaponNbr == 0)
            {
                DestroyWeapon();
                DestroyArrow();
            }
            else if (weaponNbr == 1 && pStatus.hasSword)
            {
                if (currWeapon == null ||
                    !currWeapon.gameObject.CompareTag("Sword"))
                {
                    DestroyWeapon();
                    DestroyArrow();
                    currWeapon = Instantiate(
                        swordPrefab,
                        swordHoldSpot.transform.position,
                        swordHoldSpot.transform.rotation);
                    currWeapon.gameObject.transform.parent = swordHoldSpot;
                    currWeapon.isKinematic = true;
                    currWeapon.transform.localPosition = Vector3.zero;
                    currWeapon.gameObject.SetActive(true);
                }
            }
            else if (weaponNbr == 2 && pStatus.hasBow)
            {
                if (currWeapon == null ||
                    !currWeapon.gameObject.CompareTag("Bow"))
                {
                    DestroyWeapon();
                    DestroyArrow();

                    currWeapon = Instantiate(
                        bowPrefab,
                        bowHoldSpot.transform.position,
                        bowHoldSpot.transform.rotation);
                    currWeapon.gameObject.transform.parent = bowHoldSpot;
                    currWeapon.isKinematic = true;
                    currWeapon.transform.localPosition = Vector3.zero;
                    currWeapon.gameObject.SetActive(true);
                }
            }
            else
            {

            }
        }

        private void DestroyWeapon()
        {
            if (currWeapon != null)
            {
                currWeapon.gameObject.transform.parent = null;
                Destroy(currWeapon.gameObject);
                currWeapon = null;
            }
        }

        private void DestroyArrow()
        {
            if (currArrow != null)
            {
                currArrow.gameObject.transform.parent = null;
                Destroy(currArrow.gameObject);
                currArrow = null;
            }
        }

        public void ReleaseArrow()
        {
            // Debug.Log("Here");
            if (currArrow == null)
                return;

            if (currArrow != null)
            {
                Vector3 forceDir = currArrow.gameObject.transform.
                    TransformDirection(Vector3.right);
                float forceAdded = 100f;
                currArrow.gameObject.GetComponent<ArrowProjectile>().
                    SetParameters(forceDir, forceAdded);
            }
            currArrow.gameObject.transform.parent = null;
            currArrow.gameObject.GetComponent<ArrowProjectile>().released =
                true;
            currArrow = null;
        }

        public void RotateFollowTarget()
        {
            followTarget.transform.rotation *= Quaternion.AngleAxis(
                _cameraRotation.x, Vector3.up);
            followTarget.transform.rotation *= Quaternion.AngleAxis(
                -_cameraRotation.y, Vector3.right);

            var angles = followTarget.transform.localEulerAngles;
            angles.z = 0;
            var angle = followTarget.transform.localEulerAngles.x;

            if (angle > 180 && angle < 340)
            {
                angles.x = 340;
            }
            else if (angle < 180 && angle > 40)
            {
                angles.x = 40;
            }

            followTarget.transform.localEulerAngles = angles;
        }

        public void OnTriggerEnterHitbox(Collider other)
        {
            if (other.gameObject.CompareTag("Sword") ||
                other.gameObject.CompareTag("Arrow") /* ||
                // other.gameObject.CompareTag("Projectile")*/)
            {
                pStatus.currHP -= 1;
            }

            if (pStatus.currHP <= 0)
            {
                animator.SetBool("isDead", true);
                isDead = true;
            }
        }

        public void SetAnimatorTrigger(AnimatorTrigger trigger)
        {
            animator.SetInteger("TriggerNumber", (int)trigger);
            animator.SetTrigger("Trigger");
        }

        // Action Lock related
        public void Lock(
            bool lockMove, bool lockAct, bool timed,
            float delay, float lockDuration)
        {
            StopCoroutine("_Lock");
            StartCoroutine(
                _Lock(lockMove, lockAct, timed, delay, lockDuration));
        }

        private IEnumerator _Lock(
            bool lockMove, bool lockAct, bool timed,
            float delay, float lockDuration)
        {
            if (delay > 0f)
            {
                yield return new WaitForSeconds(delay);
            }
            if (lockMove)
            {
                _canMove = false;
                OnLockMovement();
            }
            if (lockAct)
            {
                _canAction = false;
                OnLockActions();
            }
            if (timed)
            {
                yield return new WaitForSeconds(lockDuration);
                Unlock(true, true);
            }
        }

        public void Unlock(bool lockMove, bool lockAct)
        {
            if (lockMove)
            {
                _canMove = true;
                OnUnlockMovement();
            }
            if (lockAct)
            {
                _canAction = true;
                OnUnlockActions();
            }
        }

        public void Interact()
        {
            Debug.Log("Trying to Interact");
            if (interact_item != null)
            {
                Debug.Log(interact_item.name);
                Interaction interaction = new Interaction();
                interaction.Interact(interact_item);
                Destroy(interaction);
            }
        }

        public void PickUp()
        {
            Debug.Log("Trying to Pick Up Item");
            if (chest != null)
            {
                Debug.Log(chest.name);
                PickingUp pickingup = new PickingUp();
                pickingup.PickUp(chest);
            }

        }
    }
}
