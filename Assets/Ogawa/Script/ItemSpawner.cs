using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    // エリアの配置場所やアイテム情報を管理するデータ
    [SerializeField] private InGameData _inGameData;

    // 実際にステージ上に生成したアイテムを保存するリスト
    public List<ItemData> SpawnedItems { get; private set; }
        = new List<ItemData>();

    // ゲーム開始時にアイテムを生成する
    private void Start()
    {
        SpawnItems();
    }

    // すべてのエリアのアイテムを生成する
    private void SpawnItems()
    {
        // すでに選ばれたアイテムを記録する
        // 同じアイテムが重複して生成されないようにする
        HashSet<ItemData> usedItems = new HashSet<ItemData>();

        // エリア1のアイテムを生成する
        SpawnArea(_inGameData.area1ItemsPos,
            _inGameData.area1Item, usedItems);

        // エリア2のアイテムを生成する
        SpawnArea(_inGameData.area2ItemsPos,
            _inGameData.area2Item, usedItems);

        // エリア3のアイテムを生成する
        SpawnArea(_inGameData.area3ItemsPos,
            _inGameData.area3Item, usedItems);
    }

    // 指定されたエリアにアイテムを生成する
    private void SpawnArea(
        List<Transform> spawnPositions,
        List<ItemData> itemList,
        HashSet<ItemData> usedItems)
    {
        // エリア内の配置場所を1つずつ確認する
        foreach (Transform spawnPosition in spawnPositions)
        {
            // 配置場所が設定されていない場合は次の場所へ進む
            if (spawnPosition == null)
            {
                continue;
            }

            // まだ使用されていないアイテムを入れるリスト
            List<ItemData> availableItems = new List<ItemData>();

            // エリアに登録されているアイテムを1つずつ確認する
            foreach (ItemData item in itemList)
            {
                // アイテムが存在し、まだ使用されていない場合
                if (item != null && !usedItems.Contains(item))
                {
                    // 抽選候補のリストに追加する
                    availableItems.Add(item);
                }
            }

            // 抽選できるアイテムがない場合は次の配置場所へ進む
            if (availableItems.Count == 0)
            {
                continue;
            }

            // 抽選候補の中からランダムな番号を選ぶ
            int randomIndex = Random.Range(0, availableItems.Count);

            // 選ばれた番号のアイテムを取得する
            ItemData selectedItem = availableItems[randomIndex];

            // 選ばれたアイテムを使用済みとして記録する
            usedItems.Add(selectedItem);

            // 選ばれたアイテムを配置場所に生成する
            ItemData spawnedItem = Instantiate(
                selectedItem,
                spawnPosition.position,
                spawnPosition.rotation
            );

            // 生成したアイテムを管理用リストに追加する
            SpawnedItems.Add(spawnedItem);
        }
    }

    // 生成済みアイテムを管理リストから取り除く
    public void RemoveSpawnedItem(ItemData item)
    {
        SpawnedItems.Remove(item);
    }
}
