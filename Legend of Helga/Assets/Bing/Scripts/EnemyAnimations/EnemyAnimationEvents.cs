using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using EnemyAI;

public class EnemyAnimationEvents : MonoBehaviour
{
    public UnityEvent OnHit = new UnityEvent();
    public UnityEvent OnFootR = new UnityEvent();
    public UnityEvent OnFootL = new UnityEvent();
    Animator animator;
    WalkableEnemyController controller;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponentInParent<WalkableEnemyController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FootR()
    {
        OnFootR.Invoke();
    }

    public void FootL()
    {
        OnFootL.Invoke();
    }

    public void HitBoxOn()
    {
        controller.swordHitBox.enabled = true;
    }

    public void HitBoxOff()
    {
        controller.swordHitBox.enabled = false;
    }

    public void Hit()
    {
        OnHit.Invoke();
    }
}
