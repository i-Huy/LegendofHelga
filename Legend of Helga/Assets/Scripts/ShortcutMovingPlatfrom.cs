using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerControl;

public class ShortcutMovingPlatfrom : MonoBehaviour
{
    public Vector3[] points;
    public int point_number = 0;

    private Vector3 current_target;

    public float tolerance;
    public float speed;

    public float delay_time;

    private float delay_start;

    public bool automatic;

    Vector3 moveVectorThisFrame;

    private Vector3 scale;

    private GameObject player;
    private CharacterController characterController;


    private bool onboard = false;

    private bool hasMove = false;

    public ParticleSystem particleSystem;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        player = GameObject.FindWithTag("Player");
        
        
        if(points.Length > 0){
            current_target = points[0];
        }
        tolerance = speed * Time.deltaTime;

    }

    // Update is called once per frame
    void Update()
    {

        if ((onboard) && (transform.position != current_target)){
            MovePlatform();
            particleSystem.Stop(false);
        }
        else if (transform.position == current_target){
            UpdateTarget();
        }

        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (hasMove && (dist > 5.0f) && (transform.position != current_target)){
            this.transform.position = points[0];
            BackToStart();
            particleSystem.Play(false);
        }


        if(Input.GetKeyDown(KeyCode.Space) && onboard)
        {
            //Debug.Log("Jump on platform");
            player.transform.parent = null;
        }

    }

    void MovePlatform(){
        hasMove = true;
        Vector3 heading = current_target - transform.position;
        transform.position += (heading / heading.magnitude) * speed * Time.deltaTime;

        if (heading.magnitude < tolerance){
            transform.position = current_target;
            delay_start = Time.time;
        }
    }

    void UpdateTarget(){
        if (automatic){
            if(Time.time - delay_start > delay_time){
                NextPlatform();
            }
        }
    }

    public void NextPlatform(){
        point_number++;
        if (point_number >= points.Length)
        {
            point_number = 0;
        }
        current_target = points[point_number];
    }

    public void BackToStart(){
        point_number = 0;
        current_target = points[point_number];
        hasMove = false;
    }

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject == player)
        {
            onboard = true;
            other.gameObject.transform.parent = transform;
        }
        
    }
    private void OnTriggerExit(Collider other) {
        //Debug.Log("Exit platform");
        if (other.gameObject == player){
            other.gameObject.transform.parent = null;
            onboard = false;
        }
        
    }
}
