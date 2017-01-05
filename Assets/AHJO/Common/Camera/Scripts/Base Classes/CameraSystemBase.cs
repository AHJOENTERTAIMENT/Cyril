using UnityEngine;
using System.Collections;

namespace AHJO.Cameras {

    public class CameraSystemBase : MonoBehaviour {

        public enum FollowMode { Disabled = 0, Position = 1, Rotation = 2, Both = 3}

        [ShowOnly] public int managedID;

        [Tooltip ("The Camera used for Raycasting and such.")]
        public Camera mainCamera;
        public Transform lookNode;
        
        [Header ("Camera Settings")]
        [Range (60, 180)]
        public float fieldOfView;
        public float xRotMin, xRotMax;

        [Header ("Follow Settings")]
        public Transform followTarget;
        public FollowMode followMode = FollowMode.Both;
        public Vector3 followOffset;
        public float followSmoothing;
        
        [Header ("Look Settings")]
        public Transform lookTarget;
        public float lookSmoothing;

        public Vector3 crosshairPosition;

        public CameraBehaviourBase[] cameraBehaviours;

        // Replace with the managed Update...?
        protected virtual void LateUpdate () {
            for (int i = 0; i < cameraBehaviours.Length; i++) {
                cameraBehaviours[i].UpdateBehaviour ();
            }
            if (followTarget) {
                if (lookTarget == null && (followMode & FollowMode.Rotation) == FollowMode.Rotation) {
                    transform.rotation = Quaternion.Euler (transform.eulerAngles.x, followTarget.eulerAngles.y, 0);
                }
                if ((followMode & FollowMode.Position) == FollowMode.Position) {
                    transform.position = followTarget.position + followTarget.TransformDirection (followOffset);
                }
            }
            if (lookTarget) {
                transform.rotation = Quaternion.Slerp (Quaternion.LookRotation (lookTarget.position - transform.position, transform.up), transform.rotation, Time.deltaTime * lookSmoothing);
            }
        }

        internal void SetActive (bool active) {
            this.gameObject.SetActive (active);
        }

        public void SetFollowTarget (Transform target) {
            followTarget = target;
        }

        protected void UpdatePosAndRot () {
            transform.position = followTarget.position + followTarget.InverseTransformVector (followOffset);
            transform.rotation = followTarget.transform.rotation;
        }

        protected virtual void OnValidate () {
            if (mainCamera == null) mainCamera = GetComponent<Camera> ();      
        }

    }

}

