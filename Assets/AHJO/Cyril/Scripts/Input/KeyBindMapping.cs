using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHJO.Cyril {

    [CreateAssetMenu (menuName = "AHJO/Cyril/Input Settings", fileName = "Input Settings")]
    public class KeyBindMapping : ScriptableObject {

        public delegate void KeyBindsChanged ();

        [Header ("Individual Keys")]
        public InputDefinition[] inputDefinitions = new InputDefinition[] {
             new InputDefinition ("Console", KeyCode.Backslash),
             new InputDefinition ("Chat", KeyCode.Return),

             new InputDefinition ("Forward", KeyCode.W),
             new InputDefinition ("Backwards", KeyCode.S),
             new InputDefinition ("Left", KeyCode.L),
             new InputDefinition ("Right", KeyCode.R),

             new InputDefinition ("Jump", KeyCode.Space),
             new InputDefinition ("Sprint", KeyCode.LeftShift),
             new InputDefinition ("Crouch", KeyCode.LeftAlt),
             new InputDefinition ("Activate", KeyCode.F),
             new InputDefinition ("ToggleWalk", KeyCode.CapsLock),

             new InputDefinition ("Inventory", KeyCode.I),
             new InputDefinition ("Character", KeyCode.P),
             new InputDefinition ("Map", KeyCode.M),
             new InputDefinition ("Scoreboard", KeyCode.Tab),

             new InputDefinition ("Use Primary", KeyCode.Mouse0),
             new InputDefinition ("Use Secondary", KeyCode.Mouse1),
             new InputDefinition ("Use Tertiary", KeyCode.Mouse2),

             new InputDefinition ("Ready", KeyCode.R),

             new InputDefinition ("Ability 1", KeyCode.Alpha1),
             new InputDefinition ("Ability 2", KeyCode.Alpha2),
             new InputDefinition ("Ability 3", KeyCode.Alpha3),
             new InputDefinition ("Ability 4", KeyCode.Alpha4),
             new InputDefinition ("Ability 5", KeyCode.Alpha5),
             new InputDefinition ("Ability 6", KeyCode.Alpha6),
             new InputDefinition ("Ability 7", KeyCode.Alpha7),
             new InputDefinition ("Ability 8", KeyCode.Alpha8),
             new InputDefinition ("Ability 9", KeyCode.Alpha9),

             new InputDefinition ("Ability 10", KeyCode.Q),
             new InputDefinition ("Ability 11", KeyCode.E),
             new InputDefinition ("Ability 12", KeyCode.Z),
             new InputDefinition ("Ability 13", KeyCode.X)
        };

        [Header ("Input Axis")]
        public InputAxis[] axisDefinitions = new InputAxis[] {
            new InputAxis ("Horizontal", KeyCode.D, KeyCode.A),
            new InputAxis ("Vertical", KeyCode.W, KeyCode.S),
        };

        protected Dictionary<string, KeyCode> inputMap;
        protected Dictionary<string, KeyValuePair<KeyCode, KeyCode>> axisMap;

        void OnEnable () {
            inputMap = new Dictionary<string, KeyCode> ();
            axisMap = new Dictionary<string, KeyValuePair<KeyCode, KeyCode>> ();

            int i = 0;
            for (; i < inputDefinitions.Length; i++) {
                inputMap.Add (inputDefinitions[i].name, inputDefinitions[i].key);
            }
            for (i = 0; i < axisDefinitions.Length; i++) {
                axisMap.Add (axisDefinitions[i].name, new KeyValuePair<KeyCode, KeyCode> (axisDefinitions[i].positiveKey, axisDefinitions[i].negativeKey));
            }
        }

        public KeyCode GetMappedKey (string inputName) {
            KeyCode kc;
            if (inputMap.TryGetValue (inputName, out kc)) {
                return kc;
            }
            return KeyCode.None;
        }

        public KeyValuePair<KeyCode, KeyCode> GetAxisKeys (string axisName) {
            KeyValuePair<KeyCode, KeyCode> retKeys;
            if (!axisMap.TryGetValue (axisName, out retKeys)) {
                retKeys = new KeyValuePair<KeyCode, KeyCode> (KeyCode.None, KeyCode.None);
            }
            return retKeys;
        }

        [System.Serializable]
        public struct InputDefinition {
            public readonly string name;
            public readonly KeyCode key;

            public InputDefinition (string name, KeyCode keyCode) {
                this.name = name;
                this.key = keyCode;
            }

        }

        [System.Serializable]
        public struct InputAxis {
            public readonly string name;
            public readonly KeyCode positiveKey, negativeKey;

            public InputAxis (string name, KeyCode positiveKey, KeyCode negativeKey) {
                this.name = name;
                this.positiveKey = positiveKey;
                this.negativeKey = negativeKey;
            }
        }
    }

}