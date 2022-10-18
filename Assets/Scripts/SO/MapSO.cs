using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "SO/Map")]
public class MapSO : ScriptableObject
{
    public int Xsize;
    public int Ysize;
    public int[,] mapTile = new int[1, 1];
}
