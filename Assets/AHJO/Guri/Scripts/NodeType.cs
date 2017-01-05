using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHJO.Guri {

    public class NodeType {

        private static NodeType _walkable = new NodeType (true);
        public static NodeType Walkable { get { return _walkable; } }

        private static NodeType _blocked = new NodeType (false);
        public static NodeType Blocked { get { return _blocked; } }

        public float movementCost;
        public bool walkable;

        public NodeConnectRule connectRule;

        public NodeType () {
            walkable = true;
        }

        public NodeType (bool walkable) {
            this.walkable = walkable;
        }

        [System.Flags]
        public enum NodeConnectRule {
            // Diagonals are allowed by their neighbourhs, i.e. if North and East are allowed then North-East is also.
            North = 1,
            East = 2,
            South = 4,
            West = 8,
            Up = 16,
            Down = 32
        }      
    }

}