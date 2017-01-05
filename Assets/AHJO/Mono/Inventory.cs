using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHJO.Mono {

    [System.Serializable]
    public class Inventory {

        public InvetorySlot[] inventorySlots;

        public Inventory (int numSlots) {
            this.inventorySlots = new InvetorySlot[numSlots];

            for (int i = 0; i < inventorySlots.Length; i++) {
                inventorySlots[i] = new InvetorySlot (i);
            }
        }

        public virtual bool Open () {
            return false;
        }

        public virtual void Close () {

        }

    }

    [System.Serializable]
    public struct InvetorySlot {

        public int slotID;
        public ItemStack itemStack;

        public InvetorySlot (int slotID, ItemStack stack = null) {
            this.slotID = slotID;
            this.itemStack = stack;
        }
    }

}

