using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace AHJO.Guri {

    public abstract class Pathfinding {

        internal const int DIAGONAL_COST = 1414, DEFAULT_COST = 1000;

        protected Grid2D grid;
        public LogLevel logLevel;

        public abstract void TryFindPath (PathRequest pathRequest);

        public T SetGrid<T> (Grid2D grid) where T : Pathfinding {
            this.grid = grid;
            return (T) this;
        }

        protected Vector3[] RetracePath (Node startNode, Node endNode) {
            List<Node> path = new List<Node> ();
            Node currentNode = endNode;

            while (currentNode != startNode) {
                path.Add (currentNode);
                currentNode = currentNode.parent;
            }
            Vector3[] waypoints = SimplifyPath (path);
            Array.Reverse (waypoints);
            return waypoints;
        }

        protected Vector3[] SimplifyPath (List<Node> path) {
            List<Vector3> waypoints = new List<Vector3> ();
            Vector2 directionOld = Vector2.zero;

            for (int i = 1; i < path.Count; i++) {
                Vector2 directionNew = new Vector2 (path[i - 1].gridPosition.x - path[i].gridPosition.x, path[i - 1].gridPosition.y - path[i].gridPosition.y);
                if (directionNew != directionOld) {
                    waypoints.Add (path[i].worldPosition);
                }
                directionOld = directionNew;
            }
            return waypoints.ToArray ();
        }
    }

}

