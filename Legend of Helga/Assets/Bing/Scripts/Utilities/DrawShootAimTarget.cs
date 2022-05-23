using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerControl
{
    public class DrawShootAimTarget : MonoBehaviour
    {
        private PlayerController controller;
        public float rayLength;
        public float circleSize;

        // Start is called before the first frame update
        void Start()
        {
            controller = GetComponent<PlayerController>();
            if (controller == null)
            {
                Debug.LogError("Cannot find controller in DrawShootAimTarget");
            }
        }

        // Update is called once per frame
        void LateUpdate()
        {
            Vector3 drawDir, drawStart;
            drawStart = controller.aimCamera.transform.position;
            drawDir = controller.aimCamera.transform.forward;
            Vector3 drawEnd;

            if (controller.IsActive("ShootAim"))
            {
                RaycastHit hit;
                // int lmask = 1 << 11;
                if (Physics.Raycast(
                    controller.aimCamera.transform.position,
                    controller.aimCamera.transform.forward,
                    out hit,
                    Mathf.Infinity,
                    ~0,
                    QueryTriggerInteraction.Ignore))
                {
                    Debug.Log("Here hit");
                    drawEnd = hit.point;
                }
                else
                {
                    Debug.Log("Here not hit");
                    drawEnd =
                        controller.aimCamera.transform.position +
                        controller.aimCamera.transform.forward * 250f;
                }

                DrawLine(drawStart, drawEnd, Time.deltaTime / 2);
                DrawLine(
                    drawEnd + circleSize * Vector3.up,
                    drawEnd + circleSize * Vector3.down,
                    Time.deltaTime / 2);

                Vector3 xDir = Vector3.ProjectOnPlane(drawDir, Vector3.up);
                Vector3 zDir = new Vector3(xDir.z, 0f, -xDir.x);
                DrawLine(
                    drawEnd + circleSize * zDir,
                    drawEnd - circleSize * zDir,
                    Time.deltaTime / 2);
            }
        }

        private void DrawLine(Vector3 start, Vector3 end, float duration)
        {
            GameObject lineObject = new GameObject();
            LineRenderer l = lineObject.AddComponent<LineRenderer>();
            List<Vector3> vexs = new List<Vector3>();
            vexs.Add(start);
            vexs.Add(end);
            l.startWidth = 0.01f;
            l.endWidth = 0.01f;
            l.startColor = Color.white;
            l.endColor = Color.white;
            l.SetPositions(vexs.ToArray());
            l.useWorldSpace = true;
            Destroy(lineObject, duration);
        }


    }
}
