using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHJO.Cyril {

    public class CyrilManager : SingletonBehaviour<CyrilManager> {

        [Header ("Managers")]
        public SingletonBehaviourBase[] managers;

        private IManager[] _iManagers;

        protected sealed override void SingletonAwake () {
            _iManagers = new IManager[managers.Length];
            for (int i = 0; i < managers.Length; i++) {
                _iManagers[i] = managers[i] as IManager;
                Debug.Log (_iManagers[i].GetType ());
            }
        }

        void Update () {
            for (int i = 0; i < _iManagers.Length; i++) {
                _iManagers[i].ManagerUpdate ();
            }   
        }

        private int previousHash;
        void OnValidate () {
            if (managers != null && managers.GetHashCode () != previousHash) {
                previousHash = managers.GetHashCode ();
                int i;
                List<SingletonBehaviourBase> temp = new List<SingletonBehaviourBase> ();
                for (i = 0; i < managers.Length; i++) {
                    if (managers[i] == null) continue;
                    if (managers[i] is IManager) {
                        temp.Add (managers[i]);
                    } else {
                        Debug.LogError ("Singleton does not implement the IManager interface! Cannot use as a Manager Component. Offendor: " + managers[i].name);
                    }
                }
                managers = temp.ToArray ();
            } 

        }
       
    }

}
