using UnityEngine;
using System.Collections.Generic;

namespace AHJO {

    [System.Serializable]
    public struct GridCoord {

        public byte x, y;

        #region constructors
        public GridCoord (byte x, byte y) {
            this.x = x;
            this.y = y;
        }

        public GridCoord (int x, int y) {
            this.x = (byte) x;
            this.y = (byte) y;
        }
        #endregion constructors

        #region operators
        public static explicit operator IntVector2 (GridCoord gc) {
            return new IntVector2 (gc.x, gc.y);
        }

        public static explicit operator GridCoord (IntVector2 iv) {
            return new GridCoord (iv.x, iv.y);
        }

        public static GridCoord operator +(GridCoord a, GridCoord b) {
            return new GridCoord (a.x + b.x, a.y + b.y);
        }

        public static GridCoord operator - (GridCoord a, GridCoord b) {
            return new GridCoord (a.x - b.x, a.y - b.y);
        }
        #endregion operators
    }

}
