using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AHJO.Cyril.UI {

    public class StylableButton : Button {

        [Header ("Style Settings")]
        public GUIStyle guiStyle;

        public void UpdateFromStyle () {
            if (guiStyle) {
                //transition = guiStyle.b_transition;
                //colors = guiStyle.b_colors;
            }
            if (image) {
                //image.color = guiStyle.
            }
        }


#if UNITY_EDITOR
        [MenuItem ("GameObject/UI/AHJO/Stylable Button", false, 10)]
        static void NewStylableButton (MenuCommand menuCommand) {
            var go = new GameObject ("Stylable Button");
            go.AddComponent<Image> ();
            var sb = go.AddComponent<StylableButton> ();
            sb.UpdateFromStyle ();

            go.GetComponent<RectTransform> ().sizeDelta = new Vector2 (160, 30);
                
            GameObjectUtility.SetParentAndAlign (go, menuCommand.context as GameObject);
            Undo.RegisterCreatedObjectUndo (go, "Create " + go.name);
            Selection.activeGameObject = go;
        }
#endif
    }

}

