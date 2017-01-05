using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHJO.Mono {

    [System.Serializable]
    public class ItemStack {

        public Item stackItem;
        public int count;
        
        public ItemStack (Item stackItem, int count) {
            this.stackItem = stackItem;
            this.count = count;
        }
    }

}
