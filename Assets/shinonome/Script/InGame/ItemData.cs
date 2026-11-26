using UnityEngine;
using UnityEngine.UI;

public class ItemData : MonoBehaviour
{
    public GameObject cover;//隠すときのブラックボックス
    public ItemBase item;//アイテムの情報群
}

[System.Serializable]
public struct ItemBase
{
    [SerializeField] private GameObject itemOBJ;//ゲームオブジェクト本体
    [field:SerializeField] public int point { get; private set; }//アイテム取得時のポイント
    public string name;//アイテムの名前
    public Image itemImage;//UI表示用の名前
}
