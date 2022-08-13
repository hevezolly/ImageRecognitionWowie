using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StandartPictureMovement), typeof(SpriteRenderer))]
public class SortingOriderOffsetLinker : MonoBehaviour
{
    private StandartPictureMovement movement;
    private SpriteRenderer lookupRenderer;

    private List<ISortingOrderOffset> offsets = new List<ISortingOrderOffset>();

    private void Awake()
    {
        movement = GetComponent<StandartPictureMovement>();
        lookupRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Link();
    }
    public void Link()
    {
        GetComponentsInChildren(offsets);
        foreach (var o in offsets)
        {
            o.SetLookupRenderer(lookupRenderer);
            movement.SelectEvent.RemoveListener(o.OnSelect);
            movement.SelectEvent.AddListener(o.OnSelect);
            o.OnSelect();
        }
    }
}
