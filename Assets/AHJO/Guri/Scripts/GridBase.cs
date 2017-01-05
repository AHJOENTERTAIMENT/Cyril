using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHJO.Guri {

    public abstract class GridBase : MonoBehaviour {

        protected bool requiresInitialization;

        protected Vector3 worldBottomLeft;

        public abstract int MaxGridSize { get; }

        public float nodeRadius;
        protected float nodeDiameter;

        public LayerMask unwalkableMask;

        public bool drawDebug;

        public abstract Node ClosestNodeFromWorldPoint (Vector3 worldPoint);

        public abstract void DrawDebugMap ();

        public abstract void CreateGrid ();

        public abstract List<Node> GetNeighbours (Node node);

        protected abstract void InitializeGrid ();

        void Start () {
            requiresInitialization = true;
            CreateGrid ();
        }


    }

}

