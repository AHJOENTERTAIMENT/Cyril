using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHJO.Enta {

    [AddComponentMenu (null)]
    public abstract class ActorBehaviour : MonoBehaviour {

        public byte updatePriority = 125;

        [ShowOnly]
        public Actor parentActor;

        public BehaviourUpdateType behaviourUpdateType;

        protected static readonly string behaviourName = "Actor Behaviour";
        public string BehaviourName { get { return behaviourName; } }

        public void InitBehaviour (Actor parentActor) {
            this.parentActor = parentActor;
            OnInitBehaviour ();
        }

        protected abstract void OnInitBehaviour ();

        public abstract void BehaviourUpdate ();

        public abstract void FixedBehaviourUpdate ();

        public virtual void DrawBehaviourInspector () {
        }

    }

}

