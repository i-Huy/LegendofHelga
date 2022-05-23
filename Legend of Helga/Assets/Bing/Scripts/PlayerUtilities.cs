using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerControl
{
    public class PlayerUtilities
    {
        public static bool GroundNearChecker(
            Vector3 pos,
            float jumpableMaxNormalAngle,
            float rayOffset,
            float rayLen,
            out bool jumpable)
        {
            bool ret = false;
            bool _jumpable = false;

            float fullRayLen = rayOffset + rayLen;
            Ray ray = new Ray(pos + Vector3.up * rayOffset, Vector3.down);
            // int layerMask = 1 << LayerMask.NameToLayer("Default");

            RaycastHit[] hits = Physics.RaycastAll(ray, fullRayLen);
            RaycastHit groundHit = new RaycastHit();

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject.CompareTag("Ground"))
                {
                    ret = true;
                    groundHit = hit;
                    _jumpable = Mathf.Abs(Vector3.Angle(
                        Vector3.up, hit.normal)) < jumpableMaxNormalAngle;
                    break;
                }
            }

            DrawRay(ray, fullRayLen, hits.Length > 0, groundHit,
                Color.magenta, Color.green);
            jumpable = _jumpable;

            return ret;
        }

        public static void DrawRay(
            Ray ray, float rayLength, bool hitFound, RaycastHit hit,
            Color rayColor, Color hitColor)
        {

            Debug.DrawLine(
                ray.origin, ray.origin + ray.direction * rayLength, rayColor);

            if (hitFound)
            {
                //draw an X that denotes where ray hit
                const float ZBufFix = 0.01f;
                const float edgeSize = 0.2f;
                Color col = hitColor;

                Debug.DrawRay(
                    hit.point + Vector3.up * ZBufFix,
                    Vector3.forward * edgeSize, col);
                Debug.DrawRay(
                    hit.point + Vector3.up * ZBufFix,
                    Vector3.left * edgeSize, col);
                Debug.DrawRay(
                    hit.point + Vector3.up * ZBufFix,
                    Vector3.right * edgeSize, col);
                Debug.DrawRay(
                    hit.point + Vector3.up * ZBufFix,
                    Vector3.back * edgeSize, col);
            }
        }

    }

}
