using UnityEngine;
using System.Collections;
using UnityEditor;

public class CreateNewLevel {
    [MenuItem("Assets/Create/Inventory Item List")]
    public static LevelList Create()
    {
        LevelList asset = ScriptableObject.CreateInstance<LevelList>();

        AssetDatabase.CreateAsset(asset, "Assets/Levels/LevelList.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }

    public static Level CreateLevel(int levelNumber)
    {
        Level asset = ScriptableObject.CreateInstance<Level>();

        AssetDatabase.CreateAsset(asset, "Assets/Levels/Level " + levelNumber.ToString() + ".asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}