using UnityEngine;

namespace AHJO {

    [System.Serializable]
    public struct IntVector2 {

        public static IntVector2 Zero { get { return new IntVector2 (0, 0); } }
        public static IntVector2 One { get { return new IntVector2 (1, 1); } }

        public static IntVector2 Left { get { return new IntVector2 (-1, 0); } }
        public static IntVector2 Right { get { return new IntVector2 (1, 0); } }

        public static IntVector2 Up { get { return new IntVector2 (0, 1); } }
        public static IntVector2 Down { get { return new IntVector2 (0, -1); } }

        public readonly int x, y;

        #region constructors
        public IntVector2 (int x, int y) {
            this.x = x;
            this.y = y;
        }

        public IntVector2 (float x, float y) {
            this.x = (int) x;
            this.y = (int) y;
        }
        #endregion constructors

        #region overrides
        public override string ToString () {
            var s = string.Concat ("( ", x, " ,", y, " )");
            return s;
        }
        #endregion overrides

        #region Implicit Operators
        public static IntVector2 operator + (IntVector2 a, IntVector2 b) {
            var temp = new IntVector2 (a.x + b.x, a.x + b.x);
            return temp;
        }
        #endregion operators

        #region Explicit Operators
        public static explicit operator Vector2 (IntVector2 intVector) {
            var temp = new Vector2 (intVector.x, intVector.y);
            return temp;
        }

        public static explicit operator IntVector2 (Vector2 intVector) {
            var temp = new IntVector2 (
                (int) (intVector.x),
                (int) (intVector.y)
                );
            return temp;
        }
        #endregion Explicit Operators
    }

    [System.Serializable]
    public struct IntVector3 {

        public static IntVector3 Zero { get { return new IntVector3 (0, 0, 0); } }
        public static IntVector3 One { get { return new IntVector3 (1, 1, 1); } }

        public static IntVector3 Left { get { return new IntVector3 (-1, 0, 0); } }
        public static IntVector3 Right { get { return new IntVector3 (1, 0, 0); } }

        public static IntVector3 Up { get { return new IntVector3 (0, 1, 0); } }
        public static IntVector3 Down { get { return new IntVector3 (0, -1, 0); } }

        public static IntVector3 Forward { get { return new IntVector3 (0, 0, 1); } }
        public static IntVector3 Backward { get { return new IntVector3 (0, 0, -1); } }

        public readonly int x, y, z;

        #region Contructors

        public IntVector3 (int x, int y, int z) {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public IntVector3 (float x, float y, float z) {
            this.x = (int) x;
            this.y = (int) y;
            this.z = (int) z;
        }

        #endregion Constructors

        #region Overrides
        public override string ToString () {
            var s = string.Concat ("( ", x, " ,", y, ", ", z, " )");
            return s;
        }
        #endregion Overrides

        #region Implicit Operators

        public static IntVector3 operator + (IntVector3 a, IntVector3 b) {
            // var temp = new IntVector3 (a.x + b.x, a.y + b.y, a.z + b.z);
            // return temp;
            return new IntVector3 (a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static IntVector3 operator / (IntVector3 v, int d) {
            // var temp = new IntVector3 (v.x / d, v.y / d, v.z / d);
            // return temp;
            return new IntVector3 (v.x / d, v.y / d, v.z / d);
        }

        #endregion Implicit Operators

        #region Explicit Operators

        public static explicit operator Vector3 (IntVector3 intVector) {
            var temp = new Vector3 (intVector.x, intVector.y, intVector.z);
            return temp;
        }

        public static explicit operator IntVector3 (Vector3 intVector) {
            var temp = new IntVector3 (
                (int) (intVector.x),
                (int) (intVector.y),
                (int) (intVector.z)
                );
            return temp;
        }

        public static explicit operator IntVector3 (IntVector2 iv2) {
            var temp = new IntVector3 (
                iv2.x,
                iv2.y,
                0);
            return temp; 
        }

        #endregion Explicit Operators

        /// <summary>
        /// Return a Vector with all axis pointing towards positive, but with the same overall length.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static IntVector3 GetSizeVector (IntVector3 vector) {
            // int x = (vector.x > 0) ? vector.x : -vector.x;
            // int y = (vector.y > 0) ? vector.y : -vector.y;
            // int z = (vector.z > 0) ? vector.z : -vector.z;
            // return new IntVector3 (x, y, z);

            return new IntVector3 (
                (vector.x > 0) ? vector.x : -vector.x,
                (vector.y > 0) ? vector.y : -vector.y,
                (vector.z > 0) ? vector.z : -vector.z
                );
        }
    }
}