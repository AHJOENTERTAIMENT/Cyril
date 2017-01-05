using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AHJO.Cyril.ActorDelegates;

namespace AHJO.Enta {

    // Just a Base Class with not much of Functionaly. At least it will be :Dd
    public abstract class Actor : Entity {

        public List<ActorBehaviour> actorBehaviours = new List<ActorBehaviour> ();
        protected bool updateListNeedUpdate;

        protected ActorBehaviour[] updateBehaviours;
        protected ActorBehaviour[] fixedUpdatedBehaviours;

        [MaskField]
        public LogLevel logLevel;

        /// <summary>
        /// Adds a Actor Behaviour to the Actor and returns the created instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T AddActorBehaviour<T> () where T : ActorBehaviour {
            actorBehaviours.Add (gameObject.AddComponent<T> ());
            actorBehaviours[actorBehaviours.Count - 1].hideFlags = HideFlags.HideInInspector;
            return (T) actorBehaviours[actorBehaviours.Count - 1];
        }

        #region Unity Functions
        protected virtual void Awake () {
            InitBehaviours ();
        }

        protected virtual void Update () {
            if (updateListNeedUpdate) {
                UpdateBehaviourUpdateLists ();
            }
            DoBehaviourUpdate ();
        }

        protected virtual void FixedUpdate () {
            if (updateListNeedUpdate) {
                UpdateBehaviourUpdateLists ();
            }
            DoFixedBehaviourUpdate ();
        }

        #endregion Unity Functions

        protected void InitBehaviours () {
            for (int i = 0; i < actorBehaviours.Count; i++) {
                actorBehaviours[i].InitBehaviour (this);
            }
            UpdateBehaviourUpdateLists ();
        }

        protected void UpdateBehaviourUpdateLists () {
            int numFixed = 0, numUpdate = 0, i;

            for (i = 0; i < actorBehaviours.Count; i++) {
                if (actorBehaviours[i].behaviourUpdateType == BehaviourUpdateType.FixedUpdate) numFixed++;
                if (actorBehaviours[i].behaviourUpdateType == BehaviourUpdateType.Update) numUpdate++;
            }

            updateBehaviours = new ActorBehaviour[numUpdate];
            fixedUpdatedBehaviours = new ActorBehaviour[numFixed];

            for (i = 0; i < actorBehaviours.Count; i++) {
                if (actorBehaviours[i].behaviourUpdateType == BehaviourUpdateType.Update)
                    updateBehaviours[updateBehaviours.Length - numUpdate--] = actorBehaviours[i];
                if (actorBehaviours[i].behaviourUpdateType == BehaviourUpdateType.FixedUpdate)
                    fixedUpdatedBehaviours[fixedUpdatedBehaviours.Length - numFixed--] = actorBehaviours[i];
            }
            updateListNeedUpdate = false;
        }

        /// <summary>
        /// Adds a Actor Behaviour to the Actor WITHOUT returning the instance.
        /// </summary>
        /// <typeparam name="Type of Actor Behaviour"></typeparam>
        /// <returns></returns>
        public void AddActorBehaviour (Type type) {
#if UNITY_EDITOR || DEBUG
            if (type.IsAssignableFrom (typeof (ActorBehaviour))) {
                Debug.LogError ("[ACTOR](" + this.GetInstanceID () + ") : trying to Add non Actor Behaviour as one! Offendor " + type);
                return;
            }
#endif 
            actorBehaviours.Add (gameObject.AddComponent (type) as ActorBehaviour);
            actorBehaviours[actorBehaviours.Count - 1].hideFlags = HideFlags.HideInInspector;

#if !UNITY_EDITOR
            actorBehaviours[actorBehaviours.Count - 1].InitBehaviour (this);
#endif
            updateListNeedUpdate = true;
        }

        protected void DoBehaviourUpdate () {
            for (int i = 0; i < updateBehaviours.Length; i++) {
                updateBehaviours[i].BehaviourUpdate ();
            }
        }

        protected void DoFixedBehaviourUpdate () {
            for (int i = 0; i < fixedUpdatedBehaviours.Length; i++) {
                fixedUpdatedBehaviours[i].FixedBehaviourUpdate ();
            }
        }

#if UNITY_EDITOR

        protected virtual void OnValidate () {       
            
        }

        public virtual void ActorBehaviourInspectors () {

        }

        public bool E_showBehaviours;
        public bool E_contentChanged;


        [ContextMenu ("Toggle Behaviour Visibility")]
        public void ToggleBehaviourVisibility () {
            E_showBehaviours = !E_showBehaviours;
        }

        [ContextMenu ("Remove Behaviours")]
        public void RemoveAllBehaviours () {
            E_contentChanged = true;
            var allb = this.GetComponents<ActorBehaviour> ();
            for (int i = 0; i < allb.Length; i++) {
                DestroyImmediate (allb[i]);
            }
            actorBehaviours.Clear ();
        }

#endif
        [System.Flags]
        public enum OverrideFlag {
            OverrideJump
        }

    }


}
