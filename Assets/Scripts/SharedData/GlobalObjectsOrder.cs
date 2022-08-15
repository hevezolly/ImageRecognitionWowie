using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "shared data/Objects Order")]
public class GlobalObjectsOrder : ScriptableObject, IComparer<ObjectSelectable>
{
    [SerializeField]
    private int minOrder;
    [SerializeField]
    private int maxAvalibleOrder;
    [SerializeField]
    private int offsetBetweenOrders;

    public delegate void OffsetOrders(int delta);
    public int MinOrder { get; private set; }
    public int MaxOrder { get; private set; }

    private SortedSet<ObjectSelectable> selectableObjects;

    private void OnEnable()
    {
        var min = int.MaxValue;
        var max = int.MinValue;

        var allSelectable = FindObjectsOfType<ObjectSelectable>();
        selectableObjects = new SortedSet<ObjectSelectable>(this);
        foreach (var s in allSelectable)
        {
            min = Mathf.Min(min, s.Order);
            max = Mathf.Max(max, s.Order);
            selectableObjects.Add(s);
        }
        UpdateOrders();
    }

    public int RequestHighestOrder(ObjectSelectable by)
    {
        if (!selectableObjects.Contains(by))
            Register(by);
        if (by == selectableObjects.Max)
            return MaxOrder;
        if (MaxOrder + offsetBetweenOrders > maxAvalibleOrder)
            OffsetAllOrders();
        
        selectableObjects.Remove(by);

        by.Order = MaxOrder + offsetBetweenOrders;

        selectableObjects.Add(by);

        UpdateOrders();

        return MaxOrder;
    }

    private void UpdateOrders()
    {
        if (selectableObjects.Count == 0)
            return;
        MaxOrder = selectableObjects.Max.Order;
        MinOrder = selectableObjects.Min.Order;
    }

    public void Register(ObjectSelectable obj)
    {
        if (selectableObjects.Contains(obj))
            return;
        selectableObjects.Add(obj);
        if (MaxOrder >= maxAvalibleOrder)
            OffsetAllOrders();
        UpdateOrders();
    }

    public void Unregister(ObjectSelectable obj)
    {
        if (selectableObjects.Contains(obj))
        {
            selectableObjects.Remove(obj);
            UpdateOrders();
        }
    }

    public void OffsetAllOrders()
    {
        var offset = minOrder - MinOrder;
        foreach (var s in selectableObjects)
        {
            s.Order += offset;
        }
        UpdateOrders();
    }

    public int Compare(ObjectSelectable x, ObjectSelectable y)
    {
        return x.Order.CompareTo(y.Order);
    }
}
