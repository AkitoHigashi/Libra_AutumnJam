using System.Collections.Generic;
using UnityEngine;

public class ItemTransformer : MonoBehaviour
{
    [SerializeField] private GameObject _commonItemModel;

    // 元のメッシュの表示状態を保存する
    private Dictionary<ItemData, Renderer[]> _itemRenderers = new();
    private Dictionary<Renderer, bool> _rendererStates = new();

    public void TransformItems()
    {
        if (_commonItemModel == null)
        {
            Debug.LogWarning("共通モデルが設定されていません");
            return;
        }

        ItemData[] itemDataList =
            FindObjectsByType<ItemData>(FindObjectsSortMode.None);

        foreach (ItemData itemData in itemDataList)
        {
            if (itemData == null)
            {
                continue;
            }

            // すでに変換済みならスキップ
            if (itemData.transform.Find(_commonItemModel.name) != null)
            {
                continue;
            }

            // 元のアイテムのメッシュを取得
            Renderer[] renderers =
                itemData.GetComponentsInChildren<Renderer>();

            _itemRenderers[itemData] = renderers;

            foreach (Renderer renderer in renderers)
            {
                // 元の表示状態を保存
                _rendererStates[renderer] = renderer.enabled;

                // 元のメッシュを非表示
                renderer.enabled = false;
            }

            // 共通モデルを生成
            GameObject commonModel = Instantiate(
                _commonItemModel,
                itemData.transform
            );

            commonModel.transform.localPosition = Vector3.zero;
            commonModel.transform.localRotation = Quaternion.identity;
        }

        Debug.Log($"アイテム変換完了：{itemDataList.Length}個");
    }

    // 正誤判定時に元のメッシュを表示
    public void RestoreItemMesh(ItemData itemData)
    {
        if (itemData == null ||
            !_itemRenderers.ContainsKey(itemData))
        {
            return;
        }

        Renderer[] renderers = _itemRenderers[itemData];

        foreach (Renderer renderer in renderers)
        {
            if (renderer == null)
            {
                continue;
            }

            // 保存していた表示状態に戻す
            if (_rendererStates.TryGetValue(renderer, out bool wasEnabled))
            {
                renderer.enabled = wasEnabled;
            }
        }

        // 復元済みのデータを削除
        _itemRenderers.Remove(itemData);

        foreach (Renderer renderer in renderers)
        {
            if (renderer != null)
            {
                _rendererStates.Remove(renderer);
            }
        }
    }
}

