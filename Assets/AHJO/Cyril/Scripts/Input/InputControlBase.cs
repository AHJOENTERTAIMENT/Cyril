using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AHJO.Cyril {

    public abstract class InputControlBase : MonoBehaviour {

        public InputAction[] inputActions;

        public void AddInputAction () {

        }

        public void RemoveInputAction () {

        }

        public enum KeyTriggerType {
            Down,
            Pressed,
            Released,
        }
    }

}