using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static string playerName;
    // Start is called before the first frame update
    void Start()
    {
        if (_saveData == null)
            Load();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        Save();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("main");
    }

    public void SetName(string name)
    {
        playerName = name;
    }
    public static void TrySetHighScore(int newScore)
    {
        if(newScore > _saveData.highScore)
        {
            _saveData.highScore = newScore;
            _saveData.highScorer = playerName;
            Save();
        }
    }

    public static void Save()
    {
        File.WriteAllText(saveFile, JsonUtility.ToJson(_saveData));
    }
    public static void Load()
    {
        if (File.Exists(saveFile))
            _saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveFile));
        else
            _saveData = new();
    }

    public static void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit()
#endif
    }

    [Serializable]
    public class SaveData
    {
        public string highScorer = "";
        public int highScore = 0;
    }

    public static SaveData _saveData = null;
    static string saveFile { get => $"{Application.dataPath}/save.json"; }
}
