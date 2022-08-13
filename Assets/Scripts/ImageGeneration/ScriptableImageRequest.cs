using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "image gen/image request")]
public class ScriptableImageRequest : ScriptableObject, IImageRequest
{
    [SerializeField]
    private SinglePartRequest image;
    public SinglePartRequest Image => image;

    [SerializeField]
    private SinglePartRequest body;
    public SinglePartRequest Body => body;

    [SerializeField]
    private SinglePartRequest eyes;

    public SinglePartRequest Eyes => eyes;

    [SerializeField]
    private SinglePartRequest nose;

    public SinglePartRequest Nose => nose;

    [SerializeField]
    private SinglePartRequest mouth;

    public SinglePartRequest Mouth => mouth;
}
