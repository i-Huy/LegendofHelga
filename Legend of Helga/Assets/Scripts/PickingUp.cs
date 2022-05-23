using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickingUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickUp(GameObject chest)
    {
        //Debug.Log("Picking?");

        if (chest.name.Contains("Chest"))
        {
            ChestScript chestScript = chest.GetComponent<ChestScript>();
            chestScript.PickUp();
        }
    }
}
