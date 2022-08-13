using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IRef<T> : ISerializationCallbackReceiver where T : class
{
    [SerializeField]
    private UnityEngine.Object target;
    public T I { get => target as T; }

    public static implicit operator bool(IRef<T> ir) => ir.target != null;
    void OnValidate()
    {
        if ((target is T))
            return;
        if (target is GameObject gameObject)
        {
            target = null;
            foreach (Component c in gameObject.GetComponents<Component>())
            {
                if (c is T)
                {
                    target = c;
                    return;
                }
            }
        }
        target = null;
    }

    void ISerializationCallbackReceiver.OnBeforeSerialize() => this.OnValidate();
    void ISerializationCallbackReceiver.OnAfterDeserialize() { }
}