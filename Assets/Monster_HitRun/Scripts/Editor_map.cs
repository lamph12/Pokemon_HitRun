using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(map))]
public class Editor_map : Editor
{
    // Start is called before the first frame update
    public override void OnInspectorGUI()
    {
        map Map = (map)target;
        base.OnInspectorGUI();
        if (GUILayout.Button("Creat map"))
        {
            Map.taomap();
        }
    }
   
}