using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHJO.Cyril {

    /// <summary>
    /// Extended Input Controller with predefinable InputMappings
    /// </summary>
    public class CyrilInputListener : InputControlBase {

        public AxisAction[] axisActions;

        void Start () {
            CyrilInputManager.Instance.InputMappingUpdatedHandler += OnInputMapsChanged;
            CyrilInputManager.Instance.AddInputListeners (inputActions);
            CyrilInputManager.Instance.AddAxisListeners (axisActions);
        }

        void OnDestroy () {
            CyrilInputManager.Instance.InputMappingUpdatedHandler -= OnInputMapsChanged;
        }

        void OnInputMapsChanged () {

        }
    }

}

