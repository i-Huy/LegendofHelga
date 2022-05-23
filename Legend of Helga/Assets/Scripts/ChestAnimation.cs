using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestAnimation : MonoBehaviour
{
    [SerializeField] public Animator myAnimationController;
    // Start is called before the first frame update
    void Start()
    {
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myAnimationController.SetBool("InRange", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myAnimationController.SetBool("InRange", false);
        }

    }

}
