using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Level : ScriptableObject
{

    public int LevelNumber;
    public Texture2D levelIcon;
    public string LevelData;
    public float Speed;

}
