using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;

public class RequirementGenerator : MonoBehaviour
{

    [SerializeField]
    private ScriptableValue<Requirement> currentRequirement;
    [SerializeField]
    private ScriptableValue<string> prompt;


    public string GeneratePromptFromRequirement(IImageRequirement requirement)
    {
        var parts = requirement.Enumerate();
        if (parts.First().shapeId == null && parts.First().styleId == null)
            parts = parts.Skip(1);
        parts = parts.OrderBy(v => Random.value);
        var stringParts = new List<string>();
        foreach (var p in parts)
        {
            var s = new StringBuilder();
            if (p.partId == null)
                continue;
            if (p.styleId != null)
            {
                s.Append(p.styleId.Value);
                s.Append(" ");
            }
            if (p.shapeId != null)
            {
                s.Append(p.shapeId.Value);
                s.Append(" ");
            }
            s.Append(p.partId.Value);
            stringParts.Add(s.ToString());
        }
        if (stringParts.Count == 0)
            stringParts.Add(requirement.Image.partId.Value);
        prompt.Value = string.Join(", ", stringParts);
        return prompt.Value;
    }

    private void OnApplicationQuit()
    {
        prompt.Value = "";
    }
}
