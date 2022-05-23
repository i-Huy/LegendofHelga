using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPlatform : MonoBehaviour
{
    [SerializeField] string playerTag = "Player";
    [SerializeField] float disappearTime = 2.5f;
    Animator anim;

    [SerializeField] bool canReset;
    [SerializeField] float resetTime;

    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetFloat("DisappearTime", 2.3f / disappearTime);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.transform.tag == playerTag)
        {
            anim.SetBool("Trigger", true);
        }
    }

    public void TriggerReset()
    {
        if (canReset){
            StartCoroutine(Reset());
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(resetTime);
        anim.SetBool("Trigger", false);
    }
}
