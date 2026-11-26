using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CinemachineOrbitalFollow))]
public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private PlayerSettings _playerSettings;

    private const float BaseSensitivity = 0.1f;

    private CinemachineOrbitalFollow _orbitalFollow;

    /// <summary>
    /// カメラ操作に必要なコンポーネントを取得する。
    /// </summary>
    private void Awake()
    {
        _orbitalFollow = GetComponent<CinemachineOrbitalFollow>();
    }

    /// <summary>
    /// マウスによるカメラ回転とズームを処理する。
    /// </summary>
    private void Update()
    {
        if (SceneManagement.IsTransitioning || Mouse.current == null || _playerSettings == null)
        {
            return;
        }

        RotateCamera();
        ZoomCamera();
    }

    /// <summary>
    /// マウスの移動量に応じてカメラを回転させる。
    /// </summary>
    private void RotateCamera()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        float sensitivity =
            _playerSettings.CameraSensitivity * BaseSensitivity;

        _orbitalFollow.HorizontalAxis.Value =
            _orbitalFollow.HorizontalAxis.ClampValue(
                _orbitalFollow.HorizontalAxis.Value +
                mouseDelta.x * sensitivity);

        _orbitalFollow.VerticalAxis.Value =
            _orbitalFollow.VerticalAxis.ClampValue(
                _orbitalFollow.VerticalAxis.Value -
                mouseDelta.y * sensitivity);
    }

    /// <summary>
    /// マウスホイールの入力に応じてカメラの距離を変更する。
    /// </summary>
    private void ZoomCamera()
    {
        float scroll = Mouse.current.scroll.ReadValue().y;

        float cameraDistance =
            _orbitalFollow.Radius -
            scroll * _playerSettings.ZoomSpeed;

        _orbitalFollow.Radius = Mathf.Clamp(
            cameraDistance,
            _playerSettings.MinCameraDistance,
            _playerSettings.MaxCameraDistance);
    }
}