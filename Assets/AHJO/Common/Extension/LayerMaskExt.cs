using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class LayerMaskExtensions {

    public static bool HasFlag (this LayerMask layerMask, LayerMask flag) {
        return (layerMask & flag) == flag;
    }

}
