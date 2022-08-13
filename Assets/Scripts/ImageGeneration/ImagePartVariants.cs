using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "image gen/Image Part Variant")]
public class ImagePartVariants : ScriptableObject
{
    [SerializeField]
    private ImagePropertyID id;
    public ImagePropertyID Id => id;

    [SerializeField]
    private List<ImagePart> variants;
}
