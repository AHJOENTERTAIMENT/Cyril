using System.Collections;
using System.Collections.Generic;
using System.Threading;

using UnityEngine;

using AHJO.Cyril;

namespace AHJO.Guri {

    public delegate void PathfindingDone (PathfindResult pathfindResult);

    public class PathfindingManager : SingletonBehaviour<PathfindingManager>, ICyrilAwake {

        public GridBase grid;
        protected Pathfinding pathfinding;

        Thread pfThread;

        Queue<PathRequest> pathQueue = new Queue<PathRequest> ();
        PathRequest currentRequest;
        volatile bool findingPath, doPathfind;

        private ManualResetEvent mre = new ManualResetEvent (false);

        public void FindPath (Vector3 worldFrom, Vector3 worldTo, PathfindingDone callback) {
            lock (pathQueue) {
                pathQueue.Enqueue (new PathRequest (worldFrom, worldTo, callback));
                Debug.Log ("Path Request Queued");
            }
            if (!findingPath) {
                mre.Set ();
            }
            
        } // End FindPath

        private void _TryFindPath () {
            while (doPathfind) {
                // Waits for the first request to be made.
                mre.WaitOne ();

                lock (pathQueue) {
                    if (pathQueue.Count > 0) {
                        Debug.Log ("Finding Path");
                        findingPath = true;
                        
                        currentRequest = pathQueue.Dequeue ();
                    } else {
                        findingPath = false;
                        
                        mre.Reset ();
                    }
                }
                pathfinding.TryFindPath (currentRequest);
            }
        } // TryFindPath

        protected sealed override void SingletonAwake () {
            pathfinding = new AStar ().SetGrid<AStar> ((Grid2D) grid);
            pathfinding.logLevel = this.logLevel;

            doPathfind = true;
            pfThread = new Thread (new ThreadStart (_TryFindPath));
            pfThread.Name = "Pathfind Thread";
            pfThread.Start ();
        }

        protected sealed override void OnSingletonDestroyed () {
            doPathfind = false;
            pfThread.Interrupt ();
            pfThread.Abort ();
            Instance = null;
            if (logLevel.HasFlag (LogLevel.INFO)) {
                Debug.Log (logName + "Pathfinding terminated. Thread state " + pfThread.ThreadState);
            }
        }
    
    }

    public struct PathRequest {
        public Vector3 worldFrom;
        public Vector3 worldTo;
        public PathfindingDone callback;
        public bool getSimplified;

        public PathRequest (Vector3 worldFrom, Vector3 worldTo, PathfindingDone callback, bool getSimplified = true) {
            this.worldFrom = worldFrom;
            this.worldTo = worldTo;
            this.callback = callback;
            this.getSimplified = getSimplified;
        }
    }

    public struct PathfindResult {
        public readonly bool pathFound;
        public Vector3[] path;

        public PathfindResult (bool pathFound, Vector3[] path) {
            this.pathFound = pathFound;
            this.path = pathFound ? path : null;
        }
    }

}