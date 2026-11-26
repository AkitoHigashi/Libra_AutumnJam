using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static new List<ScoreAndName> scoreAndPlayerNameList = new();
}

public struct ScoreAndName
{
    public string PlaterName;
    public float Score;
}

