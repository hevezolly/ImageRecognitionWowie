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
    [SerializeField]
    private AudioSource audio;
    [SerializeField]
    private float playFrequency;
    [SerializeField]
    private Vector2 minMaxFreq;

    public UnityEvent DialogueFinishedEvent;

    private Coroutine currentDialogue;

    private WaitForSeconds waitSound;

    private void Awake()
    {
        waitSound = new WaitForSeconds(playFrequency);
    }

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
            var lower = w.word.ToLower();
            text.text += " " + w.word;
            var count = 0;
            for (int i = 0; i < w.word.Length; i++)
            {
                if (lower[i] == 'a' || lower[i] == 'e' || lower[i] == 'i' || lower[i] == 'o' || lower[i] == 'u')
                {
                    count++;
                }
            }

            for (var i = 0; i < count; i++)
            {
                audio.pitch = Random.Range(minMaxFreq.x, minMaxFreq.y);
                audio.Play();
                yield return waitSound;
            }

            yield return new WaitForSeconds(Mathf.Max(w.delay - playFrequency * count, 0));
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


