using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using PlayerControl;

namespace EnemyAI
{
    public enum EnemyAIState
    {
        WonderAround,
        Chase,
        Strafe,
        Attack,
        Dead,
    }

    public class WalkableEnemyController : MonoBehaviour
    {
        public GameObject[] waypoints;
        [SerializeField] float wonderAnimSpeedMultiplier;
        [SerializeField] float chaseAnimSpeedMultiplier;
        [SerializeField] float strafeAnimSpeedMultiplier;
        [SerializeField] float moveSpeedMultiplier;
        public float strafeThreshold = 4.5f;
        public float wonderStoppingDist = 2.0f;
        public float chaseStoppingDist = 3.5f;
        public float attackCloseDist = 3.2f;
        private float turnSpeed = 1.0f;
        public float deathDuration = 2.0f;
        public float wonderWaitThreshold = 2.0f;
        private Transform weaponHoldSpot;

        private NavMeshAgent agent;
        private Animator animator;
        private Rigidbody rb;
        private float turnAmount;
        private float forwardAmount;
        private int index;
        private int nbrOfWaypoints;
        private GameObject currTarget;
        private float sightAngle = 75f;
        public float cancelWarningTime = 5;
        private float cancelWarningTimer;
        private bool startCancelWarningTimer;
        private float enemyDistance;
        public float attackInitiateTime = 1.0f;
        private float attackInitiateTimer;
        private float attackRandomTimeInterval;
        private bool startAttackTimer;
        private bool attacked;
        private float strafeSampleInterval;
        private float attackAnimDuration = 1f;
        [HideInInspector] public bool isDead = false;
        private float deathDurationTimer;
        private float wonderWaitTimer;
        public Collider swordHitBox;

        private EnemyAIState enemyState;

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponentInChildren<Animator>();
            if (animator == null)
            {
                Debug.LogError("Cannot find animator");
            }
            animator.gameObject.AddComponent<EnemyAnimationEvents>();

            weaponHoldSpot = this.transform.Find("RPG-Character/Motion/B_Pelvis/B_Spine/B_Spine1/B_Spine2/B_R_Clavicle/B_R_UpperArm/B_R_Forearm/B_R_Hand/SwordHoldSpot");

            agent = GetComponent<NavMeshAgent>();
            rb = GetComponent<Rigidbody>();

            index = 0;
            nbrOfWaypoints = waypoints.Length;
            currTarget = null;
            startCancelWarningTimer = false;
            cancelWarningTimer = 0f;
            attackInitiateTimer = 0f;
            attackRandomTimeInterval = attackInitiateTime;
            attacked = false;
            strafeSampleInterval = 0f;
            deathDurationTimer = 0f;
            wonderWaitTimer = 0f;
            swordHitBox.enabled = false;

            agent.SetDestination(waypoints[index].transform.position);
        }

