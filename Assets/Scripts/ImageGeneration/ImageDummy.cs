using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageDummy : SelfPopulatingPart
{
    [SerializeField]
    private GameObject successSymbol;
    [SerializeField]
    private GameObject failSymbol;

    private ImageDescription savedDescription;

    public ImageDescription Description => savedDescription;

    [SerializeField]
    private List<SpriteRenderer> allRenderers;
    [SerializeField]
    private Material materialToUse;

    public bool hasMask { get; private set; }

    private Material _copy;

    public SpriteMask mask;
    public SpriteMask WavyMask;

    public Texture2D MonsterTexture { get; private set; }

    public override Material usedMaterial => _copy;
    public void Initiate(ImageDescription description, Texture2D texture)
    {
        MonsterTexture = texture;
        _copy = new Material(materialToUse);
        savedDescription = description;
        foreach (var r in allRenderers)
            r.material = usedMaterial;
    }

    public void DisplayAnswer(IImageRequirement to)
    {
        var fit = savedDescription.IsFitForRequirement(to);
        if (fit)
            successSymbol.SetActive(true);
        else
            failSymbol.SetActive(true);
    }
    public override void FillDescription(ImageDescription description)
    {
        description.Body = savedDescription.Body;
        description.Image = savedDescription.Image;
        description.Eyes = savedDescription.Eyes;
        description.Nose = savedDescription.Nose;
        description.Mouth = savedDescription.Mouth;
    }

    public override void PopulateBy(IImageRequest request)
    {
        hasMask = request.Image.Shape != null;
        PopulateSelf(request.Image);
    }
}
