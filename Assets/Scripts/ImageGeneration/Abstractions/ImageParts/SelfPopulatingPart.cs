using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SelfPopulatingPart : OverrideModificationImagePart
{
    protected virtual void PopulateSelf(SinglePartRequest request)
    {
        if (request.Shape != null)
            ApplyModification(request.Shape);
        if (request.Style != null)
            ApplyModification(request.Style);
    }
}
