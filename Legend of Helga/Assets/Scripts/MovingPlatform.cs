using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3[] points;
    public int point_number = 0;

    private Vector3 current_target;

    public float tolerance;
    public float speed;

    public float delay_time;

    private float delay_start;

    public bool automatic;
    // Start is called before the first frame update

    Vector3 moveVectorThisFrame;

    private Vector3 scale;

    private GameObject player;
    private CharacterController characterController;

    private bool onboard = false;


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
        if (transform.position != current_target){
            MovePlatform();
        }
        else{
            UpdateTarget();
        }

        if(Input.GetKeyDown(KeyCode.Space) && onboard)
        {
            //Debug.Log("Jump on platform");
            player.transform.parent = null;
        }

    }

    void MovePlatform(){
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

    private void OnTriggerEnter(Collider other) {
        //Debug.Log("Parent platform" + gameObject.name);
        // var emptyObject = new GameObject();
        // emptyObject.transform.parent = transform;
        // other.transform.parent = emptyObject.transform;
        
        //other.gameObject.transform.parent = transform.parent.transform;
        if (other.gameObject == player)
        {
            onboard = true;
            other.gameObject.transform.parent = transform;
        }
        
        //other.gameObject.transform.parent = transform.parent;
        // scale = other.transform.localScale;
        // Debug.LogError(scale);
        // other.transform.parent = transform;
        // other.transform.localScale = scale;

    }
    private void OnTriggerExit(Collider other) {
        //Debug.Log("Exit platform");
        if (other.gameObject == player){
            other.gameObject.transform.parent = null;
            onboard = false;
        }
        
    }
}
