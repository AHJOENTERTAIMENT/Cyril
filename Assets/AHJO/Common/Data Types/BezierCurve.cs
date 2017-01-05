using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AHJO { 

	public struct BezierCurve {

        public Vector3[] points;

        public BezierCurve (Vector3[] points) {
            this.points = points;
        }

        public void Reset () {
            points = new Vector3[] {
                new Vector3(1f, 0f, 0f),
                new Vector3(2f, 0f, 0f),
                new Vector3(3f, 0f, 0f)
            };
        }

#if UNITY_EDITOR
        public static void DrawBezierCurve (BezierCurve curve) {

        }
#endif

    }
	
}
