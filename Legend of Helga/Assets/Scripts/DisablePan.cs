using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisablePan : MonoBehaviour
{
    // Start is called before the first frame update

    public float PanDuration = 0.0f;

    public GameObject player;

    public GameObject Pan;

    public GameObject PanSubtitle;

    private GameObject globalControlObject;
    
    private GlobalControl globalControl;


    private float timeRemaining;

    void Start()
    {
        globalControlObject = GameObject.Find("GlobalObject");
        globalControl = globalControlObject.GetComponent<GlobalControl>();

        PlayerControl.InputController ict =
                player.GetComponent<PlayerControl.InputController>();
        
        ict.enabled = false;
        
        if (panPlayStatus())
        {
            Pan.SetActive(false);

            if (PanSubtitle != null)
            {
                PanSubtitle.SetActive(false);
            }
        
            ict.enabled = true;
        }
        timeRemaining = PanDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (!panPlayStatus())
        {
            panCountDownTime();
        }
    }

    public void panCountDownTime()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            disablePanGlobal();
            PlayerControl.InputController ict =
                player.GetComponent<PlayerControl.InputController>();
        
            ict.enabled = true;
        }
    }

    public void disablePanGlobal()
    {
        if (SceneManager.GetActiveScene().name.Contains("Wind"))
        {
            globalControl.windPanPlayed = true;
        }
        else if (SceneManager.GetActiveScene().name.Contains("Begin"))
        {
            globalControl.beginPanPlayed = true;
        }
        else if (SceneManager.GetActiveScene().name.Contains("Fire"))
        {
            // TODO: expandable
        }
    }

    public bool panPlayStatus()
    {
        if (SceneManager.GetActiveScene().name.Contains("Wind"))
        {
            return globalControl.windPanPlayed;
        }
        else if (SceneManager.GetActiveScene().name.Contains("Begin"))
        {
            return globalControl.beginPanPlayed;
        }
        return true;
    
    }
}
