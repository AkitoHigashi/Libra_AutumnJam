using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    [Header("移動設定")]
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _rotationSpeed;

    [Header("カメラ設定")]
    [SerializeField, Min(0f)]
    private float _cameraSensitivity;

    public float WalkSpeed => _walkSpeed;
    public float RunSpeed => _runSpeed;
    public float RotationSpeed => _rotationSpeed;
    public float CameraSensitivity => _cameraSensitivity;
}