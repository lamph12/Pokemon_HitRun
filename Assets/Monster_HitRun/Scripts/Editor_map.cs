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
        if (GUILayout.Button("Creat road_1"))
        {
            Map.road_1();
        }
        if (GUILayout.Button("Creat road_2"))
        {
            Map.road_2();
        }
        if (GUILayout.Button("Creat wall_right"))
        {
            Map.creatwall_right();
        }
        if (GUILayout.Button("Creat wall_left"))
        {
            Map.creatwall_left();
        }
        if (GUILayout.Button("Back"))
        {
            Map.back();
        }
        if (GUILayout.Button("Creat Emty"))
        {
            Map.createmty();
        }



    }
   
}
