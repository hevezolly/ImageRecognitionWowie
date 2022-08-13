using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagnifyingGlassCanvasDepthSetter : MonoBehaviour, ISortingOrderOffset
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private SpriteRenderer glassRenderer;

    private void Awake()
    {
        OnSelect();
    }

    public void OnSelect()
    {
        canvas.sortingOrder = glassRenderer.sortingOrder - 1;
    }

    public void SetLookupRenderer(SpriteRenderer renderer)
    {
        glassRenderer = renderer;
    }
}
