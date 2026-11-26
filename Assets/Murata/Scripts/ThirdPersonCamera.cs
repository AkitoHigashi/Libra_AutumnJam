using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

[RequireComponent(typeof(CinemachineOrbitalFollow))]
public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private PlayerSettings _playerSettings;

    private CinemachineOrbitalFollow _orbitalFollow;

    // マウス1ピクセルの移動に対する基準回転角度
    private const float BaseSensitivity = 0.1f;

    /// <summary>
    /// カメラの回転制御コンポーネントを取得する。
    /// </summary>
    private void Awake()
    {
        _orbitalFollow = GetComponent<CinemachineOrbitalFollow>();
    }

    /// <summary>
    /// マウスの移動量に応じてカメラを回転させる。
    /// </summary>
    private void Update()
    {
        if (Mouse.current == null)
        {
            return;
        }

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
}