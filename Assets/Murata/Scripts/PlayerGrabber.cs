using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGrabber : MonoBehaviour
{
    [Header("参照設定")]
    [SerializeField] private PlayerSettings _playerSettings;
    [SerializeField] private InGameData _inGameData;

    [Header("検索設定")]
    [SerializeField] private LayerMask _itemLayer;

    private readonly List<ItemPickup> _pickedUpItems = new();

    /// <summary>
    /// アイテム取得キーの入力を受け取る。
    /// </summary>
    /// <param name="context">入力アクションの情報</param>
    public void OnPickup(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        TryPickupItem();
    }

    /// <summary>
    /// アイテムを取得できるゲーム進行状態か確認する。
    /// </summary>
    /// <returns>探索中の場合はtrue。</returns>
    private bool CanPickupItem()
    {
        return InGameData.gameState == GameState.Search;
    }

    /// <summary>
    /// 最も近い未取得アイテムを拾い、InGameDataに反映する。
    /// </summary>
    private void TryPickupItem()
    {
        if (_playerSettings == null || _inGameData == null)
        {
            Debug.LogError("PlayerGrabberの参照が設定されていません。", this);
            return;
        }

        if (!CanPickupItem())
        {
            return;
        }

        ItemPickup nearestItem = FindNearestItem();

        if (nearestItem == null)
        {
            return;
        }

        ItemData itemData = nearestItem.Data;

        _inGameData.playerTakeItem = itemData;

        _inGameData.isTake = true;

        nearestItem.PickUp();

        _pickedUpItems.Add(nearestItem);
    }

    /// <summary>
    /// 検索範囲内にある最も近い未取得アイテムを検索する。
    /// </summary>
    /// <returns>最も近いアイテム。存在しない場合はnull。</returns>
    private ItemPickup FindNearestItem()
    {
        return Physics.OverlapSphere(
                transform.position,
                _playerSettings.PickupRadius,
                _itemLayer,
                QueryTriggerInteraction.Collide)
            .Select(collider =>
                collider.GetComponentInParent<ItemPickup>())
            .Where(item =>
                item != null && !item.IsPickedUp)
            .Distinct()
            .OrderBy(item =>
                (item.transform.position -
                 transform.position).sqrMagnitude)
            .FirstOrDefault();
    }

    /// <summary>
    /// ラウンド中に取得したアイテムの取得状態をリセットする。
    /// </summary>
    public void ResetPickupItems()
    {
        foreach (ItemPickup item in _pickedUpItems)
        {
            if (item != null)
            {
                item.ResetPickup();
            }
        }

        _pickedUpItems.Clear();

        if (_inGameData != null)
        {
            _inGameData.playerTakeItem = default;
        }

        _inGameData.isTake = false;
    }

    /// <summary>
    /// Sceneビューにアイテムの検索範囲を表示する。
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if (_playerSettings == null)
        {
            return;
        }

        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(
            transform.position,
            _playerSettings.PickupRadius);
    }
}