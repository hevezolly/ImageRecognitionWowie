using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherSpriteOrderOffset : MonoBehaviour, ISortingOrderOffset
{
    [SerializeField]
    private int offset;
    [SerializeField]
    private SpriteRenderer targetRenderer;
    [SerializeField]
    private SpriteRenderer lookupRenderer;

    private void Awake()
    {
        targetRenderer.sortingOrder = lookupRenderer.sortingOrder + offset;
    }

    public void OnSelect()
    {
        targetRenderer.sortingOrder = lookupRenderer.sortingOrder + offset;
    }

    public void SetLookupRenderer(SpriteRenderer renderer)
    {
        lookupRenderer = renderer;
    }
}
