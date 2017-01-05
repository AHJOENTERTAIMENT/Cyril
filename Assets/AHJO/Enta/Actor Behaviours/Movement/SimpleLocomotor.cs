using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHJO.Enta {

    [AddComponentMenu ("")]
    public sealed class SimpleLocomotor : LocomotorBase {

        protected sealed override void OnInitBehaviour () {
            this.behaviourUpdateType = BehaviourUpdateType.FixedUpdate;
#if DEBUG
            Debug.Log ("[Simple Locomotor (Actor Behaviour)]: Initialized for <" + this.name + ">.");
#endif
        }

        public override void BehaviourUpdate () {
            throw new NotImplementedException ();
        }

        float x, y;
        public override void FixedBehaviourUpdate () {
            x = Input.GetAxis ("Horizontal");
            y = Input.GetAxis ("Vertical");

            this.transform.position += new Vector3 (x, 0, y);
        }

    }

}

