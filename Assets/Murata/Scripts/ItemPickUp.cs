using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private ItemData _itemData;

    private bool _isPickedUp;

    public ItemData Data => _itemData;
    public bool IsPickedUp => _isPickedUp;

    /// <summary>
    /// アイテムを取得済みにする。
    /// </summary>
    public void PickUp()
    {
        _isPickedUp = true;
    }

    /// <summary>
    /// アイテムの取得状態をリセットする。
    /// </summary>
    public void ResetPickup()
    {
        _isPickedUp = false;
    }
}