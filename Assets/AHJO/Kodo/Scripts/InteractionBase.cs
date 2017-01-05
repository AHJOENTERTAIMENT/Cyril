using UnityEngine;
using System.Collections.Generic;

namespace AHJO.Kodo { 

	public abstract class InteractionBase : ActionBase {

        public string displayName = "Interact";

        public virtual void Interact (KeyCode eventKey) {
            if (actionKey == eventKey) {

            }
        }
    }
	
}
