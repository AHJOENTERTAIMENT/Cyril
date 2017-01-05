using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

using AHJO.Cyril;

namespace AHJO.CyrilEditor { 

	public class CyrilEditorBase<T> : Editor where T : MonoBehaviour {

        private T property;
        protected T Property {
            get {
                if (property) {
                    return property;
                } else {
                    property = target as T;
                    return property;
                }
            }
        }
      
    }
	
}
