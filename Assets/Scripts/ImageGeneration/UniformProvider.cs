using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "image gen/UniformProvider")]
public class UniformProvider : ScriptableObject, IPartProvider
{
    [SerializeField]
    private List<ImagePart> parts;
    public ImagePart GetUninstanciatedPart()
    {
        return parts[Random.Range(0, parts.Count)];
    }
}
