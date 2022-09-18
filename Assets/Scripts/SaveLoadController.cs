using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveSystem
{
	private static string SavePath => Application.dataPath + "/Save/";

	public static void Save(GameData saveData, string saveFileName)
	{
		if (!Directory.Exists(SavePath))
		{
			DefaultDataSetting(saveData);
		}

		string saveJson = JsonUtility.ToJson(saveData);

		string saveFilePath = SavePath + saveFileName + ".json";
		File.WriteAllText(saveFilePath, saveJson);
		Debug.Log("Save Success: " + saveFilePath);
	}

	public static GameData Load(string saveFileName)
	{
		string saveFilePath = SavePath + saveFileName + ".json";

		if (!File.Exists(saveFilePath))
		{
			DefaultDataSetting(GameManager.Instance.CurrentGameData);
		}

		string saveFile = File.ReadAllText(saveFilePath);
		GameData saveData = JsonUtility.FromJson<GameData>(saveFile);
		return saveData;
	}

	public static void DefaultDataSetting(GameData saveData)
    {
		Directory.CreateDirectory(SavePath);

		//여기 4 값을 플레이어 참가 수 만큼으로 바꿔야함
		saveData._playersData = new List<PlayerData>(new PlayerData[4]);

		for(int i = 0; i< saveData._playersData.Count-1; i++)
        {
			saveData._playersData[i]._name = "player1";
			saveData._playersData[i]._playerStat.SetStatAmount(Stat.Type.VIT, 5);
			saveData._playersData[i]._playerStat.SetStatAmount(Stat.Type.MGI, 5);
			saveData._playersData[i]._playerStat.SetStatAmount(Stat.Type.STR, 5);
			saveData._playersData[i]._playerStat.SetStatAmount(Stat.Type.DEX, 5);
			saveData._playersData[i]._playerStat.SetStatAmount(Stat.Type.INT, 5);
			saveData._playersData[i]._playerStat.SetStatAmount(Stat.Type.CHA, 5);
		}

	}
}

public class SaveLoadController : MonoBehaviour
{
    private void Awake()
    {
		SaveSystem.Load("Player1Save");
	}
    void Update()
	{
		if (Input.GetKeyDown(KeyCode.S))
		{
			SaveSystem.Save(GameManager.Instance.CurrentGameData, "Player1Save");
		}

		if (Input.GetKeyDown(KeyCode.L))
		{
			GameData loadData = SaveSystem.Load("Player1Save");
			Debug.Log(string.Format("LoadData Result => name : {0}, hp : {1}, mp : {2}", loadData._playersData[0]._name, loadData._playersData[0]._playerStat.GetStatAmount(Stat.Type.VIT), loadData._playersData[0]._playerStat.GetStatAmount(Stat.Type.MGI)));
		}
	}
}
