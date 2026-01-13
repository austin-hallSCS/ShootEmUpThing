using UnityEngine;
using UnityEditor;
using WizardGame.Spells;

namespace WizardGame.Editor
{
    [CustomPropertyDrawer(typeof(SpellLevelData))]
    public class SpellLevelDataDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // 1. Find the "Level" property inside this class
            // Since you used [field: SerializeField], the internal name is confusing ("<Level>k__BackingField").
            // We try the backing field name first, then the standard name just in case you change it later.
            var levelProp = property.FindPropertyRelative("<Level>k__BackingField")
                            ?? property.FindPropertyRelative("Level");

            // 2. Create the new name
            string newLabel = "Level ?";
            if (levelProp != null)
            {
                newLabel = $"Level {levelProp.intValue}";
            }

            // 3. Draw the property with the NEW label
            // The 'true' at the end tells Unity to draw all the children (Modifiers list, etc.) normally
            EditorGUI.PropertyField(position, property, new GUIContent(newLabel), true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            // Ensure the height calculates correctly for the children elements
            return EditorGUI.GetPropertyHeight(property, label, true);
        }
    }
}