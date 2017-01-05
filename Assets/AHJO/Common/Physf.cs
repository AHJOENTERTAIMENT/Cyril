using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHJO {

    public static class Physf {

        /// <summary>
        /// s = displacement
        /// u = initial velocity
        /// v = final velocity
        /// a = acceleration
        /// t = time
        /// </summary>
        public static bool Displacement (out float s, float u, float? v, float? a, float t) {

            if (v != null) {
                s = (u + (int) v) / 2f * t;
                return true;
            } else if (a != null) {
                s =  u * t + ((int) a * t * t) / 2f;
                return true;
            }
            s = 0;
            return false;
        }

    }

}
