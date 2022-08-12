using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GlobalObjectsOrder))]
public class GlobalOrderDisplay : Editor
{
    private GlobalObjectsOrder order;

    private GUIContent maxOrder;
    private GUIContent minOrder;

    private void OnEnable()
    {
        order = (GlobalObjectsOrder)target;
        maxOrder = new GUIContent("MaxOrder");
        minOrder = new GUIContent("MinOrder");
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space();
        GUI.enabled = false;
        EditorGUILayout.IntField(maxOrder, order.MaxOrder);
        EditorGUILayout.IntField(minOrder, order.MinOrder);
        GUI.enabled = true;
    }
}
