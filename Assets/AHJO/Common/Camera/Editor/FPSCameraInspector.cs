using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AHJO.Cameras {

    [CustomEditor (typeof (FPSCamera))]
    public class FPSCameraInspector : CameraBaseInspector<FPSCamera> {

        protected bool showFPS_Settings;

        public sealed override void OnInspectorGUI () {
            base.OnInspectorGUI ();

            if (showFPS_Settings = EditorGUILayout.Foldout (showFPS_Settings, "First Person Settings", true, EditorStyles.boldLabel)) {

            }
        }
    }
}