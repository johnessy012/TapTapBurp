using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class LevelJsonData {

    public static string levelPath = Application.dataPath + "/Resources/Levels/leveldata.json";

    public static void CreateTemplateData(Levels levelData)
    {
        if (!File.Exists(levelPath))
        {
            Debug.LogWarning("There is no file here so we should create one");
            StreamWriter sw = File.CreateText(levelPath);
            sw.Close();
        }
        StreamWriter nsw = new StreamWriter(levelPath);
        levelData.levels[0].level = "RRFFEEPPTT";
        levelData.levels[0].levelNumber = 1;
        string level = JsonUtility.ToJson(levelData, true);
        nsw.Write(level);
        nsw.Close();
    }

    // This allows us to load from Json with a generic method. 
    public static Levels LoadData(Levels levelData)
    {
        string data = File.ReadAllText(levelPath);
        Debug.Log(data);
        return levelData = JsonUtility.FromJson<Levels>(data);
    }

    public static void LoadData(LevelData level, ScriptableObject levelData)
    {
    }
}
