using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartPictureMovement : ObjectSelectable
{
    [SerializeField]
    private InputPointer input;
    [SerializeField]
    private SpriteRenderer renderer;
    [SerializeField]
    private GlobalObjectsOrder globalOrders;

    [SerializeField]
    [Range(0, 360)]
    private float maxRotationWhileDrug;

    [SerializeField]
    [Range(0, 360)]
    private float initialrotationOffset;

    private Vector2 globalGrubOffset;
    private bool isInDrug;

    public override int Order
    {
        get => renderer.sortingOrder;
        set => renderer.sortingOrder = value;
    }

    private void OnEnable()
    {
        globalOrders.Register(this);
    }

    private void OnDisable()
    {
        globalOrders.Unregister(this);
    }

    public override void OnSelect()
    {
        var globalPos = input.Position;
        globalGrubOffset = (Vector2)transform.position - globalPos;
        isInDrug = true;
        globalOrders.RequestHighestOrder(this);
    }

    public override void OnRelease()
    {
        isInDrug = false;
    }

    private void Update()
    {
        if (isInDrug)
        {
            var newMousePos = input.Position;
            transform.position = newMousePos + globalGrubOffset;
        }
    }
}