        // Update is called once per frame
        void Update()
        {
            if (isDead)
            {
                enemyState = EnemyAIState.Dead;
                if (deathDurationTimer == 0f)
                {
                    // Debug.Log("Here");
                    animator.SetBool("isDead", true);
                    agent.enabled = false;
                }
                else
                {
                    animator.SetBool("isDead", false);
                }
                if (deathDurationTimer > deathDuration)
                    Destroy(this.gameObject);
                deathDurationTimer += Time.deltaTime;
            }
            if (startCancelWarningTimer)
            {
                cancelWarningTimer += Time.deltaTime;
            }
            if (cancelWarningTimer >= cancelWarningTime)
            {
                currTarget = null;
                cancelWarningTimer = 0f;
                startCancelWarningTimer = false;
            }
            if (currTarget != null)
            {
                enemyDistance = (currTarget.transform.position -
                    transform.position).magnitude;
            }
            // Debug.Log(enemyDistance);

            // go through states
            if (enemyState == EnemyAIState.WonderAround)
            {
                if (currTarget != null)
                {
                    if (enemyDistance > strafeThreshold)
                    {
                        enemyState = EnemyAIState.Chase;
                        animator.SetBool("isChase", true);
                        animator.SetBool("isWalking", false);
                        animator.applyRootMotion = false;
                    }
                    else
                    {
                        enemyState = EnemyAIState.Strafe;
                        animator.SetBool("isStrafe", true);
                        animator.SetBool("isWalking", false);
                        animator.applyRootMotion = false;
                    }
                }
            }
            else if (enemyState == EnemyAIState.Chase)
            {
                if (currTarget == null)
                {
                    enemyState = EnemyAIState.WonderAround;
                    wonderWaitTimer = 0f;
                    animator.SetBool("isWalking", true);
                    animator.SetBool("isChase", false);
                    animator.applyRootMotion = false;
                }
                else if (enemyDistance < strafeThreshold)
                {
                    enemyState = EnemyAIState.Strafe;
                    animator.SetBool("isStrafe", true);
                    animator.SetBool("isChase", false);
                    animator.applyRootMotion = false;
                }
            }
            else if (enemyState == EnemyAIState.Strafe)
            {
                if (currTarget == null)
                {
                    enemyState = EnemyAIState.WonderAround;
                    wonderWaitTimer = 0f;
                    animator.SetBool("isWalking", true);
                    animator.SetBool("isStrafe", false);
                    animator.applyRootMotion = false;
                }
                else if (enemyDistance > strafeThreshold)
                {
                    enemyState = EnemyAIState.Chase;
                    animator.SetBool("isChase", true);
                    animator.SetBool("isStrafe", false);
                    animator.applyRootMotion = false;
                }
            }

            if (enemyState == EnemyAIState.WonderAround)
            {
                weaponHoldSpot.gameObject.SetActive(false);
                agent.stoppingDistance = wonderStoppingDist;
                if (agent.destination != waypoints[index].transform.position)
                {
                    agent.SetDestination(waypoints[index].transform.position);
                }
                if (agent.remainingDistance < agent.stoppingDistance &&
                    !agent.pathPending)
                {
                    if (wonderWaitTimer <= wonderWaitThreshold)
                    {
                        wonderWaitTimer += Time.deltaTime;
                    }
                    else
                    {
                        wonderWaitTimer = 0f;
                        index += 1;
                        if (index >= nbrOfWaypoints)
                            index = 0;
                        agent.SetDestination(
                            waypoints[index].transform.position);
                    }
                }
                UpdateAnimatorWonderAround(agent.desiredVelocity);
            }
            else if (enemyState == EnemyAIState.Chase)
            {
                weaponHoldSpot.gameObject.SetActive(true);
                agent.stoppingDistance = chaseStoppingDist;
                agent.SetDestination(currTarget.transform.position);
                UpdateAnimatorChase(agent.desiredVelocity);
            }
            else if (enemyState == EnemyAIState.Strafe)
            {
                weaponHoldSpot.gameObject.SetActive(true);
                RotateTowardsTarget();
                if (enemyDistance < attackCloseDist && !attacked)
                {
                    animator.speed = 1f;
                    animator.SetBool("Attacking", true);
                    attacked = true;
                }
                else
                {
                    animator.SetBool("Attacking", false);
                    if (attackInitiateTimer > attackRandomTimeInterval)
                    {
                        attacked = false;
                        attackRandomTimeInterval = attackAnimDuration +
                            Random.Range(3f, attackInitiateTime);
                        attackInitiateTimer = 0f;
                    }
                    else
                    {
                        attackInitiateTimer += Time.deltaTime;
                    }

                    if (strafeSampleInterval > 0.75f)
                    {
                        strafeSampleInterval = 0f;

                        float velx = Random.Range(-1f, 1f);
                        float ymin = (enemyDistance - 4.5f) / 3.0f;
                        float ymax = (enemyDistance - 1.5f) / 3.0f;
                        float vely = Random.Range(ymin, ymax);
                        Vector3 randomDir = new Vector3(velx, 0f, vely);
                        randomDir.Normalize();
                        randomDir = transform.TransformDirection(randomDir);
                        transform.Translate(0.25f * randomDir, Space.World);

                        animator.SetFloat("velocityY", 0.5f);
                        animator.SetFloat("velocityX", 0.5f);
                        animator.speed = strafeAnimSpeedMultiplier;
                    }
                    else
                    {
                        strafeSampleInterval += Time.deltaTime;
                    }
                }
            }
        }

