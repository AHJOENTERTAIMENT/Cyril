using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHJO.Guri {

    public class Node : IHeapItem<Node> {

        public NodeType nodeType;

        public Vector3 worldPosition;
        public IntVector2 gridPosition;

        public int gCost;
        public int hCost;

        int heapIndex;

        public Node parent;

        public int fCost { get { return gCost + hCost; } }

        public bool IsWalkable { get { return nodeType.walkable; } }

        #region Constructors

        public Node (NodeType nodeType, Vector3 worldPosition, IntVector2 gridPosition) {
            this.nodeType = nodeType;
            this.worldPosition = worldPosition;
            this.gridPosition = gridPosition;

            this.heapIndex = 0;
            this.gCost = 0;
            this.hCost = 0;
        }

        #endregion Constructors

        #region IHeapItem

        public int HeapIndex {
            get {
                return heapIndex;
            }
            set {
                heapIndex = value;
            }
        }

        public int CompareTo (Node nodeToCompare) {
            int compare = fCost.CompareTo (nodeToCompare.fCost);
            if (compare == 0) {
                compare = hCost.CompareTo (nodeToCompare.hCost);
            }
            return -compare;
        }

        #endregion IHeapItem

        public static int GetDistanceSqrt (Node a, Node b) {
            int distX = a.gridPosition.x - b.gridPosition.x;
            int distY = a.gridPosition.y - b.gridPosition.y;
            return distX * distX + distY * distY;
        }

        public static int GetDistanceAbsolute (Node a, Node b) {
            int distX = a.gridPosition.x - b.gridPosition.x;
            int distY = a.gridPosition.y - b.gridPosition.y;
            return Mathf.RoundToInt (Mathf.Sqrt (distX * distX + distY * distY));
        }

        public static int GetDistanceManhattan (Node a, Node b) {
            int dstX = a.gridPosition.x - b.gridPosition.x;
            int dstY = a.gridPosition.y - b.gridPosition.y;

            if (dstX < 0) dstX = -dstX;
            if (dstY < 0) dstY = -dstY;

            return Pathfinding.DIAGONAL_COST * dstX + Pathfinding.DEFAULT_COST * (dstX > dstY ? dstX - dstY : dstY - dstX);
        }
    }

}

