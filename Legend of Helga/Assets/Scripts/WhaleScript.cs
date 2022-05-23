using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject waypoint1;
    public GameObject waypoint2;
    public float speed = 10;
    public Animator anim;

    public enum WhaleState
    {
        Closingin,
        Goingaway,
        Stop
    }

    public WhaleState whaleState;

    void Start()
    {
        transform.position = waypoint1.transform.position;
        anim = GetComponent<Animator>();
        whaleState = WhaleState.Stop;
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Swimming"))
        {
            if(Vector3.Distance(waypoint2.transform.position, transform.position) < 1f)
            {
                anim.SetTrigger("Stop1");
            }
            transform.position = Vector3.MoveTowards(transform.position, waypoint2.transform.position, step);
        }

        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Swimming Back"))
        {
            if (Vector3.Distance(waypoint1.transform.position, transform.position) < 1f)
            {
                anim.SetTrigger("Stop2");
            }
            transform.position = Vector3.MoveTowards(transform.position, waypoint1.transform.position, step);
        }
    }
    
}
