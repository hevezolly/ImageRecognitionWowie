using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IImageRequest
{
    SinglePartRequest Image { get; }
    SinglePartRequest Body { get; }
    SinglePartRequest Eyes { get; }
    SinglePartRequest Nose { get; }
    SinglePartRequest Mouth { get; }
}

public static class ImageRequestExtentions
{
    public static SinglePartRequest GetCorrectBodyVariant(this IImageRequest request)
    {
        if (request.Body == null || request.Body.PartProvider == null)
            return null;
        var body = request.Body.PartProvider;
        var shape = request.Body.Shape ?? (request.Image.Shape.Propagate() ? request.Image.Shape : null);
        var style = request.Body.Style ?? (request.Image.Style.Propagate() ? request.Image.Style : null);

        return new SinglePartRequest(body, shape, style);
    }
    
    private static bool Propagate(this ImagePartModification mod)
    {
        return mod != null && mod.Propagate;
    }

    private static SinglePartRequest GetCorrectVariantOf(this IImageRequest request, SinglePartRequest part)
    {
        if (request.Body == null || request.Body.PartProvider == null)
            return null;
        if (part == null || part.PartProvider == null)
            return null;
        var body = part.PartProvider;
        var shape = part.Shape ?? 
            (request.Body.Shape.Propagate() ? request.Body.Shape : null) ??
            (request.Image.Shape.Propagate() ? request.Image.Shape : null);
        var style = part.Style ??
            (request.Body.Style.Propagate() ? request.Body.Style : null) ??
            (request.Image.Style.Propagate() ? request.Image.Style : null);

        return new SinglePartRequest(body, shape, style);
    }

    public static SinglePartRequest GetCorrectEyesVariant(this IImageRequest request) =>
        request.GetCorrectVariantOf(request.Eyes);

    public static SinglePartRequest GetCorrectNoseVariant(this IImageRequest request) =>
        request.GetCorrectVariantOf(request.Nose);

    public static SinglePartRequest GetCorrectMouthVariant(this IImageRequest request) =>
        request.GetCorrectVariantOf(request.Mouth);

    public static bool HasBody(this IImageRequest request)
    {
        return request.Body != null && request.Body.PartProvider != null;
    }

    private static bool HasPart(this IImageRequest request, SinglePartRequest part)
    {
        return request.HasBody() && part != null && part.PartProvider != null;
    }

    public static bool HasEyes(this IImageRequest request) => request.HasPart(request.Eyes);
    public static bool HasMouth(this IImageRequest request) => request.HasPart(request.Mouth);
    public static bool HasNose(this IImageRequest request) => request.HasPart(request.Nose);
}

[System.Serializable]
public class SinglePartRequest
{
    [SerializeField]
    private IRef<IPartProvider> partProvider;
    [SerializeField]
    private ImagePartModification shape;
    [SerializeField]
    private ImagePartModification style;

    public IPartProvider PartProvider { get => baseProvider ?? partProvider?.I; set => baseProvider = value; }
    public ImagePartModification Shape { get => shape; set => shape = value; }
    public ImagePartModification Style { get => style; set => style = value; }

    private IPartProvider baseProvider;

    public SinglePartRequest() { }
    public SinglePartRequest(IPartProvider provider, ImagePartModification shape, ImagePartModification style)
    {
        baseProvider = provider;
        this.shape = shape;
        this.style = style;
    }
}
