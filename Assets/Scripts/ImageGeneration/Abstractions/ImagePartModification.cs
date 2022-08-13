using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ImagePartModification : ScriptableObject
{
    [SerializeField]
    private ImagePropertyID id;
    [SerializeField]
    private ModificationType modificationType;

    public ModificationType ModificationType => modificationType;
    public ImagePropertyID Id => id;
    public abstract void MakeChangeTo(ImagePart part);
    
    public bool FeetsForDescription(ImagePropertyID id)
    {
        return Id == id;
    }
}

public enum ModificationType
{
    Shape,
    Style,
    None
}
