using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;
using System.Reflection;

[CustomPropertyDrawer(typeof(IRef<>))]
public class IRefPropertyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        var tooltip = new StringBuilder(label.tooltip);
        if (tooltip.Length > 0)
            tooltip.Append("\n");
        tooltip.Append("should implement ");
        tooltip.Append(fieldInfo.FieldType.GetGenericArguments()[0].Name);
        label.tooltip = tooltip.ToString();
        EditorGUI.PropertyField(position, property.FindPropertyRelative("target"), label);
        EditorGUI.EndProperty();
    }
}
