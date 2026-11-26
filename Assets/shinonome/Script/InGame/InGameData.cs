using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// インゲーム中の進行状態、スコア、ライフ、エリアごとのアイテム配置など共有データを保持するクラス
/// </summary>
public class InGameData : MonoBehaviour
{
    /// <summary>現在のインゲーム進行ステート</summary>
    public static GameState gameState = GameState.None;

    /// <summary>残りライフ（間違えられる回数）</summary>
    public int life = 3;

    /// <summary>現在の合計得点</summary>
    public int sumPoint = 0;

    /// <summary>現在の最高得点</summary>
    public int nowMaxPoint;

    [Header("エリア別アイテムスポーン位置")]
    /// <summary>エリア1のアイテム設置位置リスト</summary>
    public List<Transform> area1ItemsPos = new();
    /// <summary>エリア2のアイテム設置位置リスト</summary>
    public List<Transform> area2ItemsPos = new();
    /// <summary>エリア3のアイテム設置位置リスト</summary>
    public List<Transform> area3ItemsPos = new();

    [Header("エリア別生成済みアイテム")]
    /// <summary>エリア1に配置されたアイテムのリスト</summary>
    public List<ItemData> area1Item = new();
    /// <summary>エリア2に配置されたアイテムのリスト</summary>
    public List<ItemData> area2Item = new();
    /// <summary>エリア3に配置されたアイテムのリスト</summary>
    public List<ItemData> area3Item = new();

    [Header("プレイヤー・お題情報")]
    /// <summary>プレイヤーのスタート位置（リセット位置）</summary>
    public Transform playerStartPos;
    /// <summary>プレイヤーが選択・取得したアイテム</summary>
    public ItemData playerTakeItem;
    /// <summary>今回のお題として抽選されたターゲットアイテム</summary>
    public ItemData picUpItem;

    [Header("時間・進行フラグ")]
    /// <summary>アイテム配置を覚えて練り歩ける制限時間（秒）</summary>
    public float WalkTime = 20;
    /// <summary>お題アイテムを探せる制限時間（秒）</summary>
    public float SearchTime = 10;
    /// <summary>現在のフェーズでの経過時間（秒）</summary>
    public float nowSearchTime;
    /// <summary>正解した合計回数</summary>
    public int trueIntsum;
    /// <summary>現在プレイヤーがアイテムを取得済みかどうか</summary>
    public bool isTake;
}

/// <summary>
/// インゲームの進行フェーズを表す列挙型
/// </summary>
public enum GameState
{
    /// <summary>未開始・待機状態</summary>
    None,
    /// <summary>ラウンド開始前のカウントダウン</summary>
    CountDown,
    /// <summary>エリアを練り歩いてアイテムを覚えるフェーズ</summary>
    Walk,
    /// <summary>探すべきお題アイテムの抽選・発表フェーズ</summary>
    lottery,
    /// <summary>探索後の正解発表・スコア判定フェーズ</summary>
    Instructions,
    /// <summary>お題アイテムを探す探索フェーズ</summary>
    Search,
    /// <summary>ゲーム終了後のリザルト移行フェーズ</summary>
    StartResult
}