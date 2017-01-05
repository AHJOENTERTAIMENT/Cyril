using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using AHJO.Cyril;

namespace AHJO.CyrilEditor {

    [CustomEditor (typeof (KeyBindMapping))]
    public class KeyBindMappingInspector : Editor {

        private int numDefs;
        public static bool editEnabled;

        private bool showKeys, showAxis;

        new KeyBindMapping target;

        // Add a menu item called "Double Mass" to a Rigidbody's context menu.
        [MenuItem ("CONTEXT/KeyBindMapping/Toggle Edit")]
        static void DoubleMass (MenuCommand command) {
            editEnabled = !editEnabled;
        }


        void OnEnable () {
            target = (KeyBindMapping) base.target;
        }

        public override void OnInspectorGUI () {
           
            if (showKeys = GUILayout.Toggle (showKeys, "Input Keys", EditorStyles.boldLabel)) {
                for (int i = 0; i < target.inputDefinitions.Length; i++) {
                    EditorGUILayout.BeginHorizontal ();
                    if (editEnabled) {
                        EditorGUI.BeginChangeCheck ();
                        var s = EditorGUILayout.TextField (target.inputDefinitions[i].name);
                        if (EditorGUI.EndChangeCheck ()) target.inputDefinitions[i] = new KeyBindMapping.InputDefinition (s, target.inputDefinitions[i].key);
                    } else {
                        EditorGUILayout.LabelField (target.inputDefinitions[i].name);
                    }
                    var k = EditorGUILayout.EnumPopup (target.inputDefinitions[i].key);
                    EditorGUILayout.EndHorizontal ();
                }
            }
            
            if (showAxis = GUILayout.Toggle (showAxis, "Input Axis", EditorStyles.boldLabel)) {
                EditorGUILayout.LabelField ("Use Unity Axis for now...");
            }

        }

    }

}

