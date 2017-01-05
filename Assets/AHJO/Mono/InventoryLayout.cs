using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHJO.Mono {

    [CreateAssetMenu (fileName = "InventoryLayout", menuName = "AHJO/Mono/Inventory Layout")]
    public class InventoryLayout : ScriptableObject {

        public int numSlots;

        public Canvas inventoryGUI;

        public Inventory CreateInventory () {
            return new Inventory (numSlots);
        }

    }

}