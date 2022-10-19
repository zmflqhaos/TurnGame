using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "SO/Map")]
public class MapSO : ScriptableObject
{
    public Vector2 size;
    public int[,] mapTile;
}
