using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerMovementController : MonoBehaviour
{
    private PlayerMovement _playerMovement;

    /// <summary>
    /// プレイヤーの移動コンポーネントを取得する。
    /// </summary>
    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    /// <summary>
    /// ゲーム開始前のプレイヤーの移動を禁止する。
    /// </summary>
    private void OnEnable()
    {
        UpdateMovementState();
    }

    /// <summary>
    /// 現在のゲームステートに応じて移動を制御する。
    /// </summary>
    private void Update()
    {
        UpdateMovementState();
    }

    /// <summary>
    /// ゲーム開始前とカウントダウン中の移動を禁止する。
    /// </summary>
    private void UpdateMovementState()
    {
        bool canMove =
            InGameData.gameState != GameState.None &&
            InGameData.gameState != GameState.CountDown;

        _playerMovement.SetCanMove(canMove);
    }
}