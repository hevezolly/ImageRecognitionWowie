using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosedBook : StandartPictureMovement
{
    [SerializeField]
    private Vector2 openCenter;
    [SerializeField]
    private OpenedBook openedBook;

    private Vector2 selectPosition;
    public override void OnSelect()
    {
        base.OnSelect();
        selectPosition = input.Position;
    }

    public override void OnRelease()
    {
        base.OnRelease();
        if (Vector2.Distance(selectPosition, input.Position) < 0.01f)
        {
            OnOpen();
        }
    }

    private void OnOpen()
    {
        openedBook.transform.SetPositionAndRotation(transform.TransformPoint(openCenter),
            transform.rotation);
        openedBook.gameObject.SetActive(true);
        gameObject.SetActive(false);
        openedBook.OnOpen(-(int)Mathf.Sign(transform.localScale.x));
        openedBook.MoveOnTop();
    }

    public void Open(Vector3 position)
    {
        var offset = transform.TransformVector(-openCenter);
        transform.position = position + offset;
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(openCenter, 0.1f);
    }


}
