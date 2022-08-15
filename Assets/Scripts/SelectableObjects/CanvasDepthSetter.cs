using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasDepthSetter : MonoBehaviour, ISortingOrderOffset
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private SpriteRenderer glassRenderer;
    [SerializeField]
    private int offset = -1;

    private void Awake()
    {
        OnSelect();
    }

    public void OnSelect()
    {
        canvas.sortingOrder = glassRenderer.sortingOrder +offset;
    }

    public void SetLookupRenderer(SpriteRenderer renderer)
    {
        glassRenderer = renderer;
    }
}