        public void UpdateAnimatorWonderAround(Vector3 velocity)
        {
            if (velocity.magnitude > 1f)
                velocity.Normalize();

            Vector3 dir = transform.InverseTransformDirection(velocity);
            turnAmount = Mathf.Atan2(dir.x, dir.z);
            forwardAmount = dir.z;

            animator.SetFloat("velocityY", forwardAmount, 0.1f, Time.deltaTime);
            animator.SetFloat("velocityX", turnAmount, 0.1f, Time.deltaTime);
            animator.speed = wonderAnimSpeedMultiplier;
        }

        public void UpdateAnimatorChase(Vector3 velocity)
        {
            if (velocity.magnitude > 1f)
                velocity.Normalize();

            Vector3 dir = transform.InverseTransformDirection(velocity);
            turnAmount = Mathf.Atan2(dir.x, dir.z);
            forwardAmount = dir.z;

            animator.SetFloat("velocityY", forwardAmount, 0.1f, Time.deltaTime);
            animator.SetFloat("velocityX", turnAmount, 0.1f, Time.deltaTime);
            animator.speed = chaseAnimSpeedMultiplier;
        }

        public void RotateTowardsTarget()
        {
            if (currTarget == null)
                return;

            Vector3 faceDirection = currTarget.transform.position -
                this.transform.position;
            faceDirection = Vector3.ProjectOnPlane(faceDirection, Vector3.up);
            faceDirection.Normalize();

            if (faceDirection.magnitude > 0.01f)
            {
                Quaternion toRotation = Quaternion.LookRotation(
                    faceDirection);
                Quaternion actualRotation = Quaternion.Slerp(
                    transform.rotation, toRotation,
                    turnSpeed * Time.deltaTime);
                this.transform.rotation = actualRotation;
            }
        }

        public void OnAnimatorMove()
        {
            if (Time.deltaTime > 0.000001f)
            {
                Vector3 v = (animator.deltaPosition * moveSpeedMultiplier) /
                    Time.deltaTime;

                v.y = rb.velocity.y;
                rb.velocity = v;
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player"))
            {
                return;
            }

            PlayerController cllr = other.gameObject.
                GetComponent<PlayerController>();
            if (cllr.isDead)
            {
                currTarget = null;
                return;
            }

            // Debug.Log("Encounter!");
            Vector3 dir = other.gameObject.transform.position -
                this.transform.position;
            Vector3 dist = other.gameObject.transform.position -
                this.transform.position;
            if (dir.magnitude > 1f)
                dir.Normalize();
            if (!InSight(transform.forward, dir, dist.magnitude))
                return;

            if (currTarget != null && currTarget == other.gameObject)
            {
                cancelWarningTimer = 0f;
                startCancelWarningTimer = false;
            }
            else if (currTarget == null)
            {
                currTarget = other.gameObject;
                startCancelWarningTimer = false;
            }
        }

        void OnTriggerStay(Collider other)
        {
            if (!other.gameObject.CompareTag("Player"))
                return;

            PlayerController cllr = other.gameObject.
                GetComponent<PlayerController>();
            if (cllr.isDead)
            {
                currTarget = null;
                return;
            }

            // Debug.Log("Encounter!");
            Vector3 dir = other.gameObject.transform.position -
                this.transform.position;
            Vector3 dist = other.gameObject.transform.position -
                this.transform.position;
            if (dir.magnitude > 1f)
                dir.Normalize();
            if (!InSight(transform.forward, dir, dist.magnitude))
                return;

            if (currTarget != null && currTarget == other.gameObject)
            {
                cancelWarningTimer = 0f;
                startCancelWarningTimer = false;
            }
            else if (currTarget == null)
            {
                currTarget = other.gameObject;
                startCancelWarningTimer = false;
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (currTarget != null && other.gameObject == currTarget)
            {
                cancelWarningTimer += Time.deltaTime;
                startCancelWarningTimer = true;
            }
        }

        private bool InSight(Vector3 faceDir, Vector3 actualDir, float dist)
        {
            float angle = Vector3.Angle(faceDir, actualDir);
            RaycastHit hit;
            int layerMask = 1 << 12;

            if (angle > -sightAngle && angle < sightAngle)
            {
                if (Physics.Raycast(
                    transform.position, actualDir, out hit,
                    dist + 0.05f, layerMask))
                    return false;
                else
                    return true;
            }
            else
                return false;
        }
    }
}
