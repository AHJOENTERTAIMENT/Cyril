using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

namespace AHJO.Cyril {

    [System.Serializable]
    public class InputAction {

        public string inputName;
        public UnityEvent inputEvent;

    }

    [System.Serializable]
    public class AxisAction {

        public string axisName;
        public AxisEvent axisEvent;
    }
}