using UnityEngine;

public class ItemTransformStarter : MonoBehaviour
{
    [SerializeField] private ItemTransformer _itemTransformer;
    [SerializeField] private InGameData _inGameData;

    public void TransformItem(ItemData itemData)
    {
        TransformItems(_inGameData.area1Item);
    }

    private void TransformItems(
        System.Collections.Generic.List<ItemData> itemList)
    {
        foreach (ItemData itemData in itemList)
        {
            if (itemData == null)
            {
                continue;
            }

            _itemTransformer.TransformItem(itemData);
        }
    }
}

