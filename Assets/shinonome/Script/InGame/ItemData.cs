using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// フィールド上に配置される各アイテムオブジェクトにアタッチし、見た目・隠蔽用カバー・基本情報を管理するクラス
/// </summary>
public class ItemData : MonoBehaviour
{
    /// <summary>アイテムを隠す際に被せるブラックボックスオブジェクト</summary>
    public GameObject cover;

    /// <summary>このアイテムの名称・ポイント・UI画像などの基本データ</summary>
    public ItemBase item;
}

/// <summary>
/// アイテム1つあたりの基本スペック（オブジェクト・得点・名前・UIアイコン）をまとめた構造体
/// </summary>
[System.Serializable]
public struct ItemBase
{
    /// <summary>アイテムの3D/2Dゲームオブジェクト本体</summary>
    public GameObject itemOBJ;

    /// <summary>アイテム正解時に加算（または時間切れ時に減算）される基準ポイント</summary>
    [field: SerializeField] public int point { get; private set; }

    /// <summary>アイテムの表示名</summary>
    public string name;

    /// <summary>お題表示などのUIに使用するアイテム画像</summary>
    public Image itemImage;
}