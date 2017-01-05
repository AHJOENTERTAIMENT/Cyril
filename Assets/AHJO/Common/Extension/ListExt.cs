using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListExtensions {

    /// <summary>
    /// Strips entries with persisting directions from a List of Vector3s
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public static List<Vector3> Simplify (this List<Vector3> list) {
        // Nothing to sort really. Just return the original.
        if (list.Count < 2) {
            return list;
        }
        float startAngle =  Vector3.Angle (Vector3.zero, list[0]);
        var rList = new List<Vector3> ();

        for (int i = 1; i < list.Count; i++) {
            if (Mathf.Approximately (startAngle, Vector3.Angle (list[i -1], list[i]))) {
                rList.Add (list[i]);
            }
        }
        return rList;
    }
}