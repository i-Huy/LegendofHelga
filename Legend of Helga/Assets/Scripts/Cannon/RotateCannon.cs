using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCannon : MonoBehaviour
{
    public GameObject target;
    private CannonCollision cannonStatus;
    // Start is called before the first frame update

    void Awake()
    {
        cannonStatus = GameObject.FindGameObjectsWithTag("BossParent")[0].GetComponent<CannonCollision>();
    }

    // Update is called once per frame
    void Update()
    {
        int currHP = cannonStatus.currHP;

        if(currHP != 0)
        {
            this.transform.LookAt(target.transform);
        }
    }
}
