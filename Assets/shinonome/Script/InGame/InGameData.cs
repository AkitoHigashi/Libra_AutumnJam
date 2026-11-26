using System.Collections.Generic;
using UnityEngine;

public class InGameData : MonoBehaviour
{
    public GameState gameState = GameState.None;
    public int life = 3;
    public int point;
    public List<GameObject> area1Items = new ();
    public List<GameObject> area2Items = new();
    public List<GameObject> area3Items = new ();
}

public enum GameState
{
    None,
    CountDown,
    Walk,
    Instructions,
    Search
}
