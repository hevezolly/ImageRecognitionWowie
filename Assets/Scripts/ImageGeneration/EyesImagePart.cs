using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesImagePart : BodyPartImagePart
{
    protected override void FillPartialRequirement(PartRequirement requirement, ImageDescription description)
    {
        description.Eyes = requirement;
    }

    protected override SinglePartRequest FindSelfInRequest(IImageRequest request)
    {
        return request.GetCorrectEyesVariant();
    }
}
