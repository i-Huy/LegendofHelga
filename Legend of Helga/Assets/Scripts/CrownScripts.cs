using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CrownScripts : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;
    
    [SerializeField] private GameObject hiddenTextBox_0;
    [SerializeField] private GameObject hiddenTextBox_1;

    public SceneChanger sceneChanger;

    
    [SerializeField] private bool needBow = false;
    [SerializeField] private bool needSword = false;

    private GameObject globalControlObject;

    private GlobalControl globalControl;

    private bool canTransfer = false;

    void Start()
    {
        //dialogueBox.SetActive(false);
        hiddenTextBox_1.SetActive(false);
        hiddenTextBox_0.SetActive(false);
        textLabel.text = string.Empty;
        globalControlObject = GameObject.Find("GlobalObject");
        globalControl = globalControlObject.GetComponent<GlobalControl>();
    }

    void Update()
    {
        if (needBow && globalControl.hasBow)
            canTransfer = true;
        if (needSword && globalControl.hasSword)
            canTransfer = true;
    }


    void OnTriggerStay(Collider c)
    {
        if (c.attachedRigidbody != null && c.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E) && canTransfer)
            {
                sceneChanger.FadetoScene("BeginningIsland");
            }
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player" && canTransfer)
        {
            hiddenTextBox_1.SetActive(true);
        }

        if (c.gameObject.tag == "Player" && !canTransfer)
        {
            hiddenTextBox_0.SetActive(true);
        }
        
    }

    void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Player" && canTransfer)
            hiddenTextBox_1.SetActive(false);
        if (c.gameObject.tag == "Player" && !canTransfer)
            hiddenTextBox_0.SetActive(false);
    }

    // public void ShowDialog(DialogueObject dialogueObj)
    // {
    //     StartCoroutine(LoopThroughDialogues(dialogueObj));
    // }

    // private IEnumerator LoopThroughDialogues(DialogueObject dialogueObj)
    // {
    //     foreach (string dialogue in dialogues.Dialogue)
    //     {
    //         hiddenTextBox.SetActive(false);
    //         yield return dialogCanvas.GetComponent<TypewriterEffect>().Run(dialogue, textLabel);
    //         yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
    //     }

    //     dialogueBox.SetActive(false);
    //     textLabel.text = string.Empty;
    //     activeDialogue = false;
    // }
}
