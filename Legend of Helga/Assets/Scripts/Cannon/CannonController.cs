using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject target;
    private UnityEngine.AI.NavMeshAgent cannon;
    private CannonCollision cannonStatus;

    void Awake()
    {
        cannon = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (cannon == null)
            Debug.Log("NavMeshAgent could not be found");

        cannonStatus = GetComponent<CannonCollision>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.transform.position, transform.position);
        Vector3 direction = (target.transform.position - transform.position).normalized;
        GameObject frontWheels = GameObject.FindGameObjectsWithTag("FrontWheels")[0];
        GameObject backWheels = GameObject.FindGameObjectsWithTag("BackWheels")[0];

        if(cannonStatus.currHP <= 0)
        {
            cannon.enabled = false;
        }
        if(distance > 20f && cannonStatus.currHP != 0)
        {
            frontWheels.GetComponent<Animator>().enabled = true;
            backWheels.GetComponent<Animator>().enabled = true;
            cannon.stoppingDistance = 20f;
            cannon.SetDestination(target.transform.position);
        }

        if(cannon.velocity.magnitude <= 0f)
        {
            frontWheels.GetComponent<Animator>().enabled = false;
            backWheels.GetComponent<Animator>().enabled = false;
        }
    }
}
