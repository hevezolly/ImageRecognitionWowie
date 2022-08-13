using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IImageRequirement
{
    public PartRequirement Image { get; }
    public PartRequirement Body { get; }
    public PartRequirement Eyes { get; }
    public PartRequirement Nose { get; }
    public PartRequirement Mouth { get; }
}

[System.Serializable]
public class PartRequirement
{
    public ImagePropertyID partId;
    public ImagePropertyID styleId;
    public ImagePropertyID shapeId;
}
