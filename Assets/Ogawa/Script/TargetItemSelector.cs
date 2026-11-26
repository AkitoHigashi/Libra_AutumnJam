
using System.Collections.Generic;
using UnityEngine;

public class TargetItemSelector : MonoBehaviour
{
    // ゲーム中の情報を管理するデータ
    [SerializeField] private InGameData _inGameData;

    // ステージ上に生成されたアイテムを管理するスクリプト
    [SerializeField] private ItemSpawner _itemSpawner;

    // お題となるアイテムをランダムに選ぶメソッド
    public void SelectTargetItem()
    {
        // 必要なデータが設定されていない場合は処理を終了する
        if (_inGameData == null || _itemSpawner == null)
        {
            return;
        }

        // お題の抽選候補となるアイテムを入れるリスト
        List<ItemData> availableItems = new List<ItemData>();

        // ステージ上に生成されているアイテムを1つずつ確認する
        foreach (ItemData item in _itemSpawner.SpawnedItems)
        {
            // アイテムが存在している場合
            if (item != null)
            {
                // 抽選候補のリストに追加する
                availableItems.Add(item);
            }
        }

        // 抽選できるアイテムが1つもない場合
        if (availableItems.Count == 0)
        {
            // お題のアイテムを空にする
            _inGameData.picUpItem = null;

            // これ以上の抽選を行わずに処理を終了する
            return;
        }

        // 抽選候補の中からランダムな番号を選ぶ
        int randomIndex = Random.Range(0, availableItems.Count);

        // 選ばれたアイテムを次のお題として保存する
        _inGameData.picUpItem = availableItems[randomIndex];
    }
}



