using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Map))]
public class Editor_map : Editor
{
    
    public override void OnInspectorGUI()
    {
        Map Map = (Map)target;
        base.OnInspectorGUI();
        if (GUILayout.Button("Creat map /kc la7"))
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
        if (GUILayout.Button("Creat Emty"))
        {
            Map.createmty();
        }
        //if (GUILayout.Button("Creat Trap/TRap kc la 2 la lien nhau"))
        //{
        //    Map.creat_Trap();
        //}
        //if (GUILayout.Button("Creat Loxo/ LOxo kc la 19"))
        //{
        //    Map.creat_Loxo();
        //}
        if (GUILayout.Button("Creat coins/ Coins kc la 3"))
        {
            Map.creat_Coins();
        }
        //if (GUILayout.Button("Back"))
        //{
        //    Map.back();
        //}
        if (GUILayout.Button("Creat enemyChick /kc la3"))
        {
            Map.createChick();
        }


    }

}
