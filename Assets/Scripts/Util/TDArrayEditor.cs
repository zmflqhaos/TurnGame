using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapSO))]
public class TDArrayEditor : Editor
{
    MapSO map;
    MapSO clone = new MapSO();

    public MapSO DeepCopy(MapSO map)
    {
        MapSO deepCopyClass = new MapSO();
        deepCopyClass.mapTile = map.mapTile;
        deepCopyClass.Xsize = map.Xsize;
        deepCopyClass.Ysize = map.Ysize;
        return deepCopyClass;
    }

    private void OnEnable()
    {
        map = target as MapSO;
        clone = DeepCopy(map);
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.LabelField("Tile Map");

        if (clone.Xsize != map.Xsize || clone.Ysize != map.Ysize)
        {
            map.mapTile = new int[map.Ysize, map.Xsize];

            clone = DeepCopy(map);
        }

        if (clone.Xsize>0&&clone.Ysize>0)
        {
            for (int i = 0; i < clone.Ysize; i++)
            {
                EditorGUILayout.BeginHorizontal();
                for (int k = 0; k < clone.Xsize; k++)
                    map.mapTile[i, k] = EditorGUILayout.IntField(clone.mapTile[i, k]);
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
