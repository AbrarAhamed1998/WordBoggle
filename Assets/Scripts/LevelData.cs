using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public char[,] boggle;
    public List<Vector2> bugLocations = new List<Vector2>();
    public List<string> wordsInBoggle = new List<string>();
}
