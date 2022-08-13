using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageDescription : IImageRequirement
{
    public PartRequirement Image { get; set; }

    public PartRequirement Body { get; set; }

    public PartRequirement Eyes { get; set; }

    public PartRequirement Nose { get; set; }

    public PartRequirement Mouth { get; set; }
}
