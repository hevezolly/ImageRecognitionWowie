using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskOrderOffset : MonoBehaviour, ISortingOrderOffset
{
    [SerializeField]
    private SpriteMask mask;
    [SerializeField]
    [Min(0)]
    private int addOffset;
    [SerializeField]
    [Min(0)]
    private int subOffset;
    private SpriteRenderer lookupRenderer;
    public void OnSelect()
    {
        mask.frontSortingOrder = lookupRenderer.sortingOrder + addOffset;
        mask.backSortingOrder = lookupRenderer.sortingOrder - subOffset;
    }

    public void SetLookupRenderer(SpriteRenderer renderer)
    {
        lookupRenderer = renderer;
    }
}
