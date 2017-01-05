using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHJO {

    [System.Flags]
    public enum BehaviourUpdateType {
        None = 0,
        Update = 1,
        FixedUpdate = 2
    }

}