
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("参照設定")]
    [SerializeField] private PlayerSettings _playerSettings;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private InGameData _inGameData;

    private Rigidbody _rigidbody;
    private Vector2 _moveInput;

    private const float DefaultSpeedMultiplier = 1f;
    private const float ReverseInputMultiplier = -1f;
    private const float MaxInputMagnitude = 1f;

    private float _speedMultiplier = DefaultSpeedMultiplier;

    private bool _isRunning;
    private bool _isInputReversed;
    private bool _canMove = true;

    /// <summary>
    /// Rigidbodyを取得する。
    /// </summary>
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// 物理演算に合わせて移動処理を実行する。
    /// </summary>
    private void FixedUpdate()
    {
        if (!_canMove)
        {
            StopMovement();
            return;
        }

        MovePlayer();
    }

    /// <summary>
    /// 移動入力を取得する。
    /// </summary>
    /// <param name="context">移動入力情報</param>
    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// ダッシュ入力を取得する。
    /// </summary>
    /// <param name="context">ダッシュ入力情報</param>
    public void OnRun(InputAction.CallbackContext context)
    {
        _isRunning = context.ReadValueAsButton();
    }
    /// <summary>
    /// カメラの向きを基準にプレイヤーを移動させる。
    /// </summary>
    private void MovePlayer()
    {
        Vector2 moveInput = _moveInput;

        if (_isInputReversed)
        {
            moveInput *= ReverseInputMultiplier;
        }

        Vector3 cameraForward = Vector3.ProjectOnPlane(
            _cameraTransform.forward,
            Vector3.up).normalized; // カメラの前方向を水平方向に制限する

        Vector3 cameraRight = Vector3.Cross(
            Vector3.up,
            cameraForward).normalized; // カメラの右方向を水平方向に制限する

        Vector3 moveDirection =
            cameraForward * moveInput.y +
            cameraRight * moveInput.x;

        moveDirection = Vector3.ClampMagnitude(
            moveDirection,
            MaxInputMagnitude);

        float speed = _isRunning
            ? _playerSettings.RunSpeed
            : _playerSettings.WalkSpeed;

        speed *= _speedMultiplier;

        Vector3 velocity = moveDirection * speed;

        velocity.y = _rigidbody.linearVelocity.y;

        _rigidbody.linearVelocity = velocity;

        if (moveDirection != Vector3.zero)
        {
            RotatePlayer(moveDirection);
        }
    }

    /// <summary>
    /// プレイヤーを移動方向へ滑らかに回転させる。
    /// </summary>
    /// <param name="moveDirection">移動方向</param>
    private void RotatePlayer(Vector3 moveDirection)
    {
        if (moveDirection == Vector3.zero)
        {
            return;
        }

        Quaternion targetRotation =
            Quaternion.LookRotation(moveDirection);

        Quaternion nextRotation = Quaternion.RotateTowards(
            _rigidbody.rotation,
            targetRotation,
            _playerSettings.RotationSpeed * Time.fixedDeltaTime);

        _rigidbody.MoveRotation(nextRotation);
    }

    /// <summary>
    /// 水平方向の移動を停止する。
    /// </summary>
    private void StopMovement()
    {
        Vector3 velocity = Vector3.zero;

        // 移動停止中も重力の影響を維持する。
        velocity.y = _rigidbody.linearVelocity.y;

        _rigidbody.linearVelocity = velocity;
        _rigidbody.angularVelocity = Vector3.zero;
    }
    /// <summary>
    /// InGameDataに登録された初期位置と向きに
    /// プレイヤーをリセットする。
    /// </summary>
    public void ResetPosition()
    {
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        _rigidbody.position = _inGameData.playerStartPos.position;
        _rigidbody.rotation = _inGameData.playerStartPos.rotation;
    }

    /// <summary>
    /// 移動速度の倍率を変更する。
    /// </summary>
    /// <param name="multiplier">移動速度倍率</param>
    public void SetSpeedMultiplier(float multiplier)
    {
        // 負の速度倍率による意図しない逆方向移動を防ぐ。
        _speedMultiplier = Mathf.Max(0f, multiplier);
    }

    /// <summary>
    /// 移動入力の反転状態を変更する。
    /// </summary>
    /// <param name="isReversed">操作を反転させるか</param>
    public void SetInputReversed(bool isReversed)
    {
        _isInputReversed = isReversed;
    }

    /// <summary>
    /// プレイヤーの移動許可状態を変更する。
    /// </summary>
    /// <param name="canMove">移動を許可するか</param>
    public void SetCanMove(bool canMove)
    {
        _canMove = canMove;

        if (!_canMove)
        {
            StopMovement();
        }
    }

    /// <summary>
    /// 移動に関するデバフを解除する。
    /// </summary>
    public void ResetDebuff()
    {
        _speedMultiplier = DefaultSpeedMultiplier;
        _isInputReversed = false;
    }
}
