using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoseImagePart : BodyPartImagePart
{
    protected override void FillPartialRequirement(PartRequirement requirement, ImageDescription description)
    {
        description.Nose= requirement;
    }

    protected override SinglePartRequest FindSelfInRequest(IImageRequest request)
    {
        return request.GetCorrectNoseVariant();
    }
}
