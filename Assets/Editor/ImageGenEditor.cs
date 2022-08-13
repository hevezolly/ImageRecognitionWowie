using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ImageGenerator))]
public class ImageGenEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Generate") && Application.isPlaying)
            ((ImageGenerator)target).Generate();
    }
}
