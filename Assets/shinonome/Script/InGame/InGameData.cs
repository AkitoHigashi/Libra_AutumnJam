using System.Collections.Generic;
using UnityEngine;

public class InGameData : MonoBehaviour
{
    public static GameState gameState = GameState.None;//InGameState
    public int life = 3;//間違えられる回数
    public int sumPoint = 0;//得点の合計
    public int nowMaxPoint;//現在の最高得点
    public List<Transform> area1ItemsPos = new ();//エリア1のアイテムの設置位置のリスト
    public List<Transform> area2ItemsPos = new();//エリア2のアイテムの設置位置のリスト
    public List<Transform> area3ItemsPos = new();//エリア3のアイテムの設置位置のリスト
    public List<ItemData> area1Item = new ();//エリア1のアイテムのリスト
    public List <ItemData> area2Item = new ();//エリア2のアイテムのリスト
    public List<ItemData> area3Item = new ();//エリア3のアイテムのリスト
    public Transform playerStartPos;//プレイヤーのスタート位置(リセット位置)
    public ItemData playerTakeItem;//プレイヤーが選んだアイテム
    public ItemData picUpItem;//抽選されたアイテム
    public float WalkTime = 20;//練り歩ける時間
    public float SearchTime = 10;//探せる時間
    public float NoeSearchTime;
}

public enum GameState
{
    None,
    CountDown,
    Walk,
    Instructions,
    Search
}
