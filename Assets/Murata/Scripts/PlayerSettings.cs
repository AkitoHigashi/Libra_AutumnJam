using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    [Header("移動設定")]
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _rotationSpeed;

    [Header("カメラ設定")]
    [SerializeField, Min(0f)]
    private float _cameraSensitivity = 1f;

    [Header("カメラズーム設定")]
    [SerializeField] private float _zoomSpeed = 0.01f;
    [SerializeField] private float _minCameraDistance = 2f;
    [SerializeField] private float _maxCameraDistance = 8f;

    public float ZoomSpeed => _zoomSpeed;
    public float MinCameraDistance => _minCameraDistance;
    public float MaxCameraDistance => _maxCameraDistance;

    [Header("アイテム取得設定")]
    [SerializeField, Min(0f)]
    private float _pickupRadius = 2f;

    public float WalkSpeed => _walkSpeed;
    public float RunSpeed => _runSpeed;
    public float RotationSpeed => _rotationSpeed;
    public float CameraSensitivity => _cameraSensitivity;
    public float PickupRadius => _pickupRadius;
}