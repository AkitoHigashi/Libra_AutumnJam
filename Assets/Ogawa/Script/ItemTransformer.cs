
using UnityEngine;

public class ItemTransformer : MonoBehaviour
{
    // Inspectorから設定する共通モデル
    [SerializeField] private GameObject _commonItemModel;

    // シーン内のすべてのItemDataを変換する
    public void TransformItems()
    {
        if (_commonItemModel == null)
        {
            Debug.LogWarning("共通モデルが設定されていません");
            return;
        }

        // シーン内のItemDataを取得
        ItemData[] itemDataList =
            FindObjectsByType<ItemData>(FindObjectsSortMode.None);

        // 取得したアイテムを1つずつ変換
        foreach (ItemData itemData in itemDataList)
        {
            if (itemData == null)
            {
                continue;
            }

            // すでに変換済みなら都バス
            if (itemData.transform.Find(_commonItemModel.name) != null)
            {
                continue;
            }

            // 共通モデルを生成
            GameObject commonModel = Instantiate(
                _commonItemModel,
                itemData.transform
            );

            // ローカル位置と回転を初期化
            commonModel.transform.localPosition = Vector3.zero;
            commonModel.transform.localRotation = Quaternion.identity;
        }

        Debug.Log($"アイテム変換完了：{itemDataList.Length}個");
    }
}
