
using UnityEngine;

public class ItemTransformer : MonoBehaviour
{
    // Inspectorから設定する、共通の見た目のモデル
    [SerializeField] private GameObject _commonItemModel;

    // 渡されたアイテムの見た目を変えるメソッド
    public void TransformItem(ItemData itemData)
    {
        // アイテムまたは共通モデルが設定されていない場合は処理を終了する
        if (itemData == null || _commonItemModel == null)
        {
            return;
        }

        // 共通モデルを生成する
        // 元のアイテムと同じ位置・回転に配置し、元のアイテムの子にする
        GameObject commonModel = Instantiate(
            _commonItemModel,
            itemData.transform.position,
            itemData.transform.rotation,
            itemData.transform
        );

        // 共通モデルのローカル位置を原点にする
        commonModel.transform.localPosition = Vector3.zero;

        // 共通モデルのローカル回転を初期状態にする
        commonModel.transform.localRotation = Quaternion.identity;
    }
}

