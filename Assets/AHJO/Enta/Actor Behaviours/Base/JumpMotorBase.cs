using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHJO.Enta {

    [AddComponentMenu (null)]
    public abstract class JumpMotorBase : ActorBehaviour {

        [SerializeField]
        internal float jumpPower = 7.5f;
        [Tooltip ("If slope is greater than character slope limit, will jump in direction of the surface normal, up othervise.")]
        [SerializeField]
        internal float maxSurfaceAngle = 60f;
        //[SerializeField] internal JumpConstraint jumpConstraint = JumpConstraint.GroundOnly;

        internal enum JumpConstraint {
            GroundOnly,
            DoubleJump,
            Always
        }

    }

}
