using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AHJO.Guri.Editors {

    [CustomEditor (typeof (NavigationAgent))]
    public class NavigationAgentInspector : Editor {

        public sealed override void OnInspectorGUI () {
            var t = (NavigationAgent) target;

            EditorGUILayout.BeginHorizontal ();
            EditorGUILayout.PrefixLabel ("Grid Position");
            var x = EditorGUILayout.IntField (t.gridPosition.x);
            var y = EditorGUILayout.IntField (t.gridPosition.y);
            var z = EditorGUILayout.IntField (t.gridPosition.z);
            EditorGUILayout.EndHorizontal ();

            t.gridPosition = new IntVector3 (x, y, z);

            t.target = EditorGUILayout.ObjectField ("Target", t.target, typeof (Transform), true) as Transform;
        }

    }

}

