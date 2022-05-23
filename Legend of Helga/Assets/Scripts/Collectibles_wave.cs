using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles_wave : MonoBehaviour
{
    // Start is called before the first frame update
    void Update () 
	{
		// Rotate the game object that this script is attached to by 15 in the X axis,
		// 30 in the Y axis and 45 in the Z axis, multiplied by deltaTime in order to make it per second
		// rather than per frame.
		transform.Rotate (new Vector3 (0, 30, 0) * Time.deltaTime);
	}
}
