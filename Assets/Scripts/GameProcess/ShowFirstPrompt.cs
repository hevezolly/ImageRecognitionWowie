using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowFirstPrompt : MonoBehaviour
{
    [SerializeField]
    private ScriptableValue<string> prompt;
    [SerializeField]
    private ImagePropertyID imageId;

    public void Trigger()
    {
        prompt.Value = imageId.Value;
    }
}
