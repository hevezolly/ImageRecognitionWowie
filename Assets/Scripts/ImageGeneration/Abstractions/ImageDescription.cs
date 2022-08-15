using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ImageDescription : IImageRequirement
{
    public ImageDescription()
    {
        Image = new PartRequirement();
        Body = new PartRequirement();
        Eyes = new PartRequirement();
        Nose = new PartRequirement();
        Mouth = new PartRequirement();
    }
    public PartRequirement Image { get; set; }

    public PartRequirement Body { get; set; }

    public PartRequirement Eyes { get; set; }

    public PartRequirement Nose { get; set; }

    public PartRequirement Mouth { get; set; }

    public bool IsFitForRequirement(IImageRequirement req)
    {
        return this.Enumerate()
            .Zip(req.Enumerate(), (description, requirement) => (description, requirement))
            .All(v =>
        {
            return DescFitForReq(v.description.partId, v.requirement.partId) &&
            DescFitForReq(v.description.styleId, v.requirement.styleId) &&
            DescFitForReq(v.description.shapeId, v.requirement.shapeId);
        });
    }

    private bool DescFitForReq(ImagePropertyID description, ImagePropertyID requirement)
    {
        return (requirement == null) ||
            (description == null && requirement == null) ||
            (requirement.IsDescriptionFor(description));
    }
}

