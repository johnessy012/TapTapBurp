using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
public class LevelCreator : MonoBehaviour {


    public int currentLevel = 1;

    public Levels levelData = new Levels();

    [SerializeField]
    private GameObject _endOfZone;

    [SerializeField]
    private GameObject _forward, _right, _left, _rightCorner, _leftCorner, _rightForward, _leftForward;

    [SerializeField]
    private GameObject _lastCreatedObject;

    [SerializeField]
    private Vector3 _lastBlockPosition;

    public List<Transform> blockPositions;

    private string levelPath;

    private void Start()
    {
        levelData = LevelJsonData.LoadData(levelData);
        CreateLevel(levelData.levels[0].level);
    }

    public void CreatePath(GameObject pathToCreate, Vector3 dir, string name, Vector3 rotation)
    { 
        GameObject go = Instantiate(pathToCreate, _lastBlockPosition += dir, Quaternion.identity) as GameObject;
        Debug.Log(transform.position);
        _lastCreatedObject = go;
        go.name = name;
        go.transform.localEulerAngles = rotation;
        blockPositions.Add(go.transform);
    }

    public void ResetLevelStartingPosition()
    {
        _lastBlockPosition = Vector3.zero;
    }

    

    /// <summary>
    /// R = Right
    /// ) = Right Forward
    /// L = Left
    /// ( = Left Forward
    /// 1 = Forward left
    /// 2 = Forward Right
    /// F = Forward
    /// E = Empty
    /// r = TurnRight
    /// l = TurnLeft
    /// </summary>
    public void CreateLevel(string levelData)
    {
        ///
        /// IF we are going forward then we go right, we need a right corner, if we then go forward strait away we then need to have a forward 
        ///
        foreach (char ch in levelData)
        {
            if (ch == ')')
            {
                Debug.Log("EndOfSequence");
                CreatePath(_rightForward, new Vector3(1,0,0), "RightForward", Vector3.zero);
            }
            if (ch == '(')
            {
                Debug.Log("EndOfSequence");
                CreatePath(_leftForward, new Vector3(-1, 0, 0), "LeftForward", Vector3.zero);
            }
            if (ch == 'F')
            {
                Debug.Log("Forward");
                CreatePath(_forward, new Vector3(0, 0, 1), "Forward", Vector3.zero);
            }
            if (ch == 'E')
            {
                CreatePath(_forward ,new Vector3(0, 0, 2), "Empty", Vector3.zero);
                Debug.Log("Empty Position");
            }

            if (ch == 'R')
            {
                CreatePath(_right, new Vector3(1, 0, 0), "Right", new Vector3(0,90,0));
                Debug.Log("Right");
            }

            if (ch == 'L')
            {
                CreatePath(_left, new Vector3(-1, 0, 0), "Left", new Vector3(0, -90, 0));
                Debug.Log("Left");
            }

            if (ch == '<')
            {
                CreatePath(_leftCorner, new Vector3(0, 0, 1), "Left Turn", new Vector3(0, 90, 0));
                Debug.Log("LEft Turn Position");
            }

            if (ch == '>')
            {
                CreatePath(_rightCorner, new Vector3(0, 0, 1), "Right Turn", new Vector3(0, 90, 0));
                Debug.Log("Right Turn Position");
            }
        }
    }

}

[System.Serializable]
public class LevelData
{
    public int levelNumber;
    public string level;
    public float playerSpeed;
}

[System.Serializable]
public class Levels
{
    public List<LevelData> levels;
}

