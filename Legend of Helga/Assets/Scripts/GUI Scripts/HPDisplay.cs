//// Huy
//// TODO: most of this is placeholder for implementing icons/visual hp indicator
//// trigger death animation + display death screeen when HP reaches 0
//// script is from: https://www.youtube.com/watch?v=_RIsfVOqTaE&t=14s ; modified by me

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class HPDisplay : MonoBehaviour
//{
//    public int HP = 3;
//    public Text HPText;

//    // Start is called before the first frame update
//    void Start()
//    {
//        Debug.Assert(HPText != null, "HP Text is null", gameObject);
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        HPText.text = "HP: " + HP.ToString();

//        // test hp reduction using space key as placeholder
//        if (Input.GetKeyDown(KeyCode.Space) && HP > 0)
//        {
//            HP--;
//        }
//        if (HP <= 0) // had else statement before (and it worked) but stopped working :/
//        {
//            HPText.text = "DEAD :(";
//            // placeholder, trigger death animation + screen
//        }
//    }
//}
