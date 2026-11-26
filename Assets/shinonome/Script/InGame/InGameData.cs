using System.Collections.Generic;
using UnityEngine;

public class InGameData : MonoBehaviour
{
    public GameState gameState = GameState.None;
    public int life = 3;
    public int sumPoint;
    public List<Transform> area1ItemsPos = new ();
    public List<Transform> area2ItemsPos = new();
    public List<Transform> area3ItemsPos = new();
    public List<ItemData> area1Item = new ();
    public List <ItemData> area2Item = new ();
    public List<ItemData> area3Item = new ();
    public Transform playerStartPos;
    public ItemData picUpItem;//抽選されたアイテム
}

public enum GameState
{
    None,
    CountDown,
    Walk,
    Instructions,
    Search
}
