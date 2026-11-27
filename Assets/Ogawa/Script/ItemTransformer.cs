
using UnityEngine;
using System.Collections.Generic;

public class ItemTransformer : MonoBehaviour
{
    // Inspectorから設定する、共通の見た目のモデル
    [SerializeField] private GameObject _commonItemModel;

    public void TransformItems
        (List<ItemData> itemDataList)
    {
        // 共通モデルが設定されていない場合は処理を終了
        if (_commonItemModel == null || itemDataList == null)
        {
            return;
        }
        foreach (ItemData itemData in itemDataList)
        {
            // アイテムがnullの場合はスキップする
            if (itemData == null)
            {
                continue;
            }

            // 共通モデルを生成する
            GameObject commonModel = Instantiate(
                _commonItemModel,
                itemData.transform.position,
                itemData.transform.rotation,
                itemData.transform
            );

            // ローカル位置を原点にする
            commonModel.transform.localPosition = Vector3.zero;

            // ローカル回転を初期状態にする
            commonModel.transform.localRotation = Quaternion.identity;
        }
    }
}


