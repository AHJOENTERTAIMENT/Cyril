using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHJO.Cyril {

    /// <summary>
    /// A custom Input Manager with all the functionality of the unity one. This is not thread safe!
    /// </summary>
    public class CyrilInputManager : SingletonBehaviour<CyrilInputManager>, IManager {

        public KeyBindMapping defaultKeyMap;

        /// <summary>
        /// Event fired when there is a change to the Input Mapping. This is to help notify scripts dependent on specific input so they can dynamically reload their keys.
        /// </summary>
        public event Action InputMappingUpdatedHandler;

        // Dictionary holding all the events based mappings. Arrays instead of list for performance as these wont probably get tinkered with often.
        protected Dictionary<KeyCode, InputAction[]> inputActionMap = new Dictionary<KeyCode, InputAction[]> ();
        protected Dictionary<KeyValuePair<KeyCode, KeyCode>, AxisAction[]> inputAxisActionMap = new Dictionary<KeyValuePair<KeyCode, KeyCode>, AxisAction[]> ();

        // Queues for registering new InputActions.
        private List<KeyValuePair<KeyCode, InputAction>> _addQueue = new List<KeyValuePair<KeyCode, InputAction>> ();
        private List<KeyValuePair<KeyValuePair<KeyCode, KeyCode>, AxisAction>> _addQueueAxis = new List<KeyValuePair<KeyValuePair<KeyCode, KeyCode>, AxisAction>> ();

        private bool[] keyState_CF = new bool[Enum.GetValues (typeof (KeyCode)).Length]; // Key state for this frame
        private bool[] keyState_LF = new bool[Enum.GetValues (typeof (KeyCode)).Length]; // Key state for last frame

        protected bool inputAddQueueHasItems;
        protected bool keyMapUpdated;

        bool lastKeyState;
        int keyInd;

        public T GetManagerInstance<T> () where T : SingletonBehaviourBase {
            return Instance as T;
        }

        public void ManagerUpdate () {
            if (keyMapUpdated) {
                keyMapUpdated = false;
                UpdateActionsFromKeyMap ();
            }
            if (inputAddQueueHasItems) {
                inputAddQueueHasItems = false;
                AddQueuedItems ();
            }
            foreach (var inputMapping in inputActionMap) {
                EvaluateInput (inputMapping.Key, inputMapping.Value);
            }
            foreach (var axisMapping in inputAxisActionMap) {
                EvaluateAxis (axisMapping.Key.Key, axisMapping.Key.Value, axisMapping.Value);
            }
        }

        public void AddInputListener (InputAction inputAction) {
            KeyCode kc = defaultKeyMap.GetMappedKey (inputAction.inputName);
            if (kc == KeyCode.None) {
                return;
            }
            _addQueue.Add (new KeyValuePair<KeyCode, InputAction> (kc, inputAction));
            inputAddQueueHasItems = true;
        }

        public void AddInputListeners (InputAction[] inputActions) {
            for (int i = 0; i < inputActions.Length; i++) {
                AddInputListener (inputActions[i]);
            }
        }

        public void AddAxisListener (AxisAction axisAction) {
            KeyValuePair<KeyCode, KeyCode> axisKeys = defaultKeyMap.GetAxisKeys (axisAction.axisName);
            if (axisKeys.Value == KeyCode.None) {
                return;
            }
            _addQueueAxis.Add (new KeyValuePair<KeyValuePair<KeyCode, KeyCode>, AxisAction> (axisKeys, axisAction));
            inputAddQueueHasItems = true;
        }

        public void AddAxisListeners (AxisAction[] axisAction) {
            for (int i = 0; i < axisAction.Length; i++) {
                AddAxisListener (axisAction[i]);
            }
        }

        public void RemoveInputListener () {

        }

        /// <summary>
        /// Return Key state based on its Input Map name
        /// </summary>
        /// <param name="mapped key name"></param>
        /// <returns></returns>
        public bool GetKey (string keyName) {
            var keyint = (int) defaultKeyMap.GetMappedKey (keyName);

            if (keyState_CF[keyint]) {
                return true;
            }
            return false;
        }

        public bool GetKey (int keyID) {
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapped key name"></param>
        /// <returns></returns>
        public bool GetKeyDown (string keyName) {
            var keyint =  (int) defaultKeyMap.GetMappedKey (keyName);

            if (keyState_CF[keyint] && keyState_LF[keyint]) {
                return true;
            }
            return false;
        }

        public bool GetKeyDown (int keyID) {
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapped key name"></param>
        /// <returns></returns>
        public bool GetKeyUp (string keyName) {
            var keyint = (int) defaultKeyMap.GetMappedKey (keyName);

            if ( keyState_LF[keyint] && keyState_CF[keyint]) {
                return true;
            }
            return false;
        }

        public bool GetKeyUp (int keyID) {
            return false;
        }

        void InvokeCallbacks (InputAction[] inputActions, KeyState keyState = KeyState.KeyHeld) {
            for (int i = 0; i < inputActions.Length; i++) {
                inputActions[i].inputEvent.Invoke ();
            }
        }

        void InvokeAxisCallbacks (AxisAction[] axisAction, float axisValue) {
            for (int i = 0; i < axisAction.Length; i++) {
                axisAction[i].axisEvent.Invoke (axisValue);
            }
        }

        void EvaluateAxis (KeyCode pos, KeyCode neg, AxisAction[] axisAction) {
            int value = 0;

            if (Input.GetKey (pos)) {
                value++;
            }
            if (Input.GetKey (neg)) {
                value--;
            }
            if (value != 0) {
                InvokeAxisCallbacks (axisAction, value);
            }
        }

        // Check if the specifinc Key assosiated with the Input was pressed within this frame or the last and send appropiate events.
        // Also marks the internal buffers for key state grabbing via the GetKey, GetKeyDown, GetKeyUp methods.
        void EvaluateInput (KeyCode key, InputAction[] inputActions) {
            keyInd = (int) key;
            if (keyState_CF[keyInd]) keyState_LF[keyInd] = true;

            if (Input.GetKey (key)) {
                keyState_CF[keyInd] = true;
                InvokeCallbacks (inputActions, keyState_LF[keyInd] ? KeyState.KeyHeld : KeyState.KeyPressed);            
            } else {
                if (keyState_CF[keyInd]) {
                    keyState_CF[keyInd] = false;
                    InvokeCallbacks (inputActions, KeyState.KeyReleased);
                } else if (keyState_LF[keyInd]) {
                    keyState_LF[keyInd] = false;
                }
            }
        }

        // Updates the InputAction Arrays
        void AddQueuedItems () {
            InputAction[] ia;
            int i = 0;
            for (; i < _addQueue.Count; i++) {
                if (inputActionMap.TryGetValue (_addQueue[i].Key, out ia)) {
                    Array.Resize<InputAction> (ref ia, ia.Length + 1);
                    ia[ia.Length - 1] = _addQueue[i].Value;
                } else {
                    inputActionMap.Add (_addQueue[i].Key, new InputAction[1] { _addQueue[i].Value });
                }               
            }
            AxisAction[] aa;
            for (i = 0; i < _addQueueAxis.Count; i++) {
                if (inputAxisActionMap.TryGetValue (_addQueueAxis[i].Key, out aa)) {
                    Array.Resize<AxisAction> (ref aa, aa.Length + 1);
                    aa[aa.Length - 1] = _addQueueAxis[i].Value;
                } else {
                    inputAxisActionMap.Add (_addQueueAxis[i].Key, new AxisAction[1] { _addQueueAxis[i].Value });
                }
            }   
                _addQueue.Clear ();
            _addQueueAxis.Clear ();
        }

        void UpdateActionsFromKeyMap () {

            // Clear Input Action maps
            inputActionMap.Clear ();
            inputAxisActionMap.Clear ();

            // Send Update event
            if (InputMappingUpdatedHandler != null) {
                InputMappingUpdatedHandler ();
            }
            // Proceed with normal stuff wich will automatically rebuild the Input Action Maps.
        }

    }

    public enum KeyState : byte {
        KeyPressed,
        KeyHeld,
        KeyReleased
    }
}

