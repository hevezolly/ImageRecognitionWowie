using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FullImage : SelfPopulatingPart
{
    protected ImagePart body;
    public override void PopulateBy(IImageRequest request)
    {
        if (request.HasBody())
        {
            body = InstanciateBody(request.Body.PartProvider);
            body.PopulateBy(request);
        }
        PopulateSelf(request.Image);
    }

    protected abstract ImagePart InstanciateBody(IPartProvider provider);

    public override void FillDescription(ImageDescription description)
    {
        var style = this.GetLastModificationId(ModificationType.Style);
        var shape = this.GetLastModificationId(ModificationType.Shape);
        description.Image = new PartRequirement()
        {
            partId = Id,
            shapeId = shape,
            styleId = style
        };
        body?.FillDescription(description);
    }
}
