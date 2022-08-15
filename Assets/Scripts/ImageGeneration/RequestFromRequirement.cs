using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RequestFromRequirement : MonoBehaviour
{

    [SerializeField]
    [Range(0,1)]
    private float chanceOfEmpty;
    [SerializeField]
    private ImagePart image;

    [SerializeField]
    private List<ImagePart> bodies;

    [SerializeField]
    private List<ImagePart> eyes;

    [SerializeField]
    private List<ImagePart> noses;

    [SerializeField]
    private List<ImagePart> mouthes;

    [SerializeField]
    private ExposedModification emptyModification;

    [SerializeField]
    private List<ExposedModification> StyleModifications;

    [SerializeField]
    private List<ExposedModification> ShapeModifications;

    [SerializeField]
    private List<ImagePropertyID> widePartIds;

    [SerializeField]
    private List<ImagePropertyID> wideModificationsIds;

    private IEnumerable<ImagePart> singleParts
    {
        get
        {
            return partsByCategories.SelectMany(v => v);
        }
    }

    private IEnumerable<IEnumerable<ImagePart>> partsByCategories
    {
        get
        {
            yield return new[] { image };
            yield return bodies;
            yield return eyes;
            yield return noses;
            yield return mouthes;
        }
    }


    private Dictionary<ImagePropertyID, ImagePart> idToPart;
    private Dictionary<ImagePropertyID, ExposedModification> idToMod;

    private Dictionary<ImagePropertyID, List<ImagePart>> inverseOfWidePartId;
    private Dictionary<ImagePropertyID, List<ExposedModification>> inverseOfWideModificationId;

    private void Awake()
    {
        idToPart = new Dictionary<ImagePropertyID, ImagePart>();
        foreach (var part in singleParts)
        {
            idToPart[part.Id] = part;
        }
        idToMod = new Dictionary<ImagePropertyID, ExposedModification>();
        foreach (var m in StyleModifications)
        {
            idToMod[m.modification.Id] = m;
        }
        foreach (var m in ShapeModifications)
        {
            idToMod[m.modification.Id] = m;
        }

        inverseOfWidePartId = new Dictionary<ImagePropertyID, List<ImagePart>>();

        foreach (var widePart in widePartIds)
        {
            inverseOfWidePartId[widePart] = new List<ImagePart>();
            inverseOfWidePartId[widePart].Add(null);
            foreach (var inverseId in widePart.GetInverseIds())
            {
                inverseOfWidePartId[widePart].Add(idToPart[inverseId]);
            }
        }

        inverseOfWideModificationId = new Dictionary<ImagePropertyID, List<ExposedModification>>();

        foreach (var widePart in wideModificationsIds)
        {
            inverseOfWideModificationId[widePart] = new List<ExposedModification>();
            inverseOfWideModificationId[widePart].Add(null);
            foreach (var inverseId in widePart.GetInverseIds())
            {
                inverseOfWideModificationId[widePart].Add(idToMod[inverseId]);
            }
        }
    }

    public IImageRequest RequestCorrect(IImageRequirement requirement)
    {
        var r = new SimpleRequest();
        r.Image.PartProvider = image;
        r.Image.Style = GetModById(requirement.Image.styleId, 0, StyleModifications);
        r.Image.Shape = GetModById(requirement.Image.shapeId, 0, ShapeModifications);

        r.Body.PartProvider = GetImagePartFromId(requirement.Body.partId,
            bodies,
            requirement.Eyes.partId == null &&
            requirement.Nose.partId == null &&
            requirement.Mouth.partId == null);
        r.Body.Style = GetModById(requirement.Body.styleId, 1, StyleModifications);
        r.Body.Shape = GetModById(requirement.Body.shapeId, 1, ShapeModifications);

        r.Eyes.PartProvider = GetImagePartFromId(requirement.Eyes.partId, eyes, true);
        r.Eyes.Style = GetModById(requirement.Eyes.styleId, 2, StyleModifications);
        r.Eyes.Shape = GetModById(requirement.Eyes.shapeId, 2, ShapeModifications);

        r.Nose.PartProvider = GetImagePartFromId(requirement.Nose.partId, noses, true);
        r.Nose.Style = GetModById(requirement.Nose.styleId, 3, StyleModifications);
        r.Nose.Shape = GetModById(requirement.Nose.shapeId, 3, ShapeModifications);

        r.Mouth.PartProvider = GetImagePartFromId(requirement.Mouth.partId, mouthes, true);
        r.Mouth.Style = GetModById(requirement.Mouth.styleId, 4, StyleModifications);
        r.Mouth.Shape = GetModById(requirement.Mouth.shapeId, 4, ShapeModifications);

        return r;
    }

    public IImageRequest RequestIncorrect(IImageRequirement requirement)
    {
        return RequestCorrect(new SimpleRequirement());
    }

    private ImagePart GetImagePartFromId(ImagePropertyID partId, List<ImagePart> allNotEmpty, bool allowEmpty)
    {
        List<ImagePart> possibleStyleMod;
        if (partId == null)
        {
            possibleStyleMod = new List<ImagePart>(allNotEmpty);
            if (allowEmpty && Random.value * 0.999999f < chanceOfEmpty)
                possibleStyleMod.Add(null);
        }
        else if (inverseOfWidePartId.ContainsKey(partId))
            possibleStyleMod = partId.GetChildIds().Select(id => idToPart[id]).ToList();
        else
            possibleStyleMod = new[] { idToPart[partId] }.ToList();
        return possibleStyleMod[Random.Range(0, possibleStyleMod.Count)];
    }

    private ImagePartModification GetModById(ImagePropertyID partId, int partIndex, IEnumerable<ExposedModification> allNotEmpty)
    {
        List<ExposedModification> possibleStyleMod;
        if (partId == null)
            possibleStyleMod = allNotEmpty.Concat(Enumerable.Repeat(emptyModification, 1)).ToList();
        else if (inverseOfWideModificationId.ContainsKey(partId))
            possibleStyleMod = partId.GetChildIds().Select(id => idToMod[id]).ToList();
        else
            possibleStyleMod = new[] { idToMod[partId] }.ToList();
        return AnyModification(possibleStyleMod, partIndex);
    }

    private ImagePartModification AnyModification(List<ExposedModification> possibilities, int partIndex)
    {
        var sum = possibilities.Sum(p => p.WeightByIndex(partIndex));
        var value = Random.Range(0, sum*0.999999999f);
        var current = 0f;
        foreach (var possibility in possibilities)
        {
            var weight = possibility.WeightByIndex(partIndex);
            if (value >= current && value < current + weight)
                return possibility.modification;
            current += weight;
        }
        return null;
    }

    [System.Serializable]
    private class ExposedModification
    {
        public ImagePartModification modification;

        [Range(0, 1)]
        public float ImageWeight = 1;
        [Range(0, 1)]
        public float BodyWeight = 1;
        [Range(0, 1)]
        public float EyeWeight = 1;
        [Range(0, 1)]
        public float NoseWeight = 1;
        [Range(0, 1)]
        public float MouthWeight = 1;

        public float WeightByIndex(int index)
        {
            switch (index)
            {
                case 0:
                    return ImageWeight;
                case 1:
                    return BodyWeight;
                case 2:
                    return EyeWeight;
                case 3:
                    return NoseWeight;
                case 4:
                    return MouthWeight;
                default:
                    throw new System.ArgumentException("incorrect index");
            }
        }

    }
}

public static class RequirementExtention
{
    public static IEnumerable<PartRequirement> Enumerate(this IImageRequirement req)
    {
        yield return req.Image;
        yield return req.Body;
        yield return req.Eyes;
        yield return req.Nose;
        yield return req.Mouth;
    }
}
