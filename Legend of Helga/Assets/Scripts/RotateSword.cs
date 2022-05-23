using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerControl;

public class RotateSword : MonoBehaviour
{
    public float rotation_speed = 30;
    public Vector3 pivot;

    // Update is called once per frame
    void Update()
    {
        // Rotate the game object that this script is attached to by 15 in the X axis,
        // 30 in the Y axis and 45 in the Z axis, multiplied by deltaTime in order to make it per second
        // rather than per frame.
        if (rotation_speed > 0)
        {
            transform.RotateAround(pivot, Vector3.up, rotation_speed * Time.deltaTime);
        }
    }
}
