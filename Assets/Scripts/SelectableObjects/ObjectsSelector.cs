using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsSelector : MonoBehaviour
{
    [SerializeField]
    private InputPointer pointer;
    [SerializeField]
    private LayerMask picturesLayer;

    private ObjectSelectable currentlySellected;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentlySellected = SelectSinglePicture();
            currentlySellected?.OnSelect();
        }
        if (Input.GetMouseButtonUp(0))
        {
            currentlySellected?.OnRelease();
            currentlySellected = null;
        }
    }

    private ObjectSelectable SelectSinglePicture()
    {
        var collisions = Physics2D.RaycastAll(pointer.Position, Vector2.zero, 1, picturesLayer, -1000, 1000);
        ObjectSelectable currentSelected = null;
        foreach (var c in collisions)
        {
            if (!c.collider.TryGetComponent<ObjectSelectable>(out var selectable))
                continue;
            if (currentSelected == null || selectable.Order >= currentSelected.Order)
            {
                currentSelected = selectable;
            }
        }
        return currentSelected;
    }
}
