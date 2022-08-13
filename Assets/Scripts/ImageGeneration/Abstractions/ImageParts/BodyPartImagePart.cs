using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BodyPartImagePart : SelfPopulatingPart
{
    protected abstract SinglePartRequest FindSelfInRequest(IImageRequest request);
    protected abstract void FillPartialRequirement(PartRequirement requirement, ImageDescription description);

    public override void PopulateBy(IImageRequest request)
    {
        PopulateSelf(FindSelfInRequest(request));
    }

    public override void FillDescription(ImageDescription description)
    {
        var style = this.GetLastModificationId(ModificationType.Style);
        var shape = this.GetLastModificationId(ModificationType.Shape);
        FillPartialRequirement(new PartRequirement()
        {
            partId = Id,
            shapeId = shape,
            styleId = style
        }, description);
    }
}
