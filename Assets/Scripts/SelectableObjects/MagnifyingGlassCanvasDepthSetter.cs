using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagnifyingGlassCanvasDepthSetter : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private SpriteRenderer glassRenderer;

    private void Awake()
    {
        canvas.sortingOrder = glassRenderer.sortingOrder - 1;
    }

    public void OnSelect()
    {
        canvas.sortingOrder = glassRenderer.sortingOrder - 1;
    }
    
}
