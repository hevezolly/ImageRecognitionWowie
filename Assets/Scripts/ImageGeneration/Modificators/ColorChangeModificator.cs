using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="image gen/modifications/ColorChange")]
public class ColorChangeModificator : ImagePartModification
{
    [SerializeField]
    private Color color;
    public override void MakeChangeTo(ImagePart part)
    {
        part.usedMaterial.SetColor("_BaseColor", color);
    }
}
