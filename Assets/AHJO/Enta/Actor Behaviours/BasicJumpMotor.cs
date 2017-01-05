using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AHJO.Cyril.ActorDelegates;

using AHJO.Cyril;

namespace AHJO.Enta {

    [System.Serializable]
    public class BasicJumpMotor : JumpMotorBase {

        public sealed override void BehaviourUpdate () {
            throw new NotImplementedException ();
        }

        public sealed override void FixedBehaviourUpdate () {
            if (!CheckNeedForUpdate ()) {
                return;
            }


        }

        protected sealed override void OnInitBehaviour () {

        }

        bool CheckNeedForUpdate () {
            return false;
        }

        bool EvaluateAndSetState (JumpState targetState) {
            return false;
        }

        void JumpStationary () {

        }

        void JumpLeaping () {

        }

        void OnActorHitGround (GroundHitInfo hitInfo) {

        }

    }

}

