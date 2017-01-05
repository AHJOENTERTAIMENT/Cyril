using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using AHJO.Mono;

namespace AHJO.MonoEditor {

    [CustomPropertyDrawer (typeof (InvetorySlot))]
    public class InventorySlotDrawer : PropertyDrawer {

        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
            // Overrides the default logic.
            EditorGUI.BeginProperty (position, label, property);

            // Draw label
            position = EditorGUI.PrefixLabel (position, GUIUtility.GetControlID (FocusType.Passive), new GUIContent ("Slot " + property.FindPropertyRelative ("slotID").intValue));

            // Don't make child fields be indented
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            var itemRect = new Rect (position.x, position.y, position.width - 50, position.height);
            var amountRect = new Rect (position.x + position.width - 45, position.y, 45, position.height);

            EditorGUI.PropertyField (itemRect, property.FindPropertyRelative ("itemStack").FindPropertyRelative ("stackItem"), GUIContent.none);
            EditorGUI.PropertyField (amountRect, property.FindPropertyRelative ("itemStack").FindPropertyRelative ("count"), GUIContent.none);

            // Set indent back to what it was
            EditorGUI.indentLevel = indent;

            //EditorGUI.PropertyField

            EditorGUI.EndProperty ();
        }
    }

}
