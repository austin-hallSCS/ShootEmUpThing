using UnityEngine;
using UnityEditor;
using WizardGame.Stats;

namespace WizardGame.Editor
{
    // This tells Unity: "Whenever you draw a StatModifier in the Inspector, let ME handle it."
    [CustomPropertyDrawer(typeof(StatModifier), true)]
    public class StatModifierDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            // If the property is assigned, draw it normally. 
            // If null, just draw a single line for the button.
            return property.managedReferenceValue == null
                ? EditorGUIUtility.singleLineHeight
                : EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Draw the label (e.g., "Element 0")
            EditorGUI.BeginProperty(position, label, property);

            // 1. Check if the modifier is currently NULL (empty slot)
            if (property.managedReferenceValue == null)
            {
                // Draw the label on the left
                Rect labelRect = new Rect(position.x, position.y, EditorGUIUtility.labelWidth, position.height);
                EditorGUI.LabelField(labelRect, label);

                // Draw a dropdown button on the right
                Rect buttonRect = new Rect(position.x + EditorGUIUtility.labelWidth, position.y, position.width - EditorGUIUtility.labelWidth, position.height);

                if (GUI.Button(buttonRect, "Select Modifier Type..."))
                {
                    ShowTypeMenu(property);
                }
            }
            else
            {
                // 2. If it exists, draw it normally (Standard Unity UI)
                EditorGUI.PropertyField(position, property, label, true);
            }

            EditorGUI.EndProperty();
        }

        private void ShowTypeMenu(SerializedProperty property)
        {
            GenericMenu menu = new GenericMenu();

            // Add "Add Modifier" option
            menu.AddItem(new GUIContent("Flat (+10)"), false, () =>
            {
                ApplyType(property, new AddModifier());
            });

            // Add "Mult Modifier" option
            menu.AddItem(new GUIContent("Percentage (+10%)"), false, () =>
            {
                ApplyType(property, new MultModifier());
            });

            menu.ShowAsContext();
        }

        private void ApplyType(SerializedProperty property, StatModifier newMod)
        {
            // Assign the new object to the serialized property
            property.managedReferenceValue = newMod;
            property.serializedObject.ApplyModifiedProperties();
        }
    }
}