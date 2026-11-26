
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private InGameData _inGameData;

    private void Start()
    {
        SpawnItems();
    }

    private void SpawnItems()
    {
        // エリア1のアイテムをスポーン
        SpawnArea
       (
            _inGameData.area1ItemsPos,
            _inGameData.area1Item
        );

        // エリア2のアイテムをスポーン
        SpawnArea
       (
            _inGameData.area2ItemsPos,
            _inGameData.area2Item
        );

        // エリア3のアイテムをスポーン
        SpawnArea
       (
            _inGameData.area3ItemsPos,
            _inGameData.area3Item
        );
    }

    private void SpawnArea
    (
        System.Collections.Generic.List<Transform> spawnPositions,
        System.Collections.Generic.List<ItemData> itemList)
    {
        if (spawnPositions.Count == 0 || itemList.Count == 0)
        {
            return;
        }

        foreach 
         (Transform spawnPosition in spawnPositions)
        {
            int randomIndex = Random.Range(0, itemList.Count);

            ItemData selectedItem = itemList[randomIndex];

            Instantiate
           (
                selectedItem,
                spawnPosition.position,
                spawnPosition.rotation
            );
        }
    }
}
