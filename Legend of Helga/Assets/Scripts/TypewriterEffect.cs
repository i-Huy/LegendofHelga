using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    private float typewriterSpeed = 50f;
    private CanvasGroup canvasGroup;

    void Awake() {
      canvasGroup = GetComponent<CanvasGroup>();
      // Debug.LogError();
    }

    public Coroutine Run(string text, TMP_Text textLabel) {
        // canvasGroup.interactable = true;
        // canvasGroup.blocksRaycasts = true;
        // canvasGroup.alpha = 1f;
        return StartCoroutine(TypeText(text, textLabel));
    }

    private IEnumerator TypeText(string textToType, TMP_Text textLabel) {
        float t = 0;
        int charIndex = 0;
        int wordLength = textToType.Length;

        while(charIndex < textToType.Length) {
            t += Time.deltaTime * typewriterSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);

            textLabel.text = textToType.Substring(0, charIndex);
            yield return null;
        }

        textLabel.text = textToType;
    }
}
