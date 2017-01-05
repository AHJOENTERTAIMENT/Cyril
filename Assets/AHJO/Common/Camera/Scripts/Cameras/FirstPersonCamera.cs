using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHJO.Cameras {

    public class FirstPersonCamera : CameraSystemBase {

        [Header ("First Person Overlay")]
        public Camera overlayCamera;

        void Update () {
            UpdatePosAndRot ();
        }

        #region Unity_Editor

        void OnValidate () {
            if (followTarget) {
                UpdatePosAndRot ();
            }

            if (mainCamera && overlayCamera && mainCamera.fieldOfView != fieldOfView || overlayCamera.fieldOfView != fieldOfView) {
                mainCamera.fieldOfView = fieldOfView;
                overlayCamera.fieldOfView = fieldOfView;
            }
            if (overlayCamera) {
                overlayCamera.farClipPlane = 10f;
                overlayCamera.nearClipPlane = 0.05f;
            }
        }

        #endregion Unity_Editor
    }

}