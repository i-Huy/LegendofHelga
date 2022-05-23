using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

public class DialogueCoroutineManager : MonoBehaviour
{
    // Start is called before the first frame update

    public UnityEvent StopDlg = new UnityEvent();

    // Update is called once per frame

    public void StopDialogue() {
        StopDlg.Invoke();
    }
}
