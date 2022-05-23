using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointUtility : MonoBehaviour
{
    private GameObject globalControlObject;
    private GlobalControl globalControl;

    private GameObject activatedCheckPoint;
    private bool checkPointisNull = false;

    public Vector3 resetPositionOffset = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        globalControlObject = GameObject.Find("GlobalObject");
        globalControl = globalControlObject.GetComponent<GlobalControl>();
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider c)
    {
        activatedCheckPoint = globalControl.currentActivedCheckpoint;
        // The object would be destroyed when load scene(reset)
        if (activatedCheckPoint == null)
            checkPointisNull = true;
        // check if is the same checkpoint saving in global
        if (c.gameObject.name == "PlayerTest")
        {
            if ((!checkPointisNull) && (gameObject != activatedCheckPoint))
            {
                // deactive previous checkpoint crystal's animation
                Animator previousCheckpointAnimator;
                previousCheckpointAnimator = activatedCheckPoint.GetComponent<Animator>();
                if (previousCheckpointAnimator != null)
                {
                    previousCheckpointAnimator.SetBool("isTrigger", false);
                }
            }
            
            // Activate current crystal's animation
            Animator checkpointAnimator;
            checkpointAnimator = GetComponent<Animator>();
            if (checkpointAnimator != null)
            {
                checkpointAnimator.SetBool("isTrigger", true);
            }
            
            // Set global value
            Vector3 ResetPosition = this.transform.position + resetPositionOffset;
            Debug.Log(resetPositionOffset);
            globalControl.SetGlobalCheckpoint(
                ResetPosition,
                SceneManager.GetActiveScene().name,
                gameObject);
            
        }
        
    }
}
