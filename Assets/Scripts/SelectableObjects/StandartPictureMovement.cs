using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StandartPictureMovement : ObjectSelectable
{
    [SerializeField]
    private InputPointer input;
    [SerializeField]
    private SpriteRenderer renderer;
    [SerializeField]
    private GlobalObjectsOrder globalOrders;
    [SerializeField]
    private float dimentionRadius;
    [SerializeField]
    [Range(0, 1)]
    private float friction;

    [SerializeField]
    [Range(0, 360)]
    private float maxRotationWhileDrug;

    [SerializeField]
    [Range(-180, 180)]
    private float initialrotationOffset = 0;

    [SerializeField]
    private UnityEvent OnSelected;

    private Quaternion rotationFromGrubToUp;

    private Vector2 localGrubOffset;
    private bool isInDrug;

    private Vector2 previusMousePos;
    private Vector2 centerLine;

    private float halvedRotation;

    private float offcenterCoefficient;

    private void Awake()
    {
        ClampAngles();
        halvedRotation = maxRotationWhileDrug / 2;
        centerLine = Quaternion.Euler(0, 0, initialrotationOffset) * Vector2.up;
    }

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
        var dir = globalPos - (Vector2)transform.position;
        offcenterCoefficient = Mathf.Clamp01(dir.magnitude / dimentionRadius) * friction;
        rotationFromGrubToUp = Quaternion.FromToRotation(dir, transform.up);
        localGrubOffset = transform.InverseTransformVector(dir);
        isInDrug = true;
        previusMousePos = Input.mousePosition;
        globalOrders.RequestHighestOrder(this);
        OnSelected?.Invoke();
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
            var dirToDesiredPos = newMousePos - (Vector2)transform.position;

            var newUp = Vector2.Lerp(transform.up, rotationFromGrubToUp * dirToDesiredPos.normalized, offcenterCoefficient);
            var angleFromCenter = Vector2.SignedAngle(centerLine, newUp);
            if (angleFromCenter > halvedRotation)
            {
                var angle = (initialrotationOffset - halvedRotation) * Mathf.Deg2Rad;
                newUp = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
            }
            if (angleFromCenter < -halvedRotation)
            {
                var angle = (initialrotationOffset + halvedRotation) * Mathf.Deg2Rad;
                newUp = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
            }

            transform.rotation = Quaternion.LookRotation(Vector3.forward, newUp);

            var globalOffset = (Vector2)transform.TransformVector(localGrubOffset);
            var pos2d = newMousePos - globalOffset;
            transform.position = new Vector3(pos2d.x, pos2d.y, transform.position.z);
            previusMousePos = newMousePos;
        }
    }

    private void ClampAngles()
    {
        var upAngle = Vector2.SignedAngle(Vector2.up, transform.up);
        initialrotationOffset = Mathf.Clamp(initialrotationOffset, -maxRotationWhileDrug / 2 + upAngle,
            maxRotationWhileDrug / 2 + upAngle);
    }

    private void OnValidate()
    {
        ClampAngles();
    }

    private void OnDrawGizmosSelected()
    {
        var scale = dimentionRadius;
        const int numOfIterations = 15;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.up*scale);

        Gizmos.color = Color.green;
        var centerDirection = Quaternion.Euler(0, 0, initialrotationOffset) * Vector2.up;
        var previusDirection = Quaternion.Euler(0, 0, -maxRotationWhileDrug / 2) * centerDirection;
        var stepRotator = Quaternion.Euler(0, 0, maxRotationWhileDrug / numOfIterations);

        Gizmos.DrawLine(transform.position, transform.position + previusDirection * scale);
        for (var i = 0; i < numOfIterations; i++)
        {
            var currentDir = stepRotator * previusDirection;
            Gizmos.DrawLine(transform.position + previusDirection * scale,
                transform.position + currentDir * scale);
            previusDirection = currentDir;
        }
        Gizmos.DrawLine(transform.position, transform.position + previusDirection * scale);

    }
}
