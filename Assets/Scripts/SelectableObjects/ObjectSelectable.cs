using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectSelectable : MonoBehaviour
{
    public abstract int Order { get; set; }

    public abstract void OnSelect();

    public abstract void OnRelease();
}
