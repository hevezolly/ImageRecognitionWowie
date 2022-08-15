using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRequirement : IImageRequirement
{
    public SimpleRequirement()
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

    public List<bool> empty;

    public void SetEmptyByRequirement(PartRequirement req)
    {
        if (req == Image)
            empty[0] = true;
        if (req == Body)
            empty[1] = true;
        if (req == Eyes)
            empty[2] = true;
        if (req == Nose)
            empty[3] = true;
        if (req == Mouth)
            empty[4] = true;
    }
}
