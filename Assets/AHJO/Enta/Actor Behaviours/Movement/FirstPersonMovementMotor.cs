using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHJO.Enta {

    [AddComponentMenu (null)]
    public class FirstPersonMovementMotor : LocomotorBase {

        [SerializeField] internal float speedForward = 3f;
        [SerializeField] internal float speedBackward = 1.8f;     
        [SerializeField] internal float speedStrafe = 2.2f;     
        [Range (1f, 100f)]
        [SerializeField] internal float runMultiplier = 2.2f;        
        [Range (1f, 100f)]
        [SerializeField] internal float sprintMultiplier = 4f;

        [Tooltip ("A multiplier in range of [0,1].")]
        [Range (0f, 1f)]
        [SerializeField]
        internal float airControl = 0.35f;
        [SerializeField]
        internal float gravityMultiplier = 1.5f;

        protected override void OnInitBehaviour () {
            throw new NotImplementedException ();
        }

        public override void BehaviourUpdate () {
            throw new NotImplementedException ();
        }

        public override void FixedBehaviourUpdate () {
            throw new NotImplementedException ();
        }


    }

}