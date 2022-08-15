using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class DialogueDisplay : MonoBehaviour
{
    [SerializeField]
    private GameObject DialogueObj;
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private float waitBeforeTurnOffDialogue;

    public UnityEvent DialogueFinishedEvent;

    private Coroutine currentDialogue;
    public void Display(IEnumerable<DialogueWord> words)
    {
        if (currentDialogue != null)
            StopCoroutine(currentDialogue);
        text.text = "";
        DialogueObj.SetActive(true);
        currentDialogue = StartCoroutine(DisplayCoroutine(words));
    }

    private IEnumerator DisplayCoroutine(IEnumerable<DialogueWord> word)
    {
        foreach (var w in word)
        {
            text.text += " " + w.word;
            yield return new WaitForSeconds(w.delay);
        }
        yield return new WaitForSeconds(waitBeforeTurnOffDialogue);
        TurnOffDialogue();
    }

    private void TurnOffDialogue()
    {
        currentDialogue = null;
        DialogueFinishedEvent?.Invoke();
    }

    public void TurnOff()
    {
        if (currentDialogue != null)
            StopCoroutine(currentDialogue);
        DialogueObj.SetActive(false);
    }
}

public class DialogueWord
{
    public string word;
    public float delay;
}


