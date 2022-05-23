using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionToEndFromCredits : MonoBehaviour
{
    public Animator animator;
    private GameObject sceneChanger_obj;
    private SceneChanger sceneChanger;

    private void Start()
    {
        sceneChanger_obj = GameObject.Find("SceneChanger");
        sceneChanger = sceneChanger_obj.GetComponent<SceneChanger>();
    }

    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.normalizedTime > 1.0f)
        {
            sceneChanger.FadetoScene("EndMenu");
        }
    }
}
