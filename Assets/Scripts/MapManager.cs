using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoSingleton<MapManager>
{
    [SerializeField]
    private MapSO[] mapSo;
    [SerializeField]
    private Tile[] tiles;
    [SerializeField]
    private Transform tileContainer;

    void Start()
    {
        MakeMap();
    }

    private void MakeMap()
    {
        MapSO map = ChooseRandomMap();

        for(int i=0; i<map.Ysize; i++)
        {
            for(int j=0; j<map.Xsize; j++)
            {
                var a = Instantiate(tiles[map.mapTile[i,j]].gameObject, new Vector3(-j, -i, 0), Quaternion.identity);
                a.transform.SetParent(tileContainer);
            }
        }
    }

    private MapSO ChooseRandomMap()
    {
        return mapSo[Random.Range(0, mapSo.Length)];
    }
}
