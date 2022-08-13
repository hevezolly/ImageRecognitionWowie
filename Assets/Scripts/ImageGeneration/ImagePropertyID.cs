using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "image gen/ID")]
public class ImagePropertyID : ScriptableObject
{
    [SerializeField]
    private string id;
    [SerializeField]
    [TextArea]
    private string Description;
    [SerializeField]
    private List<ImagePropertyID> childProperties;

    public string Value => id;

    public bool IsDescriptionFor(ImagePropertyID otherId)
    {
        return otherId == this || childProperties.Any(p => p.IsDescriptionFor(otherId));
    }
}
