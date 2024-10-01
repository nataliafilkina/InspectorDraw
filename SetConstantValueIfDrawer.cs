using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace InspectorDraw
{
    [CustomPropertyDrawer(typeof(SetConstantValueIfAttribute))]
    public class SetConstantValueIfDrawer : PropertyDrawer
    {
        #region Fields

        SetConstantValueIfAttribute setValueIf;
        SerializedProperty comparedField;

        #endregion

        /// <summary>
        /// Errors default to showing the property.
        /// </summary>
        private bool CheckСomparedField(SerializedProperty property)
        {
            setValueIf = attribute as SetConstantValueIfAttribute;
            string path = property.propertyPath.Contains(".") ? Path.ChangeExtension(property.propertyPath, setValueIf.comparedPropertyName) : setValueIf.comparedPropertyName;

            comparedField = property.serializedObject.FindProperty(path);

            if (comparedField == null)
            {
                Debug.LogError("Cannot find property with name: " + path);
                return true;
            }

            switch (comparedField.type)
            {
                case "bool":
                    return comparedField.boolValue.Equals(setValueIf.comparedValue);
                case "Enum":
                    return comparedField.enumValueIndex.Equals((int)setValueIf.comparedValue);
                default:
                    Debug.LogError("Error: " + comparedField.type + " is not supported of " + path);
                    return true;
            }
        }

        private bool TrySetDefaultValue(SerializedProperty property)
        {
            var typeDefaultValue = setValueIf.defaultValue;
            var isValueSet = true;
            switch (Type.GetTypeCode(typeDefaultValue.GetType()))
            {
                case TypeCode.Int32:
                    property.intValue = (int)setValueIf.defaultValue;
                    break;
                case TypeCode.String:
                    property.stringValue = setValueIf.defaultValue.ToString();
                    break;
                default:
                    Debug.LogError("Error: " + comparedField.type + " is not supported of ");
                    isValueSet = false ;
                    break;
            }
            return isValueSet;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (CheckСomparedField(property) && TrySetDefaultValue(property))
            {
                if (setValueIf.readOnly)
                {
                    GUI.enabled = false;
                    EditorGUI.PropertyField(position, property, label);
                    GUI.enabled = true;
                }
            }
            else
            {
                EditorGUI.PropertyField(position, property, label);
            }
        }
    }
}