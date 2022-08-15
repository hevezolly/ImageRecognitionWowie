using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class BodyImagePart : MeshImagePart
{
    protected List<ImagePart> eyes;
    protected List<ImagePart> noses;
    protected List<ImagePart> mouths;
    public override void PopulateBy(IImageRequest request)
    {
        if (request.HasEyes())
        {
            eyes = InstanciateEyes(request.Eyes.PartProvider);
            eyes.ForEach(e => e.PopulateBy(request));
        }
        if (request.HasMouth())
        {
            mouths = InstanciateMouth(request.Mouth.PartProvider);
            mouths.ForEach(e => e.PopulateBy(request));
        }
        if (request.HasNose())
        {
            noses = InstanciateNose(request.Nose.PartProvider);
            noses.ForEach(e => e.PopulateBy(request));
        }
        PopulateSelf(request.GetCorrectBodyVariant());
    }

    public override void FillDescription(ImageDescription description)
    {
        var style = this.GetLastModificationId(ModificationType.Style);
        var shape = this.GetLastModificationId(ModificationType.Shape);
        description.Body = new PartRequirement()
        {
            partId = Id,
            shapeId = shape,
            styleId = style
        };
        eyes?.FirstOrDefault()?.FillDescription(description);
        noses?.FirstOrDefault()?.FillDescription(description);
        mouths?.FirstOrDefault()?.FillDescription(description);
    }

    protected abstract List<ImagePart> InstanciateEyes(IPartProvider partProvider);
    protected abstract List<ImagePart> InstanciateMouth(IPartProvider partProvider);
    protected abstract List<ImagePart> InstanciateNose(IPartProvider partProvider);
}
