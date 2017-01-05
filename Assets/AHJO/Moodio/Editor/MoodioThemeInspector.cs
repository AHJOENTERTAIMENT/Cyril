using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace AHJO.Moodio { 

    [CustomEditor (typeof (MoodioTheme))]
	public class MoodioThemeInspector : Editor {

        public override void OnInspectorGUI () {
            //var mti = target as MoodioTheme;

            if (GUILayout.Button ("Show in Moodio Theme Editor")) {

            }
        }

    }
	
}
