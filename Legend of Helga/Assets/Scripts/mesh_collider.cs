using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mesh_collider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MeshCollider collider = GetComponent<MeshCollider>();
        collider.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
