using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

using AHJO.Enta;

namespace AHJO.EntaEditor {

    [CustomEditor (typeof (Entity))]
    public class EntityInspector : Editor {

        private AttributeList attributeList;
        private Entity t_entity;

        void OnEnable () {
            t_entity = target as Entity;
            attributeList = new AttributeList (serializedObject, serializedObject.FindProperty ("entityAttributes"), true, true, true, true);
        }

        public override void OnInspectorGUI () {
            EditorGUILayout.Space ();
            serializedObject.Update ();
            attributeList.DoLayoutList ();
            serializedObject.ApplyModifiedProperties ();
        }

        protected class AttributeList : ReorderableList {

            public AttributeList (SerializedObject serializedObject, SerializedProperty serializedProperty, bool b, bool b1, bool b2, bool b3) 
                : base (serializedObject, serializedProperty, b, b1, b2, b3) {

                this.drawHeaderCallback = OnDrawheader;
                this.drawElementCallback = OnDrawElement;
                this.onAddDropdownCallback = OnAddDropdown;
            }

            void OnDrawheader (Rect rect) {
                EditorGUI.LabelField (rect, "Entity Attributes");
            }

            void OnDrawElement (Rect rect, int index, bool isActive, bool isFocused) {

            }

            void OnAddDropdown (Rect buttonRect, ReorderableList list) {

            }

        }
    }

}

