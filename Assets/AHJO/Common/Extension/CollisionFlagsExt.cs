using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHJO {

    public static class CollisionFlagsExt {
        // Apparently these don't work? :(
        /*
        public static void SetFlag (this CollisionFlags colFlag, CollisionFlags flagToAdd) {
            colFlag |= flagToAdd;
        }

        public static void RemoveFlag (this CollisionFlags colFlag, CollisionFlags flagToRemove) {
            colFlag ^= flagToRemove;
        }
        */
        public static bool HasFlag (this CollisionFlags colFlag, CollisionFlags flagToCheck) {
            return (colFlag & flagToCheck) == flagToCheck;
        }
    }

}
