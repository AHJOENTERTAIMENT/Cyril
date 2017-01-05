using System.Reflection;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace AHJO.Cyril.UI {

    [CreateAssetMenu (fileName = "NewGUIStyle", menuName = "AHJO/UI/GUI Style")]
    public class GUIStyle : ScriptableObject {

        [SerializeField]
        protected List<IStylable> stylables = new List<IStylable> ();

        [HideInInspector]
        public bool hasButtonStyle, hasInputFieldStyle;

        public List<Selectable> stylePrototypes = new List<Selectable> ();
        public List<System.Type> selectableTypes;

        void OnEnable () {
            FindAllSelectables ();
        }

        public void FindAllSelectables () {
            var type = typeof (Selectable);
            selectableTypes = Assembly.GetAssembly (type).GetTypes ().Where (t => t != type && type.IsAssignableFrom (t)).ToList ();
        }

        #region Public Funtions

        #endregion Public Functions




/**
        #region Buttons
        // Source Image
        public bool b_useSourceImage = true;
        public Sprite b_sourceImage;
        public Image.Type b_imageType;
        public Color b_baseColor = Color.white;
        public Material b_material;

        // Transition
        public Button.Transition b_transition;
        public ColorBlock b_colors;

        // Text
        public bool b_useText = true;
        public Font b_font;
        public FontStyle b_fontStyle;
        public int b_fontSize;
        public float b_lineSpacing;
        public bool b_richText;

        #endregion Buttons

        #region Input Field
        // Source Image
        public bool if_useSourceImage = true;
        public Sprite if_sourceImage;
        public Image.Type if_imageType;
        public Color if_baseColor = Color.white;
        public Material if_material;

        // Transition
        public Button.Transition if_transition;
        public ColorBlock if_colors;

        // Caret
        public float if_caretBlinkRate;
        public int if_caretWidth;
        public bool if_customCaretColor;
        public Color if_caretColor;
        public bool hideMobileInput;


        // Text
        public bool if_useText = true;
        public Font if_font;
        public FontStyle if_fontStyle;
        public int if_fontSize;
        public float if_lineSpacing;
        public bool if_richText;
        #endregion Input Field

        public void UseAsTemplate (Selectable selectable) {
            //var img = selectable.GetComponent<Image> ();
            //if (img) {

            //}
            //b_transition = b.transition;
            //b_colors = b.colors;
        }

        [Header ("Input Field")]
        public float field;
    **/

    }

}

