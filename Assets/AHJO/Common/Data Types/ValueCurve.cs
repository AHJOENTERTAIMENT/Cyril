using UnityEngine;
using System.Collections.Generic;

namespace AHJO { 

	public struct ValueCurve {

        private float[] points;
        private float[] values;

        public ValueCurve (float[] values) {
            this.points = new float[values.Length];
            this.values = values;
            for (int i = 0; i < values.Length; i++) {
                this.points[i] = i;
            }
        }

        public ValueCurve (float[] points, float[] values) {
            this.points = points;
            this.values = values;
        }

        public ValueCurve (int numPoints, float pointSpread, float[] pointValues, float startValue = 0) {

            if (pointSpread == 0) {
                this.points = new float[1];
                this.values = new float[1];

                this.points[0] = 0;
                this.values[0] = pointValues[0];
            } else {
                this.points = new float[numPoints];
                this.values = pointValues;

                if (startValue == 0) {
                    for (int i = 0; i < numPoints; i++) {
                        this.points[i] = i * pointSpread;
                    }
                } else {
                    for (int i = 0; i < numPoints; i++) {
                        this.points[i] = i * pointSpread + startValue;
                    }
                }
            }           
            
        }

        public float GetValue (float point) {
            for (int i = 0; i < values.Length; i++) {
                if (points[i] > 0) {
                    continue;
                } else {
                    if (i == 0 || i + 1 == values.Length) {
                        return point;
                    }
                    return values[i] + values[i + 1] - values[i] * point / points[i];
                }
            }
            return point;
        }

    }
	
}
