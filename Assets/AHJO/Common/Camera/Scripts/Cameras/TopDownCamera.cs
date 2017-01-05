using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHJO.Cameras {

    public class TopDownCamera : CameraSystemBase {

        public enum TopDownMode { SimpleFollow, FollowTarget, LookAtTarget, Hybrid }

        [Header ("Mouse Settings")]
        public TopDownMode topDownMode;

        [SerializeField]
        [Range (0, 1)]
        private float amountX;
        [SerializeField]
        [Range (0, 1)]
        private float amountY;

        // For these you could use the Ground plane of the world, or then go ahead and create a camera facing "MouseInput" layer or something.
        [SerializeField]
        private LayerMask mouseRayMask;

        Vector3 mouseDistanceFromTarget = Vector3.zero;
        Vector3 currentOffset = Vector3.zero;

        protected override void LateUpdate () {
            switch (topDownMode) {
            case TopDownMode.SimpleFollow:
                if (followTarget)
                    transform.position = Vector3.Lerp (transform.position, followTarget.position + followOffset, Time.fixedDeltaTime * (1 + followSmoothing));
                break;
            case TopDownMode.LookAtTarget:
                if (followTarget) transform.position = followTarget.position + followOffset;
                if (lookTarget) lookNode.LookAt (lookTarget.position, Vector3.forward);
                break;
            case TopDownMode.FollowTarget:
                RaycastHit hitInfo;
                Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

                if (Physics.Raycast (ray, out hitInfo, 100f, mouseRayMask.value)) {
                    mouseDistanceFromTarget = (hitInfo.point - followTarget.position);
                } else {
                    mouseDistanceFromTarget = Vector3.zero;
                }

                transform.position = Vector3.Lerp (transform.position,
                    followTarget.position + followOffset + new Vector3 (mouseDistanceFromTarget.x * amountX, 0, mouseDistanceFromTarget.z * amountY),
                    Time.fixedDeltaTime * (1 + followSmoothing));
                break;
            case TopDownMode.Hybrid:
                break;
            }
        }

        Vector3 GetUpdatedLookTarget () {
            return lookTarget.position;
        }

    }

}
