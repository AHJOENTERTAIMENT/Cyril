using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHJO.Guri {

    public class NavigationAgent : MonoBehaviour {

        public IntVector3 gridPosition;

        public Transform target;

        private GridBase grid;

        bool d;

        void Start () {
            grid = PathfindingManager.Instance.grid;
            d = true;
        }

        void Update () {
            var n = grid.ClosestNodeFromWorldPoint (transform.position);
            gridPosition = new IntVector3 (n.gridPosition.x, 0, n.gridPosition.y);

            if (target && d) {
                d = false;
                PathfindingManager.Instance.FindPath (transform.position, target.position, PFDONE);
            }
        }

        void PFDONE (PathfindResult pathfindResult) {
            d = true;
            Debug.Log ("DONE: " + pathfindResult.pathFound);
        }
    }

}

