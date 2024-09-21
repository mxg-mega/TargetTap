
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private string saveFilePath;
    public static DataManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        saveFilePath = Application.persistentDataPath + "/gameData.json";
    }

    public void SaveToJson(int highScore)
    {
        GameData gameData = new GameData();

        gameData.highscore = highScore;
        string json = JsonUtility.ToJson(gameData);

        File.WriteAllText(saveFilePath, json);
    }

    public GameData LoadJson()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            GameData gameData = JsonUtility.FromJson<GameData>(json);
            return gameData;
        }
        else
        {
            return new GameData();
        }
    }

}
