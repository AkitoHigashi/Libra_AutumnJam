using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの名前とスコアの組み合わせを静的リストで管理・保持するクラス
/// </summary>
public class ScoreManager : MonoBehaviour
{
    /// <summary>
    /// プレイヤー名とスコアのデータを格納する静的リスト（シーンをまたいで参照可能）
    /// </summary>
    public static new List<ScoreAndName> scoreAndPlayerNameList = new();
    /// <summary>
    /// 新規のプレイヤー名とスコアのデータを格納する静的リスト（シーンをまたいで参照可能）
    /// </summary>
    public static ScoreAndName newPlayerData;
}

/// <summary>
/// 1プレイヤー分の名前とスコアをセットで保持する構造体
/// </summary>
public struct ScoreAndName
{
    /// <summary>プレイヤーの名前</summary>
    public string PlayerName;

    /// <summary>記録されたスコア</summary>
    public float Score;
}