using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AHJO.Cyril;

namespace AHJO.Enta {

    /// <summary>
    /// Uses the Global Audio Manager to play a surface dependent sound character on steps and such. The Audio Manager will handle any and all space related audio modulations.
    /// </summary>
    public class SurfaceSoundEmitter : ActorBehaviour {

        protected override void OnInitBehaviour () {
            throw new NotImplementedException ();
        }

        public SurfaceSoundMap surfaceSoundMap;

        public override void BehaviourUpdate () {
            throw new NotImplementedException ();
        }
        public override void FixedBehaviourUpdate () {
            throw new NotImplementedException ();
        }

        public void PlayStepSound () {

        }
    }

}

