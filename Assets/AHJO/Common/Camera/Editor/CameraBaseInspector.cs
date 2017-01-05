using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AHJO.Cameras {

    public class CameraBaseInspector<T> : Editor where T : CameraSystemBase { 

        protected bool followSettings;
        protected new T target;

        public override void OnInspectorGUI () {
            this.target = base.target as T;

            if (followSettings = EditorGUILayout.Foldout (followSettings, "Base Settings", true, EditorStyles.boldLabel)) {
                target.followTarget = EditorGUILayout.ObjectField ("Follow Target", target.followTarget, typeof (Transform), true) as Transform;
                target.followOffset = EditorGUILayout.Vector3Field ("Follow Position", target.followOffset);
                target.followSmoothing = EditorGUILayout.FloatField ("Follow Smoothing", target.followSmoothing);
                target.followMode = (CameraSystemBase.FollowMode) EditorGUILayout.EnumPopup ("Follow Mode", target.followMode);
            }
        }

    }

}
