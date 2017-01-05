using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AHJO.Mono {

    public class Item : ScriptableObject {

        public int itemID;
        public string itemName;

        public ItemType itemType;

        public ItemEntity worldObject;
    }

}

