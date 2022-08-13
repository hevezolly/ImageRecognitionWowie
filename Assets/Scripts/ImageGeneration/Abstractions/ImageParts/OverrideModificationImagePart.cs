using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OverrideModificationImagePart : ImagePart
{
    [SerializeField]
    private List<ModificationOverride> modificationOverrides;
    private Dictionary<ImagePartModification, ImagePartModification> overrides;

    public Dictionary<ModificationType, List<ImagePartModification>> appliedModifications { get; private set; }

    protected virtual void Awake()
    {
        overrides = new Dictionary<ImagePartModification, ImagePartModification>();
        foreach (var o in modificationOverrides)
        {
            overrides.Add(o.modificationToOverride, o.newModification);
        }
        appliedModifications = new Dictionary<ModificationType, List<ImagePartModification>>();
    }
    public override void ApplyModification(ImagePartModification modification)
    {
        var mod = modification;
        if (overrides.ContainsKey(modification))
            mod = overrides[mod];
        if (!appliedModifications.ContainsKey(mod.ModificationType))
            appliedModifications[mod.ModificationType] = new List<ImagePartModification>();
        appliedModifications[mod.ModificationType].Add(mod);
        mod.MakeChangeTo(this);
    }

    [System.Serializable]
    private class ModificationOverride
    {
        public ImagePartModification modificationToOverride;
        public ImagePartModification newModification;
    }
}

public static class OverrideModificationExtention
{
    public static ImagePropertyID GetLastModificationId(this OverrideModificationImagePart part, ModificationType type)
    {
        if (!part.appliedModifications.ContainsKey(type))
            return null;
        var mods = part.appliedModifications[type];
        if (mods == null)
            return null;
        if (mods.Count == 0)
            return null;
        return mods[mods.Count - 1].Id;
    }
}
