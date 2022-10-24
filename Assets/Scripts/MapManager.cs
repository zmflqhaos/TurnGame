using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoSingleton<MapManager>
{
    public Vector3Int[] vecOne;
    // = { new Vector3Int(0, 1, 0), new Vector3Int(-1, 0, 0), new Vector3Int(0, -1, 0), new Vector3Int(1, 0, 0) }
    public Tile[,] mapTiles;

    [SerializeField]
    private MapSO[] mapSo;
    [SerializeField]
    private Tile[] tiles;
    [SerializeField]
    private Transform tileContainer;

    void Awake()
    {
        MakeMap();
    }

    private void MakeMap()
    {
        MapSO map = ChooseRandomMap();
        mapTiles = new Tile[map.mapTile.GetCells().GetLength(0), map.mapTile.GetCells().GetLength(1)];
        for(int i=0; i<map.mapTile.GetCells().GetLength(0); i++)
        {
            for(int j=0; j< map.mapTile.GetCells().GetLength(1); j++)
            {
                mapTiles[i, j] = Instantiate(tiles[map.mapTile.GetCell(j, i)], new Vector3(j, i, 0), Quaternion.identity);
                mapTiles[i, j].transform.SetParent(tileContainer);
            }
        }
    }

    private MapSO ChooseRandomMap()
    {
        return mapSo[Random.Range(0, mapSo.Length)];
    }

    public Tile SelectTile(Vector3 pos)
    {
        return mapTiles[(int)pos.y, (int)pos.x];
    }
}
