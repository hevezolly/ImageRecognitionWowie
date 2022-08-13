using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshRendererOrderOffset : MonoBehaviour, ISortingOrderOffset
{
    [SerializeField]
    private SpriteRenderer lookUpRenderer;
    [SerializeField]
    private SortingLayerSetter setter;
    [SerializeField]
    private int offset;

    public void OnSelect()
    {
        setter.SortingOrder = lookUpRenderer.sortingOrder + offset;
    }

    public void SetLookupRenderer(SpriteRenderer renderer)
    {
        lookUpRenderer = renderer;
    }
}
