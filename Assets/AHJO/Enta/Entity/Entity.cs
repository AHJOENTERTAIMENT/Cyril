using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal;

namespace AHJO.Enta {

    public class Entity : MonoBehaviour {

        // TODO:
        // Consider repalcing with Hashset? Keep array for easy editor manipulation.
        [SerializeField] protected List<EntityAttribute> entityAttributes;


    }

}

