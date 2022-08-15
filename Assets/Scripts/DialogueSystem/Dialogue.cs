using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Globalization;

public class Dialogue : MonoBehaviour
{
    private const string regex = "^(.+?)(?:\\[(\\d+(?:\\.\\d+)?)\\])?$";
    private const string promptReplace = "<>";

    [SerializeField]
    private ScriptableValue<string> currentPrompt;

    [SerializeField]
    private DialogueDisplay display;
    [SerializeField]
    private float defaultDelay;

    private Regex pattern;

    private void Awake()
    {
        pattern = new Regex(regex, RegexOptions.Compiled);
    }

    public void ShowLine(string line)
    {
        display.Display(ParseLine(line));
    }
    
    private IEnumerable<DialogueWord> ParseLine(string line)
    {
        foreach (var w in line.Split(" "))
        {
            if (w == "")
                continue;
            var match = pattern.Match(w);
            if (!match.Success)
                throw new System.ArgumentException($"problems with regex. word {w}");
            var dw = new DialogueWord();
            dw.word = match.Groups[1].Value.Replace(promptReplace, currentPrompt.Value);
            dw.delay = defaultDelay;
            if (match.Groups[2].Success)
            {
                dw.delay = float.Parse(match.Groups[2].Value, CultureInfo.InvariantCulture.NumberFormat);
            }
            yield return dw;
        }
    }
}
