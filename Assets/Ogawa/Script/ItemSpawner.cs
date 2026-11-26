using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private InGameData _inGameData;
    [SerializeField] private ItemTransformer _itemTransformer;

    private void Start()
    {
        SpawnItems();
    }

    private void SpawnItems()
    {
        SpawnArea(
            _inGameData.area1ItemsPos,
            _inGameData.area1Item
        );

        SpawnArea(
            _inGameData.area2ItemsPos,
            _inGameData.area2Item
        );

        SpawnArea(
            _inGameData.area3ItemsPos,
            _inGameData.area3Item
        );
    }

    private void SpawnArea(
        System.Collections.Generic.List<Transform> spawnPositions,
        System.Collections.Generic.List<ItemData> itemList)
    {
        if (spawnPositions.Count == 0 || itemList.Count == 0)
        {
            return;
        }

        foreach (Transform spawnPosition in spawnPositions)
        {
            int randomIndex = Random.Range(0, itemList.Count);

            ItemData selectedItem = itemList[randomIndex];

            ItemData spawnedItem = Instantiate(
                selectedItem,
                spawnPosition.position,
                spawnPosition.rotation
            );

            _itemTransformer.TransformItem(spawnedItem);
        }
    }
}
