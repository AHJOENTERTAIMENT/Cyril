using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using AHJO.Enta;

namespace AHJO.EntaEditor {

    [CustomEditor (typeof (ActorBehaviour))]
    public class ActorBehaviourInspector : Editor {

        protected ActorBehaviour actorBehaviour;

        void OnEnable () {
            actorBehaviour = target as ActorBehaviour;
            actorBehaviour.hideFlags |= HideFlags.HideInInspector;
        }

        public override void OnInspectorGUI () {
            actorBehaviour.updatePriority = (byte) EditorGUILayout.IntField ("Update Priority", actorBehaviour.updatePriority);
            DrawDefaultInspector ();
        }
    }

}

