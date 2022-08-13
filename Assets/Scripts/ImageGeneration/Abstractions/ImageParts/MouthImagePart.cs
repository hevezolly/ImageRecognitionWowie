using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MouthImagePart: BodyPartImagePart
{
    protected override void FillPartialRequirement(PartRequirement requirement, ImageDescription description)
    {
        description.Mouth = requirement;
    }

    protected override SinglePartRequest FindSelfInRequest(IImageRequest request)
    {
        return request.Mouth;
    }
}
