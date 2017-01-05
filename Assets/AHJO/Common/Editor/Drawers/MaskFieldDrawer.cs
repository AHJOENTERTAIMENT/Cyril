using UnityEngine;
using UnityEditor;

namespace AHJO {

    [CustomPropertyDrawer (typeof (MaskFieldAttribute))]
    public class MaskFieldDrawer : PropertyDrawer {

        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
            property.intValue = EditorGUI.MaskField (position, label, property.intValue, property.enumDisplayNames);
        }
    }
}