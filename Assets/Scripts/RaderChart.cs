using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debuger;

public class RaderChart : MonoBehaviour
{
    [SerializeField] private StatRadarChart _statRadarChart = null;
    private GameData gameData;
    void Start()
    {
        gameData = GameManager.Instance.CurrentGameData;
        _statRadarChart.SetStats();

        DeBuger.CreateButton(new Vector2(800, 600), "hp++", new Vector2(80, 40), () =>
        {
            gameData._playersData[0]._playerStat.IncStatAmount(Stat.Type.VIT);
        });

        DeBuger.CreateButton(new Vector2(700, 600), "hp--", new Vector2(80, 40), () =>
        {
            gameData._playersData[0]._playerStat.DecStatAmount(Stat.Type.VIT);
        });

        DeBuger.CreateButton(new Vector2(800, 550), "mp++", new Vector2(80, 40), () =>
        {
            gameData._playersData[0]._playerStat.IncStatAmount(Stat.Type.MGI);
        });

        DeBuger.CreateButton(new Vector2(700, 550), "mp--", new Vector2(80, 40), () =>
        {
            gameData._playersData[0]._playerStat.DecStatAmount(Stat.Type.MGI);
        });

        DeBuger.CreateButton(new Vector2(600, 600), "str++", new Vector2(80, 40), () =>
        {
            gameData._playersData[0]._playerStat.IncStatAmount(Stat.Type.STR);
        });

        DeBuger.CreateButton(new Vector2(500, 600), "str--", new Vector2(80, 40), () =>
        {
            gameData._playersData[0]._playerStat.DecStatAmount(Stat.Type.STR);
        });

        DeBuger.CreateButton(new Vector2(600, 550), "dex++", new Vector2(80, 40), () =>
        {
            gameData._playersData[0]._playerStat.IncStatAmount(Stat.Type.DEX);
        });

        DeBuger.CreateButton(new Vector2(500, 550), "dex--", new Vector2(80, 40), () =>
        {
            gameData._playersData[0]._playerStat.DecStatAmount(Stat.Type.DEX);
        });
        DeBuger.CreateButton(new Vector2(400, 600), "int++", new Vector2(80, 40), () =>
        {
            gameData._playersData[0]._playerStat.IncStatAmount(Stat.Type.INT);
        });

        DeBuger.CreateButton(new Vector2(300, 600), "int--", new Vector2(80, 40), () =>
        {
            gameData._playersData[0]._playerStat.DecStatAmount(Stat.Type.INT);
        });

        DeBuger.CreateButton(new Vector2(400, 550), "cha++", new Vector2(80, 40), () =>
        {
            gameData._playersData[0]._playerStat.IncStatAmount(Stat.Type.CHA);
        });

        DeBuger.CreateButton(new Vector2(300, 550), "cha--", new Vector2(80, 40), () =>
        {
            gameData._playersData[0]._playerStat.DecStatAmount(Stat.Type.CHA);
        });
    }

}
