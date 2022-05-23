using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public bool paused = false;
    // Start is called before the first frame update
    void Start()
    {
        paused = false;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p") && paused == false)
        {
            Time.timeScale = 0;
            paused = true;
        }
        // Use else to make sure this block only gets executed if the above doesn't
        else if (Input.GetKeyDown("p") && paused == true)
        {
            Time.timeScale = 1;
            paused = false;
        }
    }

    public void PauseButton()
    {
        if (paused == false)
        {
            Time.timeScale = 0;
            paused = true;
        }
        else if (paused == true)
        {
            Time.timeScale = 1;
            paused = false;
        }
    }
}
