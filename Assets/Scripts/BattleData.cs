using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BattleData : MonoSingleton<BattleData>
{
    public Camera mainCam;
    public Vector3Int[] vecOne;
    // = { new Vector3Int(0, 1, 0), new Vector3Int(-1, 0, 0), new Vector3Int(0, -1, 0), new Vector3Int(1, 0, 0) }

    public Tilemap[] mapList;
    public int mapNumber;
    public float mapScale;

    public IntVec2[] mapSizes;
    public IntVec2 mapSize;

    public List<List<Vector3Int>> mapBox = new List<List<Vector3Int>>();

    private void Awake()
    {
        mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        for (int i = 0; i < mapList.Length; i++)
        {
            if (mapList[i].gameObject.activeSelf)
            {
                mapScale = mapList[i].transform.localScale.x;
                mapNumber = i;
                mapSize = mapSizes[i];
                break;
            }
        }

        for (int i = 0; i < mapSize.y; i++)
        {
            mapBox.Add(new List<Vector3Int>());
            for (int j = 0; j < mapSize.x; j++)
            {
                mapBox[i].Add(new Vector3Int(j, i));
            }
        }
    }
}
