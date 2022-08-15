using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "image gen/Requirement")]
public class Requirement : ScriptableObject, IImageRequirement
{
    [SerializeField]
    private PartRequirement image;
    public PartRequirement Image { get => image; set => image = value; }

    [SerializeField]
    private PartRequirement body;
    public PartRequirement Body { get => body; set => body = value; }

    [SerializeField]
    private PartRequirement eye;
    public PartRequirement Eyes { get => eye; set => eye = value; }

    [SerializeField]
    private PartRequirement nose;
    public PartRequirement Nose { get => nose; set => nose = value; }

    [SerializeField]
    private PartRequirement mouth;

    public PartRequirement Mouth { get => mouth; set => mouth = value; }
}
