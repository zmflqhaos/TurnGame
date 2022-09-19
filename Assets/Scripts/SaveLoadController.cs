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
			Debug.Log("���̺������� ��� ��������ϴ�.");
		}

		string saveJson = JsonUtility.ToJson(GameManager.Instance.CurrentGameData);

		string saveFilePath = SavePath + saveFileName + ".json";
		File.WriteAllText(saveFilePath, saveJson);
		Debug.Log("���̺� �Ϸ�!");
	}

	public static GameData Load(string saveFileName)
	{
		string saveFilePath = SavePath + saveFileName + ".json";

		if (!File.Exists(saveFilePath))
		{
			Debug.LogError("���̺����� ����!");
			Save(saveFileName);
			return null;
		}

		string saveFile = File.ReadAllText(saveFilePath);
		GameData saveData = JsonUtility.FromJson<GameData>(saveFile);
		GameManager.Instance.CurrentGameData = saveData;
		Debug.Log("�ε� �Ϸ�!");
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
