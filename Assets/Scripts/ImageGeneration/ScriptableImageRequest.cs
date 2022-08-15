using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "image gen/image request")]
public class ScriptableImageRequest : ScriptableObject, IImageRequest
{
    [SerializeField]
    private SinglePartRequest image;
    public SinglePartRequest Image { get => image; set => image = value; }

    [SerializeField]
    private SinglePartRequest body;
    public SinglePartRequest Body { get => body; set => body = value; }

    [SerializeField]
    private SinglePartRequest eyes;

    public SinglePartRequest Eyes { get => eyes; set => eyes = value; }

    [SerializeField]
    private SinglePartRequest nose;

    public SinglePartRequest Nose { get => nose; set => nose = value; }

    [SerializeField]
    private SinglePartRequest mouth;

    public SinglePartRequest Mouth { get => mouth; set => mouth = value; }
}
