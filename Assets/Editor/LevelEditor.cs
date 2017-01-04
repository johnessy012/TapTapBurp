using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class InventoryItemEditor : EditorWindow
{

    public LevelList levelList;
    private int viewIndex = 1;
    private GameObject _lastCreatedObject;
    public LevelCreator levelCreator;
    private string lastLoadedLevel;

    private List<GameObject> levelParts;

    [MenuItem("Appatier/Level Editor %#e")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(InventoryItemEditor));
    }

    void OnEnable()
    {
        if (EditorPrefs.HasKey("ObjectPath"))
        {
            string objectPath = EditorPrefs.GetString("ObjectPath");
            levelList = AssetDatabase.LoadAssetAtPath(objectPath, typeof(LevelList)) as LevelList;
        }
        levelCreator = FindObjectOfType<LevelCreator>();
    }

    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Level Editor", EditorStyles.boldLabel);
        if (levelList != null)
        {
            if (GUILayout.Button("Show Level List"))
            {
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = levelList;
            }
        }
        if (GUILayout.Button("Open Level List"))
        {
            Openlevels();
        }
        if (GUILayout.Button("New Level List"))
        {
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = levelList;
        }

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Show Created Level"))
        {
            CreateLevel();
        }
        GUILayout.EndHorizontal();

        if (levelList == null)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            if (GUILayout.Button("Create New Level List", GUILayout.ExpandWidth(false)))
            {
                CreateNewlevels();
            }
            if (GUILayout.Button("Open Existing Level List", GUILayout.ExpandWidth(false)))
            {
                Openlevels();
            }
            GUILayout.EndHorizontal();
        }

        GUILayout.Space(20);

        if (levelList != null)
        {
            GUILayout.BeginHorizontal();

            GUILayout.Space(10);

            if (GUILayout.Button("Prev", GUILayout.ExpandWidth(false)))
            {
                if (viewIndex > 1)
                    viewIndex--;
            }
            GUILayout.Space(5);
            if (GUILayout.Button("Next", GUILayout.ExpandWidth(false)))
            {
                if (viewIndex < levelList.levels.Count)
                {
                    viewIndex++;
                }
            }

            GUILayout.Space(60);

            if (GUILayout.Button("Add Level", GUILayout.ExpandWidth(false)))
            {
                AddItem();
            }
            if (GUILayout.Button("Delete Level", GUILayout.ExpandWidth(false)))
            {
                DeleteItem(viewIndex - 1);
            }

            GUILayout.EndHorizontal();
            if (levelList.levels == null)
                Debug.Log("wtf");
            if (levelList.levels.Count > 0)
            {
                GUILayout.BeginHorizontal();
                viewIndex = Mathf.Clamp(EditorGUILayout.IntField("Current Level", viewIndex, GUILayout.ExpandWidth(false)), 1, levelList.levels.Count);
                //Mathf.Clamp (viewIndex, 1, inventorylevels.levels.Count);
                EditorGUILayout.LabelField("of   " + levelList.levels.Count.ToString() + "  Levels", "", GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal();

                levelList.levels[viewIndex - 1].LevelData = EditorGUILayout.TextField("Level Data", levelList.levels[viewIndex - 1].LevelData as string);
                if (lastLoadedLevel != levelList.levels[viewIndex - 1].LevelData)
                {
                    lastLoadedLevel = levelList.levels[viewIndex - 1].LevelData;
                    if (!string.IsNullOrEmpty(levelList.levels[viewIndex - 1].LevelData))
                        CreateLevel();
                }
                levelList.levels[viewIndex - 1].LevelNumber = EditorGUILayout.IntField(levelList.levels[viewIndex - 1].LevelNumber);
                levelList.levels[viewIndex - 1].levelIcon = EditorGUILayout.ObjectField("Level Icon", levelList.levels[viewIndex - 1].levelIcon, typeof(Texture2D), false) as Texture2D;

                GUILayout.Space(10);

            }
            else
            {
                GUILayout.Label("This Level List is Empty.");
            }
        }
        if (GUI.changed)
        {
            EditorUtility.SetDirty(levelList);
        }
    }

    void CreateLevel()
    {
        if (levelCreator == null)
        {
            Debug.Log("Resigning the level creator reference...");
            levelCreator = FindObjectOfType<LevelCreator>();
        }
        foreach (Transform go in levelCreator.blockPositions)
        {
            if(go != null)
                DestroyImmediate(go.gameObject);
        }
        levelCreator.ResetLevelStartingPosition();
        levelCreator.blockPositions.Clear();
        levelCreator.CreateLevel(levelList.levels[viewIndex - 1].LevelData);
    }

    void CreateNewlevels()
    {
        // There is no overwrite protection here!
        // There is No "Are you sure you want to overwrite your existing object?" if it exists.
        // This should probably get a string from the user to create a new name and pass it ...
        viewIndex = 1;
        levelList = CreateNewLevel.Create();
        if (levelList)
        {
            levelList.levels = new List<Level>();
            string relPath = AssetDatabase.GetAssetPath(levelList);
            EditorPrefs.SetString("ObjectPath", relPath);
        }
    }

    void Openlevels()
    {
        string absPath = EditorUtility.OpenFilePanel("Select Level Item List", "", "");
        if (absPath.StartsWith(Application.dataPath))
        {
            string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
            levelList = AssetDatabase.LoadAssetAtPath(relPath, typeof(LevelList)) as LevelList;
            if (levelList.levels == null)
                levelList.levels = new List<Level>();
            if (levelList)
            {
                EditorPrefs.SetString("ObjectPath", relPath);
            }
        }
    }

    void AddItem()
    {
        Level newItem = CreateNewLevel.CreateLevel(levelList.levels.Count + 1);
        newItem.LevelNumber = 9999;
        levelList.levels.Add(newItem);
        viewIndex = levelList.levels.Count;
    }

    void DeleteItem(int index)
    {
        levelList.levels.RemoveAt(index);
    }
}