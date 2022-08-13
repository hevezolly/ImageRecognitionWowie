using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ImagePart : MonoBehaviour, IPartProvider
{
    [SerializeField]
    private ImagePropertyID id;
    public ImagePropertyID Id => id;
    public abstract void ApplyModification(ImagePartModification modification);
    public abstract void PopulateBy(IImageRequest request);
    public abstract void FillDescription(ImageDescription description);

    private Material _mat;
    public virtual Material usedMaterial
    {
        get
        {
            if (_mat == null)
                _mat = GetComponent<Renderer>().material;
            return _mat;
        }
    }

    public virtual Bounds GetBounds()
    {
        return new Bounds();
    }

    public ImagePart GetUninstanciatedPart()
    {
        return this;
    }
}
