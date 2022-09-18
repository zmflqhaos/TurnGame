using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField]
    private GameData gameData = null;

    public GameData CurrentGameData { get { return gameData; } }
}
