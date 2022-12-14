using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PromptDisplay : MonoBehaviour
{
    [SerializeField]
    private ScriptableValue<string> prompt;
    [SerializeField]
    private TextMeshProUGUI textDisplay;
    [SerializeField]
    private AudioSource sound;

    private void OnEnable()
    {
        prompt.ValueChangeEvent.AddListener(SetText);
        SetText(prompt.Value);
    }

    private void OnDisable()
    {
        prompt.ValueChangeEvent.RemoveListener(SetText);
    }

    private void SetText(string text)
    {
        if (text == textDisplay.text)
            return;
        if (sound != null)
            sound.Play();
        textDisplay.text = text;
    }
}
