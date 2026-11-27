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
    /// 初期化完了後にプレイヤーの移動状態を設定する。
    /// </summary>
    private void Start()
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