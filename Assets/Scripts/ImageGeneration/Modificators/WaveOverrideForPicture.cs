using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "image gen/modifications/wave for picture")]
public class WaveOverrideForPicture : ImagePartModification
{
    public override void MakeChangeTo(ImagePart part)
    {
        foreach (var r in part.GetComponentsInChildren<SpriteRenderer>())
        {
            if (r.gameObject.tag != "NoMaskInterraction")
                r.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        }
        ((ImageDummy)part).mask.enabled = false;
        ((ImageDummy)part).WavyMask.enabled = true;
    }
}
