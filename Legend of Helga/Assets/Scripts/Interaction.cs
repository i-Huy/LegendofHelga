using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Interact(GameObject interact_item)
    {
        if (interact_item.name.Contains("Chest")){
            ChestScript chestScript = interact_item.GetComponent<ChestScript>();
            chestScript.Interact();

        }
        if (interact_item.name.Contains("Sign"))
        {
            Debug.Log("A Sign");
            SignScript signScript = interact_item.GetComponent<SignScript>();
            signScript.Interact();

        }
        if (interact_item.name.Contains("NPC"))
        {
            Debug.Log("An NPC");
            DialogueScript chestScript = interact_item.GetComponent<DialogueScript>();
            chestScript.Interact();

        }
    }
}
