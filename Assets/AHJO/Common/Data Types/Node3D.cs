using UnityEngine;
using System.Collections.Generic;

namespace AHJO { 

    [System.Serializable]
	public struct Node3D {

        public float x, y, z;
        public float rx, ry, rz, rw;

        public Vector3 Position { get { return new Vector3 (x, y, z); } }
        public Quaternion Rotation { get { return new Quaternion (rx, ry, rx, rw); } }

        #region Constructors

        public Node3D (Vector3 position, Quaternion rotation) {
            this.x = position.x;
            this.y = position.y;
            this.z = position.z;

            this.rx = rotation.x;
            this.ry = rotation.y;
            this.rz = rotation.z;
            this.rw = rotation.w;
        }

        public Node3D (int x, int y, int z) {
            this.x = x;
            this.y = y;
            this.z = z;

            this.rx = 0;
            this.ry = 0;
            this.rz = 0;
            this.rw = 0;
        }

        public Node3D (Node3D node, Quaternion rotation) {
            this.x = node.x;
            this.y = node.y;
            this.z = node.z;

            this.rx = rotation.x;
            this.ry = rotation.y;
            this.rz = rotation.z;
            this.rw = rotation.w;
        }

        #endregion Constructors
    }

}