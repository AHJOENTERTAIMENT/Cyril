using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHJO.Mono {

    public class ItemEntity : MonoBehaviour {

        public Item item;

        public Item GetEntityAsItem () {
            return item;
        }
    }

}

