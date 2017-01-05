using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHJO.Guri {

    public class Grid2D : GridBase {

        public GridPlane gridPlane = GridPlane.XZ;

        public Vector2 gridWorldSize;
        
        IntVector2 gridSize;

        Node[,] grid;

        public sealed override int MaxGridSize {
            get {
                return gridSize.x * gridSize.y;
            }
        }

        public sealed override List<Node> GetNeighbours (Node node) {
            var neighbours = new List<Node> (8);

            for (int x = -1; x <= 1; x++) {
                for (int y = -1; y <= 1; y++) {
                    if (x == 0 && y == 0) continue;

                    int checkX = node.gridPosition.x + x;
                    int checkY = node.gridPosition.y + y;

                    if (checkX >= 0 && checkX < gridSize.x && checkY >= 0 && checkY < gridSize.y) {
                        neighbours.Add (grid[checkX, checkY]);
                    }
                }
            }
            return neighbours;
        }

        public sealed override void DrawDebugMap () {
            // TODO:
            // Implement fancier stuff :D
            drawDebug = true;
        }

        public sealed override Node ClosestNodeFromWorldPoint (Vector3 worldPoint) {
            float percentX = Mathf.Clamp01 ((worldPoint.x + gridWorldSize.x / 2) / gridWorldSize.x);
            float percentY = Mathf.Clamp01 (((gridPlane == GridPlane.XZ ? worldPoint.z : worldPoint.y) + gridWorldSize.y / 2) / gridWorldSize.y);

            int x = Mathf.RoundToInt ((gridSize.x - 1) * percentX);
            int y = Mathf.RoundToInt ((gridSize.y - 1) * percentY);

            return grid[x, y];
        }

        public sealed override void CreateGrid () {
            if (requiresInitialization) {
                InitializeGrid ();
            }
            grid = new Node[gridSize.x, gridSize.y];

            Vector3 worldPoint;
            Vector3 checkBoxDim = new Vector3 (nodeRadius, nodeRadius, nodeRadius);
            Vector3 v3_right = Vector3.right;

            Vector3 v3_foward = Vector3.forward;
            Vector3 v3_up = Vector3.up;
           
            for (int x = 0; x < gridSize.x; x++) {
                for (int z = 0; z < gridSize.y; z++) {
                    worldPoint = worldBottomLeft + (gridPlane == GridPlane .XZ ? v3_foward : v3_up) * (x * nodeDiameter + nodeRadius) + v3_right * (z * nodeDiameter + nodeRadius);
                    bool walkable = !(Physics.CheckSphere (worldPoint, nodeRadius * 0.95f, unwalkableMask));
                    grid[x, z] = new Node (walkable ? NodeType.Walkable : NodeType.Blocked, worldPoint, new IntVector2 (x, z));
                }
            }            
        } // End Create Grid

        protected sealed override void InitializeGrid () {
            requiresInitialization = false;

            nodeDiameter = nodeRadius * 2f;
            gridSize = new IntVector2 (
               Mathf.RoundToInt (gridWorldSize.x / nodeDiameter),
               Mathf.RoundToInt (gridWorldSize.y / nodeDiameter)
               );

            worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2f - Vector3.forward * gridWorldSize.y /2f;
        } // End Initialize Grid

        #region Unity Functions

        void OnDrawGizmos () {
            if (!drawDebug || grid == null ) {
                return;
            }

            foreach (Node n in grid) {
                Gizmos.color = n.IsWalkable ? Color.white : Color.red;
                Gizmos.DrawCube (n.worldPosition, Vector3.one * (nodeDiameter - .1f));
            }
        }

        #endregion Unity Functions

        public enum GridPlane {
            XZ,
            XY
        }
    }

}
