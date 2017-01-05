using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace AHJO { 

    [System.Serializable]
	public struct Line {

        public Vector3 pointA, pointB;

        public Line (Vector3 pointA, Vector3 pointB) {
            this.pointA = pointA;
            this.pointB = pointB;
        }

#if UNITY_EDITOR
        public static void DrawLineOnScene (Line line) {
            Gizmos.DrawLine (line.pointA, line.pointB);
        }

        public static void DrawLineOnScene (Line line, Color color) {
            Gizmos.color = color;
            Gizmos.DrawLine (line.pointA, line.pointB);
        }
#endif

    }
	
}
