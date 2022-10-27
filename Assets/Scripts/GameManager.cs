using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField]
    private GameData gameData = null;

    public GameData CurrentGameData { get { return gameData; } set { gameData = value; } }

    public List<AttackSO> attackSOList;
    public List<ItemSO> itemSOList;

    public Camera mainCam;

    private void Awake()
    {
        mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
}
