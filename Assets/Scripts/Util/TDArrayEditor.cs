using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapSO))]
[CanEditMultipleObjects]
public class TDArrayEditor : Editor
{
    MapSO map;
    Vector2 _size;

    SerializedProperty mapSize;
    SerializedProperty mapTile;

    private void OnEnable()
    {
        map = target as MapSO;

        mapSize = serializedObject.FindProperty("mapSize");
        mapTile = serializedObject.FindProperty("mapTile");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(mapSize);
        EditorGUILayout.PropertyField(mapTile);
        map.size = EditorGUILayout.Vector2Field("Map Size", map.size);

        if (_size.x != map.size.x || _size.y != map.size.y)
        {
            _size.x = map.size.x;
            _size.y = map.size.y;
            map.mapTile = new int[(int)_size.x, (int)_size.y];
        }

        EditorGUILayout.LabelField("Tile Map");
        if (_size.x > 0 && _size.y > 0)
        {
            for (int i = 0; i < _size.x; i++)
            {
                EditorGUILayout.BeginHorizontal();
                for (int k = 0; k < _size.y; k++)
                    map.mapTile[i, k] = EditorGUILayout.IntField(map.mapTile[i, k]);
                EditorGUILayout.EndHorizontal();
            }
        }
        serializedObject.ApplyModifiedProperties();
    }
}
