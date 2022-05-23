using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PlayerControl;

public class SignScript : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject dialogues;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private Canvas dialogCanvas;
    [SerializeField] private GameObject hiddenTextBox;
    [SerializeField] private GameObject hiddenTextBoxcont;

    [SerializeField] private ParticleSystem ParticleSystem;
    private bool activeDialogue = false;

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
            ShowDialog(dialogues);
            ParticleSystem.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);

        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player") && !activeDialogue)
        {
            hiddenTextBox.SetActive(true);
            PlayerController p = c.GetComponent<PlayerController>();
            p.interact_item = gameObject;
            Debug.Log("Setting Item");
        }

    }

    void OnTriggerExit(Collider c)
    {
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

        foreach (string dialogue in dialogues.Dialogue)
        {
            hiddenTextBox.SetActive(false);
            yield return dialogCanvas.GetComponent<TypewriterEffect>().Run(dialogue, textLabel);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        }

        dialogueBox.SetActive(false);
        hiddenTextBoxcont.SetActive(false);
        textLabel.text = string.Empty;
        activeDialogue = false;
    }
    public void StopDialogue()
    {
        if (co != null)
        {
            StopCoroutine(co);
            stopDialogEventCompleted = true;
            dialogueBox.SetActive(false);
            hiddenTextBoxcont.SetActive(false);
            textLabel.text = string.Empty;
            activeDialogue = false;
        }
        stopDialogEventCompleted = true;
    }
}
