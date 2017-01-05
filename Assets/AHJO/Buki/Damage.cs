using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHJO.Buki {

    public struct Damage {
        public DamageType type;
        public float amount;

        public Damage (DamageType type, float amount) {
            this.type = type;
            this.amount = amount;
        }

    }

}
