using UnityEngine;
using System.Collections.Generic;

using AHJO.Cyril;

namespace AHJO.Enta { 

    [CreateAssetMenu (fileName = "Entity Registry", menuName = "AHJO/Enta/Entity Registry")]
	public class EntityRegistry : ScriptableObject {

        [SerializeField]
        [HideInInspector]
        private int _lastRegistryLength;

        [SerializeField]
        protected RegistryEntry[] defaultEntities;
        // TODO:
        // Replace with hashset
        protected Dictionary<int, Entity> registeredEntities;

        void OnEnable () {
            registeredEntities = new Dictionary<int, Entity> ();

            if (defaultEntities != null) {

                // TODO:
                // Move ID assignment to a Editor Script
#if !UNITY_EDITOR
                for (int i = 0; i < defaultEntities.Length; i++) {
                    registeredEntities.Add (defaultEntities[i].ID, defaultEntities[i].RegistryObject);
                }
#else
                if (_lastRegistryLength != defaultEntities.Length) {
                    _lastRegistryLength = defaultEntities.Length;
                    for (int i = 0; i < _lastRegistryLength; i++) {
                        defaultEntities[i] = new RegistryEntry (i, defaultEntities[i].RegistryObject);
                    }
                }                     
#endif

        }
    }

        [System.Serializable]
        public struct RegistryEntry {

            [SerializeField]
            [ShowOnly]
            int _id;
            public int ID { get { return _id; } }

            [SerializeField]
            Entity _registryObject;
            public Entity RegistryObject { get { return _registryObject; } }

            public RegistryEntry (int id, Entity entityToregister) {
                this._id = id;
                this._registryObject = entityToregister;
            }
        }
    } 
	
}
