using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHJO.Cyril {

    public class CyrilGameManager : SingletonBehaviour<CyrilGameManager>, IManager {

        public T GetManagerInstance<T> () where T : SingletonBehaviourBase {
            return Instance as T;
        }

        public void ManagerUpdate () {

        }
    }

}

