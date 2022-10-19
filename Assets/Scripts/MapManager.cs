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

        for(int i=0; i<3; i++)
        {
            for(int j=0; j<3; j++)
            {
                var a = Instantiate(tiles[0].gameObject, new Vector3(j, -i, 0), Quaternion.identity);
                a.transform.SetParent(tileContainer);
            }
        }
    }

    private MapSO ChooseRandomMap()
    {
        return mapSo[Random.Range(0, mapSo.Length)];
    }
}
