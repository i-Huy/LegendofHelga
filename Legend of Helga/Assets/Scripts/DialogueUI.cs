using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;
    private CanvasGroup canvasGroup;

    void Awake() {
      canvasGroup = GetComponent<CanvasGroup>();
      // Debug.LogError();
    }

    private void Start() {
        // canvasGroup.interactable = false;
        // canvasGroup.blocksRaycasts = false;
        // canvasGroup.alpha = 0f;
        //GetComponent<TypewriterEffect>().Run("Hello, welcome to Insert Game/Island Name.", textLabel);
    }

    void Update() {

    }
}
