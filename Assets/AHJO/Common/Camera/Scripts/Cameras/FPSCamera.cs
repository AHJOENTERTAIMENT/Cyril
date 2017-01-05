using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHJO.Cameras {

    [ExecuteInEditMode]
    public class FPSCamera : CameraSystemBase {

        public bool useCrosshair;
        public CrosshairControl crosshairRect;
        public Texture defaultCrosshair;
        public CrosshairMode[] additionalModes;
        private LayerMask combinedMask;

        private Texture _crosshairTexture;

        public void SetCrosshairTexture (string modeName, Texture texture) {
            for (int i = 0; i < additionalModes.Length; i++) {
                if (additionalModes[i].name.Equals (modeName)) {
                    additionalModes[i] = new CrosshairMode (modeName, texture, additionalModes[i].interactionMask);
                    return;
                }
            }
        }

        public void SetCrosshairTextures (Texture[] texture) {
            int _l = texture.Length < additionalModes.Length ? additionalModes.Length : texture.Length;
            for (int i = 0; i < _l; i++) {
                additionalModes[i] = new CrosshairMode (additionalModes[i].name, texture[i], additionalModes[i].interactionMask);               
            }
        }

        Ray ray;
        RaycastHit rayHit;
        void Update () {
            if (Physics.Raycast (ray, out rayHit, combinedMask)) {
                for (int i = 1; i < additionalModes.Length; i++) {

                }
            } else {
                // Default
                _crosshairTexture = defaultCrosshair;
            }
        }

        void OnGUI () {
            GUI.DrawTexture (
                new Rect (
                    Screen.width / 2 - crosshairRect.width / 2,
                    Screen.height / 2 - crosshairRect.height / 2,
                    crosshairRect.width,
                    crosshairRect.height),
                _crosshairTexture);
        }

#if UNITY_EDITOR
        void OnValidate () {
            
        }
#endif // UNITY_EDITOR
        void UpdateInteractionMask () {
            for (int i = 0; i < additionalModes.Length; i++) {
                combinedMask |= additionalModes[i].interactionMask;
            }
        }

        [System.Serializable]
        public struct CrosshairMode {
            public string name;
            public Texture texture;
            public LayerMask interactionMask;

            public CrosshairMode (string name, Texture texture, LayerMask interactionMask) {
                this.name = name;
                this.texture = texture;
                this.interactionMask = interactionMask;
            }
        }

        [System.Serializable]
        public struct CrosshairControl {
            [Tooltip ("In percents of screen resolution.")]
            public Vector2 crosshairPosition;
            public uint width, height;

            public void ValidateControl () {
                if (crosshairPosition.x > 1 || crosshairPosition.x < 0 || crosshairPosition.y > 1 || crosshairPosition.y < 0) {
                    crosshairPosition = new Vector2 (
                        crosshairPosition.x > 1 ? 1 : crosshairPosition.x < 0 ? 0 : crosshairPosition.x,
                        crosshairPosition.y > 1 ? 1 : crosshairPosition.y < 0 ? 0 : crosshairPosition.y
                        );
                }
            }
        }

    }

}

