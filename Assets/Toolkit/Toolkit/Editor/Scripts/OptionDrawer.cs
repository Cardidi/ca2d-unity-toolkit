using UnityEditor;
using UnityEngine;

namespace Ca2d.Toolkit.Editor
{
    [CustomPropertyDrawer(typeof(Option<>))]
    public class OptionDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property.FindPropertyRelative("Value")); ;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var value = property.FindPropertyRelative("Value");
            var enable = property.FindPropertyRelative("Enabled");

            var toggleRect = position;
            toggleRect.height = toggleRect.width = 18;

            enable.boolValue = EditorGUI.Toggle(toggleRect, GUIContent.none, enable.boolValue);
            var prev = GUI.enabled;
            GUI.enabled = enable.boolValue;

            var propertyRect = position;
            propertyRect.x += 18;
            propertyRect.width -= 18;
            EditorGUI.PropertyField(propertyRect, value, label, true);
            GUI.enabled = prev;

            if (value.serializedObject.hasModifiedProperties) value.serializedObject.ApplyModifiedProperties();
        }
    }
}