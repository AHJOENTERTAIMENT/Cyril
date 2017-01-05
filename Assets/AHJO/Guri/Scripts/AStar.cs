using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

using UnityEngine;
using Debug = UnityEngine.Debug;

namespace AHJO.Guri {

    public class AStar : Pathfinding {

        public sealed override void TryFindPath (PathRequest pathRequest) {
            Debug.Log ("AStar called");

            Vector3[] waypoints = new Vector3[0];
            bool pathSuccess = false;

            Node startNode = grid.ClosestNodeFromWorldPoint (pathRequest.worldFrom);
            Node targetNode = grid.ClosestNodeFromWorldPoint (pathRequest.worldTo);


            if (startNode.IsWalkable && targetNode.IsWalkable) {
                Heap<Node> openSet = new Heap<Node> (PathfindingManager.Instance.grid.MaxGridSize);
                HashSet<Node> closedSet = new HashSet<Node> ();
                openSet.Add (startNode);

                while (openSet.Count > 0) {
                    Node currentNode = openSet.RemoveFirst ();
                    closedSet.Add (currentNode);

                    if (currentNode.Equals (targetNode)) {
                        pathSuccess = true;
                        break;
                    }

                    var nn = grid.GetNeighbours (currentNode);
                    Node cnn;
                    for (int i = 0; i < nn.Count; i++) {
                        cnn = nn[i];
                        if (!cnn.IsWalkable || closedSet.Contains (cnn)) {
                            continue;
                        }

                        int newMovementCostToNeighbour = currentNode.gCost + Node.GetDistanceManhattan (currentNode, cnn);
                        if (newMovementCostToNeighbour < cnn.gCost || !openSet.Contains (cnn)) {
                            cnn.gCost = newMovementCostToNeighbour;
                            cnn.hCost = Node.GetDistanceManhattan (cnn, targetNode);
                            cnn.parent = currentNode;

                            if (!openSet.Contains (cnn))
                                openSet.Add (cnn);
                        }
                    }
                }
            }
            if (pathSuccess) {
                Debug.Log ("asd");
                if (pathRequest.getSimplified) waypoints = RetracePath (startNode, targetNode);
            }
            pathRequest.callback (new PathfindResult (pathSuccess, waypoints));
            //requestManager.FinishedProcessingPath (waypoints, pathSuccess);
        }
        
    }

}

