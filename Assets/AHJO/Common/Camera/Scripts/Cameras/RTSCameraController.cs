using UnityEngine;
using System.Collections.Generic;

namespace AHJO.Cameras { 

	public class RTSCameraController : MonoBehaviour {

        [Header ("Moving")]
        public float moveSpeed;

        [Header ("Zoom")]
        public float zoomMax;
        public float zoomMin;
        public float zoomSpeed;
        public float maxZoomPerFrame;

        [Header ("Zoom Rotation")]
        public bool useZoomRot;
        public float angleAtMax;
        public float angleAtMin;

        [Header ("Mouse")]
        public bool screenEdgeMovement;
        public bool middleMouseRot;

        [Header ("Offset")]
        public Vector3 defaultOffset;

        Vector3 newPosition;
        float x, y, z;
        float zoomCurrent;

        float yRot, xRot;
        float rotScale;

        Vector2 lastMousePos;

        float mouseX, mouseY;

        void Update () {
            MoveCamera ();
        }

        void MoveCamera () {
            // Rotation
            yRot = 0;
            xRot = 0;
            if (Input.GetKey (KeyCode.Q)) {
                yRot -= 4;
            }
            if (Input.GetKey (KeyCode.E)) {
                yRot += 4;
            }

            if (middleMouseRot && Input.GetMouseButton (2)) {
                mouseX = Input.mousePosition.x;
                mouseY = Input.mousePosition.y;

                yRot = (lastMousePos.x < mouseX) ? 1 : (lastMousePos.x > mouseX) ? -1 : 0;
                xRot = (lastMousePos.y < mouseY) ? 1 : (lastMousePos.y > mouseY) ? -1 : 0;

                rotScale = Mathf.Clamp (((yRot > 0) ? lastMousePos.x - mouseX : mouseX - lastMousePos.x), -100, 100);
                yRot *= rotScale;
                rotScale = Mathf.Clamp (((yRot > 0) ? lastMousePos.y - mouseY : mouseY - lastMousePos.y), -100, 100);
                xRot *= rotScale;
            }

            lastMousePos = Input.mousePosition;

            // Calculate and apply camera rotation relative to a arbitrary point in front of it.
            transform.RotateAround (transform.position + transform.forward * 10f, Vector3.up, yRot * 10f * Time.deltaTime);

            // Movement and zoom
            x = Input.GetAxisRaw ("Horizontal") * moveSpeed;
            y = Input.GetAxisRaw ("Vertical") * moveSpeed;

            //Check for screen edge movement
            if (screenEdgeMovement) {
                if (x < 0.1f && x > -0.1f) {
                    x = (Input.mousePosition.x < 20) ? -moveSpeed : (Input.mousePosition.x > Screen.width - 20) ? moveSpeed : 0;
                }
                if (y < 0.1f && y > -0.1f) {
                    y = (Input.mousePosition.y < 20) ? -moveSpeed : (Input.mousePosition.y > Screen.height - 20) ? moveSpeed : 0;
                }
            }

            // Get zoom for this frame
            z = Input.GetAxisRaw ("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime;

            // Apply per frame zoom constraint to prevent weird behaviour on frames that take long to calculate
            if (z > maxZoomPerFrame) {
                z = maxZoomPerFrame;
            } else if (z < -maxZoomPerFrame) {
                z = -maxZoomPerFrame;
            }

            zoomCurrent += z;

            // Check for zoom boundaries
            if (zoomCurrent > zoomMax) {
                zoomCurrent = zoomMax;
                z = 0;
            } else if (zoomCurrent < zoomMin) {
                zoomCurrent = zoomMin;
                z = 0;
            }

            newPosition = new Vector3 (
                x * Time.deltaTime,
                0,
                y * Time.deltaTime);

            // Apply zoom relative to camera
            newPosition = transform.TransformDirection (new Vector3 (0f, 0f, z)) + newPosition;

            if (newPosition.x != transform.position.x || newPosition.y != transform.position.y || newPosition.z != transform.position.z) {
                zoomCurrent += z;
                transform.position += newPosition;
            }
        }
	}
	
}
