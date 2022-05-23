using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCenterOfMass : MonoBehaviour
{
    public Vector3 _centerOfMass = Vector3.zero;

    public GameObject comMarker = null;
    public Vector3 centerOfMass
    {
        get { return _centerOfMass; }
        set { _centerOfMass = value; }
    }
    public bool continuousUpdate = false;

    protected Rigidbody rb;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        AssignCenterOfMass();
    }

    // Update is called once per frame
    void Update()
    {
        if (continuousUpdate)
        {
            AssignCenterOfMass();
        }
    }

    public void AssignCenterOfMass()
    {
        if (comMarker != null)
        {
            centerOfMass = this.transform.InverseTransformPoint(
                comMarker.transform.position);
        }
        if (rb.centerOfMass != centerOfMass)
        {
            rb.centerOfMass = centerOfMass;
        }
    }
}
