using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "shared data/input pointer")]
public class InputPointer : ScriptableObject
{
    public Vector2 Position { get; private set; }

    public void UpdatePosition(Vector2 newPosition)
    {
        Position = newPosition;
    }
}
