using System;
using UnityEditor;
using UnityEngine;

namespace InspectorDraw
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class RandomIdDrawer : PropertyAttribute
    {

    }

    [CustomPropertyDrawer(typeof(RandomIdDrawer))]
    public class ScriptebleObjectIdDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property == null) return;

            GUI.enabled = false;
            if (string.IsNullOrEmpty(property.stringValue))
                property.stringValue = Guid.NewGuid().ToString();

            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
}
