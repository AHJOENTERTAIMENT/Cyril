using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHJO.Mono {

    public class Container : MonoBehaviour {

        public InventoryLayout layout;

        public bool hasPresetContent;
        public Inventory attachedInventory;

        public void OpenAttachedInventory () {
            var gui = GameObject.Instantiate (layout.inventoryGUI);
        }

        [ContextMenu ("Reset Inventory")]
        public void ClearInventory () {
            if (hasPresetContent) attachedInventory = new Inventory (layout.numSlots);
            else attachedInventory = new Inventory (0);
        }
    }

}

