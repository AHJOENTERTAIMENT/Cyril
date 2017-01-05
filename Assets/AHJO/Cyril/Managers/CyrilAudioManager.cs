using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHJO.Cyril {

    public class CyrilAudioManager : SingletonBehaviour<CyrilAudioManager>, IManager {

        public T GetManagerInstance<T> () where T : SingletonBehaviourBase {
            //throw new NotImplementedException ();
            return Instance as T;
        }

        public void ManagerUpdate () {
            //throw new NotImplementedException ();
        }
    }

}

