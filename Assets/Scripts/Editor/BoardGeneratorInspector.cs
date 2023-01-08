using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BoardGenerator))]
public class BoardGeneratorInspector : Editor
{
   public BoardGenerator Current
    {
        get
        {
            return (BoardGenerator)target;
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Clear"))
            Current.Clear();
        if (GUILayout.Button("Grow"))
            Current.Grow();
        if (GUILayout.Button("Shrink"))
            Current.Shrink();
        if (GUILayout.Button("Grow Area"))
            Current.GrowArea();
        if (GUILayout.Button("Shrink Area"))
            Current.ShrinkArea();
        if (GUILayout.Button("Save"))
            Current.Save();
        if (GUILayout.Button("Load"))
            Current.Load();

        if (GUI.changed)
            Current.UpdateMarker();
    }
}
