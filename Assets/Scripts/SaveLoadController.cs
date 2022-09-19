using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveSystem
{
	private static string SavePath => Application.dataPath + "/Save/";

	public static void Save(string saveFileName)
	{
		if (!Directory.Exists(SavePath))
		{
			Directory.CreateDirectory(SavePath);
			Debug.Log("세이브파일이 없어서 만들었습니다.");
		}

		string saveJson = JsonUtility.ToJson(GameManager.Instance.CurrentGameData);

		string saveFilePath = SavePath + saveFileName + ".json";
		File.WriteAllText(saveFilePath, saveJson);
		Debug.Log("세이브 완료!");
	}

	public static GameData Load(string saveFileName)
	{
		string saveFilePath = SavePath + saveFileName + ".json";

		if (!File.Exists(saveFilePath))
		{
			Debug.LogError("세이브파일 없음!");
			Save(saveFileName);
			return null;
		}

		string saveFile = File.ReadAllText(saveFilePath);
		GameData saveData = JsonUtility.FromJson<GameData>(saveFile);
		GameManager.Instance.CurrentGameData = saveData;
		Debug.Log("로딩 완료!");
		return saveData;
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
			SaveSystem.Save("Player1Save");
		}
	}
}
