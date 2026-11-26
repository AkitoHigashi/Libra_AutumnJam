using System.Collections.Generic;
using UnityEngine;

public class TargetItemSelector : MonoBehaviour
{
    [SerializeField] private InGameData _inGameData;

    public void SelectTargetItem()
    {
        if (_inGameData == null)
        {
            return;
        }

        List<ItemData> allItems = new List<ItemData>();

        allItems.AddRange(_inGameData.area1Item);
        allItems.AddRange(_inGameData.area2Item);
        allItems.AddRange(_inGameData.area3Item);

        if (allItems.Count == 0)
        {
            return;
        }

        int randomIndex = Random.Range(0, allItems.Count);

        _inGameData.picUpItem = allItems[randomIndex];
    }
}

