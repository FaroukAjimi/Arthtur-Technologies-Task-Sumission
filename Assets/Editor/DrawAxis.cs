using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(InteractionPoints))]
public class DrawAxis : Editor
{

    void OnSceneGUI()
    {
        InteractionPoints t = target as InteractionPoints;
        Handles.DrawLine(t.zero(), t.Axis()[0]);
        Handles.DrawLine(t.zero(), t.Axis()[1]);
        Handles.DrawLine(t.zero(), t.Axis()[2]);
    }
}
