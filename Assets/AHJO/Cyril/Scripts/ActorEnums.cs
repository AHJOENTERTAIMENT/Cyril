using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHJO.Cyril {

    internal enum MovementState {
        Ground_Idle,
        Ground_Walk,
        Ground_Run,
        Ground_Sprint,
        Ground_Dash,

        Levitate,

        Air_Levitate,
        Air_Fall,
        Air_Fly
    }

    internal enum JumpState {
        None,
        Waiting,
        Disabled,

        Jump, // Mark character for jumping
        Boosting, // Actor is boosting the jump
        Jumped, // Mark character for has jumped
        Landed, // Mark character for regrounding
    }

}