using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PlayerControl;

public class DialogueScript : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject dialogues;
    [SerializeField] private DialogueObject storydialogues;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private Canvas dialogCanvas;
    [SerializeField] private GameObject hiddenTextBox;
    [SerializeField] private GameObject hiddenTextBoxcont;
    private bool activeDialogue = false;
    [SerializeField] public Animator myAnimationController;

    IEnumerator co;
    GameObject dcm_obj;
    DialogueCoroutineManager dcm;
    bool stopDialogEventCompleted;


    void Start()
    {
        dialogueBox.SetActive(false);
        hiddenTextBox.SetActive(false);
        hiddenTextBoxcont.SetActive(false);
        textLabel.text = string.Empty;

        dcm_obj = GameObject.Find("DialogCoroutineManager");
        dcm = dcm_obj.GetComponent<DialogueCoroutineManager>();
        dcm.StopDlg.AddListener(StopDialogue);
        stopDialogEventCompleted = false;
    }
    public void Interact()
    {
        if (activeDialogue == false)
        {
            dialogueBox.SetActive(true);
            hiddenTextBoxcont.SetActive(true);
            activeDialogue = true;
            if (myAnimationController != null)
            {
                myAnimationController.SetTrigger("Swim1");
            }
            ShowDialog(dialogues);
        }

    }

   
    void OnTriggerEnter(Collider c) {
        if(c.CompareTag("Player") && !activeDialogue) {
            hiddenTextBox.SetActive(true);
            PlayerController p = c.GetComponent<PlayerController>();
            p.interact_item = gameObject;
            Debug.Log("Setting Item");
        }
    }

    void OnTriggerExit(Collider c) {
        if (c.CompareTag("Player"))
        {
            PlayerController p = c.GetComponent<PlayerController>();
            p.interact_item = null;
            Debug.Log("Clearing Item");
        }
        hiddenTextBox.SetActive(false);
    }

    public void ShowDialog(DialogueObject dialogueObj)
    {
        co = null;
        dcm.StopDialogue(); 
        co = LoopThroughDialogues(dialogueObj); // need to assign to co
        StartCoroutine(co);
    }


    private IEnumerator LoopThroughDialogues(DialogueObject dialogueObj)
    {
        if (GlobalControl.Instance.storycompleted != true) {
            foreach (string dialogue in storydialogues.Dialogue)
            {
                hiddenTextBox.SetActive(false);
                yield return dialogCanvas.GetComponent<TypewriterEffect>().Run(dialogue, textLabel);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
            }
        }
        GlobalControl.Instance.storycompleted = true;
        foreach (string dialogue in dialogues.Dialogue) {
        hiddenTextBox.SetActive(false);
        yield return dialogCanvas.GetComponent<TypewriterEffect>().Run(dialogue, textLabel);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        }

        dialogueBox.SetActive(false);
        hiddenTextBoxcont.SetActive(false);
        textLabel.text = string.Empty;
        activeDialogue = false;
        myAnimationController.SetTrigger("Swim2");
    }

    public void StopDialogue()
    {
        if (co != null)
        {
            StopCoroutine(co);
            stopDialogEventCompleted = true;
            hiddenTextBox.SetActive(false);
            dialogueBox.SetActive(false);
            hiddenTextBoxcont.SetActive(false);
            textLabel.text = string.Empty;
            activeDialogue = false;
            myAnimationController.SetTrigger("Swim2");
        }
        stopDialogEventCompleted = true;
    }

}
