using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlayerControl
{
    public class TargetingSystem : MonoBehaviour
    {
        public float targetAngle;

        private List<GameObject> candidateObjects;
        private int nbrOfTargetsInRange;
        private GameObject closestTarget;
        private PlayerController controller;

        [HideInInspector] public bool isDown;

        // Start is called before the first frame update
        void Start()
        {
            nbrOfTargetsInRange = 0;
            isDown = false;
            closestTarget = null;
            candidateObjects = new List<GameObject>();
            controller = GetComponentInParent<PlayerController>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            // 11 is the enemy layer
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                candidateObjects.Add(other.gameObject);
                nbrOfTargetsInRange += 1;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                candidateObjects.Remove(other.gameObject);
                nbrOfTargetsInRange -= 1;
            }
        }

        private void Cleanup()
        {
            for (int i = nbrOfTargetsInRange - 1; i >= 0; --i)
            {
                if (candidateObjects[i] == null ||
                    !candidateObjects[i].activeInHierarchy)
                {
                    candidateObjects.RemoveAt(i);
                    nbrOfTargetsInRange -= 1;
                }
            }
        }

        public void DoTargeting()
        {
            Cleanup();

            if (nbrOfTargetsInRange > 0)
            {
                List<GameObject> SortedObjects = candidateObjects.OrderBy(
                    gameObject =>
                    {
                        Vector3 targetDirection =
                            gameObject.transform.position -
                            Camera.main.transform.position;

                        var cameraForward = new Vector2(
                            Camera.main.transform.forward.x,
                            Camera.main.transform.forward.z);
                        var targetDir = new Vector2(
                            targetDirection.x,
                            targetDirection.z);
                        float angle = Vector2.Angle(cameraForward, targetDir);
                        return angle;
                    }).ToList();

                for (int i = 0; i < candidateObjects.Count(); ++i)
                {
                    candidateObjects[i] = SortedObjects[i];
                    if (!candidateObjects[i].activeInHierarchy)
                    {
                        candidateObjects.RemoveAt(i);
                        nbrOfTargetsInRange -= 1;
                    }
                }

                closestTarget = candidateObjects.First();
            }

            if (closestTarget != null)
            {
                Vector3 targetDirection =
                    closestTarget.transform.position -
                    Camera.main.transform.position;

                var cameraForward = new Vector2(
                    Camera.main.transform.forward.x,
                    Camera.main.transform.forward.z);
                var targetDir = new Vector2(
                    targetDirection.x,
                    targetDirection.z);
                float angle = Vector2.Angle(cameraForward, targetDir);

                if (Mathf.Abs(angle) >= targetAngle)
                {
                    closestTarget = null;
                }
            }

            if (closestTarget != null)
                controller.SetAimTarget(closestTarget.transform);
            else
                controller.SetAimTarget(null);
        }
    }
}
