using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRequest : IImageRequest
{
    public SimpleRequest()
    {
        Image = new SinglePartRequest();
        Body = new SinglePartRequest();
        Mouth = new SinglePartRequest();
        Eyes = new SinglePartRequest();
        Nose = new SinglePartRequest();
    }
    public SinglePartRequest Image { get; set; }

    public SinglePartRequest Body { get; set; }

    public SinglePartRequest Eyes { get; set; }

    public SinglePartRequest Nose { get; set; }

    public SinglePartRequest Mouth { get; set; }
}
