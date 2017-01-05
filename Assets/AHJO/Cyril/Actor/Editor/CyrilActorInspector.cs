using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;
using System.Linq;

using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

using AHJO.Cyril;
using AHJO.Enta;

namespace AHJO.CyrilEditor {

    [CustomEditor (typeof (CyrilActor))]
    public class CyrilActorInspector : Editor {

        CyrilActor motor;
        ReorderableList behaviourList;

        Type[] behaviourTypes;
        string[] behaviourNames;

        bool showAddMenu;
        bool pewviousBehaviourViewState;

        BehaviourPopup behehaviourPopUp;

        void OnEnable () {
            motor = target as CyrilActor;

            UpdateNames ();
            CreateReorderableBehaviours ();
        }

        void OnDisable () {
            FreeResources ();
        }

        public override void OnInspectorGUI () {
            EditorGUILayout.HelpBox ("Inspector Unfinished! Some information not displayed!", MessageType.Info);

            if (pewviousBehaviourViewState != motor.E_showBehaviours) {
                pewviousBehaviourViewState = motor.E_showBehaviours;
                UpdateBehaviourVisibility ();
            }

            if (behaviourList != null) {
                if (motor.E_contentChanged) {
                    UpdateNames ();
                    CreateReorderableBehaviours ();
                    motor.E_contentChanged = false;
                }
                behaviourList.DoLayoutList ();
            }
        }

        void CreateReorderableBehaviours () {
            behaviourList = new ReorderableList (motor.actorBehaviours, typeof (UnityEngine.Object));
            behaviourList.drawHeaderCallback += DrawHeader;
            behaviourList.drawElementCallback += DrawElement;
            behaviourList.onAddDropdownCallback += OnAddDropdown;
            behaviourList.onRemoveCallback += RemoveItem;

            UpdateBehaviourVisibility ();
        }

        void UpdateBehaviourVisibility () {
            // TODO:
            // FIX this hack! Unity bug that hide flag setting is not persisting?
            for (int i = 0; i < motor.actorBehaviours.Count; i++) {
                if (motor.E_showBehaviours) {
                    motor.actorBehaviours[i].hideFlags = HideFlags.None;
                } else {
                    motor.actorBehaviours[i].hideFlags = HideFlags.HideInInspector;
                }
            }
        }

        void FreeResources () {
            behaviourList.drawHeaderCallback -= DrawHeader;
            behaviourList.drawElementCallback -= DrawElement;
            behaviourList.onAddDropdownCallback -= OnAddDropdown;
            behaviourList.onRemoveCallback -= RemoveItem;
        }

        void DrawHeader (Rect rect) {
            GUI.Label (rect, "Actor Behaviours");
        }

        void DrawElement (Rect rect, int index, bool active, bool focused) {
            ActorBehaviour ab = (ActorBehaviour) behaviourList.list[index];

            EditorGUI.BeginChangeCheck ();

            if (ab != null) {
                EditorGUI.TextField (rect, ab.GetType ().ToString ());
            }

            EditorGUI.EndChangeCheck ();
        }

        int asd = 0;
        void OnAddDropdown (Rect buttonRect, ReorderableList list) {
            PopupWindow.Show (buttonRect, behehaviourPopUp);
        }

        void AddBehaviour (Type type) {
            if (type == null) return;
            motor.AddActorBehaviour (type);
            FreeResources ();
            CreateReorderableBehaviours ();
        }

        void RemoveItem (ReorderableList list) {
            behaviourList.list.RemoveAt (list.index);
            EditorUtility.SetDirty (target);
        }


        void UpdateNames () {
            behaviourTypes = (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies ()
                              from assemblyType in domainAssembly.GetTypes ()
                              where typeof (ActorBehaviour).IsAssignableFrom (assemblyType) && !assemblyType.IsAbstract
                              select assemblyType).ToArray ();

            behaviourNames = new string[behaviourTypes.Length];
            for (int i = 0; i < behaviourTypes.Length; i++) {
                behaviourNames[i] = behaviourTypes[i].ToString ();
            }
            behehaviourPopUp = new BehaviourPopup (behaviourTypes, AddBehaviour);
        }

        protected class BehaviourPopup : PopupWindowContent {

            Action<Type> callback;
            protected Type[] types;

            protected BehaviourPopup () {
            }
            public BehaviourPopup (Type[] typeList, Action<Type> callback) {
                this.types = typeList;
                this.callback = callback;
            }

            public override void OnGUI (Rect rect) {
                for (int i = 0; i < types.Length; i++) {
                    if (GUILayout.Button (types[i].ToString ())) {
                        callback (types[i]);
                    }
                }
            }
        }
    }

}
