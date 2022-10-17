using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleData : MonoSingleton<BattleData>
{
    public Camera mainCam;
    public Vector3Int[] vecOne;
    // = { new Vector3Int(0, 1, 0), new Vector3Int(-1, 0, 0), new Vector3Int(0, -1, 0), new Vector3Int(1, 0, 0) }

    public List<List<Vector3Int>> mapBox = new List<List<Vector3Int>>();
    public List<List<BattleBase>> mapOnChar = new List<List<BattleBase>>();

    private void Awake()
    {
        mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
}
