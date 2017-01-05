using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AHJO.Cameras;

namespace AHJO.Cyril {

    public class CameraManager : SingletonBehaviour<CameraManager>, IManager {

        private static int _nextCamSysID = -1;

        public CameraSystemBase defaultCameraSystem;
        [ShowOnly] [SerializeField] protected CameraSystemBase activeCameraSystem;
        public CameraSystemBase ActiveCameraSystem { get { return activeCameraSystem; } }

        public List<CameraSystemBase> cameraSystems;

        #region IManager
        public T GetManagerInstance<T> () where T : SingletonBehaviourBase {
            return Instance as T;
        }
        #endregion IManager

        protected override void SingletonAwake () {
            defaultCameraSystem.managedID = _nextCamSysID++;

            if (activeCameraSystem == null) {
                activeCameraSystem = defaultCameraSystem;
                activeCameraSystem.SetActive (true);
            }
            for (int i = 0; i < cameraSystems.Count; i++) {
                cameraSystems[i].SetActive (false);
                cameraSystems[i].managedID = _nextCamSysID++;
            }
        }

        public T GetCameraSystem<T> () where T : CameraSystemBase {
            T cs;
            for (int i = 0; i < cameraSystems.Count; i++) {
                if ((cs = cameraSystems[i] as T) != null) {
                    return cs;
                }   
            }
            return null;
        }

        public void RegisterCameraSystem (CameraSystemBase cameraSystem, bool setAsActive = false) {
            if (!cameraSystems.Contains (cameraSystem)) {
                cameraSystems.Add (cameraSystem);
                cameraSystem.managedID = _nextCamSysID++;
            }
        }

        public void SetActiveCameraSystem (int cameraSystemID) {
            if (cameraSystemID < cameraSystems.Count && cameraSystems[cameraSystemID] != null) {
                activeCameraSystem.SetActive (false);
                activeCameraSystem = cameraSystems[cameraSystemID];
                activeCameraSystem.SetActive (true);
            }
        }

        public void SetActiveCameraSystem (CameraSystemBase cameraSystem) {
            if (!cameraSystems.Contains (cameraSystem)) {
                activeCameraSystem.SetActive (false);
                activeCameraSystem = cameraSystem;
                activeCameraSystem.SetActive (true);
            }
        }

        public void ManagerUpdate () {
            //throw new NotImplementedException ();
        }


    }

}

