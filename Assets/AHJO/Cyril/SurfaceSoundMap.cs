using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHJO.Cyril {

    [CreateAssetMenu (menuName = "AHJO/SFX/Surface Sound Map", fileName = "Surface Sound Map")]
    public class SurfaceSoundMap : ScriptableObject {

        [SerializeField]
        protected SurfaceSoundMapping[] surfaceSounds;
        public Dictionary<PhysicMaterial, SurfaceSoundMapping> materialSoundMap;

        public SurfaceSoundMapping GetSurfaceSoundMap (PhysicMaterial surfaceMaterial) {
            SurfaceSoundMapping surfaceSoundMap;
            materialSoundMap.TryGetValue (surfaceMaterial, out surfaceSoundMap);
            return surfaceSoundMap;
        }

        [System.Serializable]
        public class SurfaceSoundMapping {
            public PhysicMaterial surfaceMaterial;
            public AudioClip[] surfaceSounds;
        }
    }

}

