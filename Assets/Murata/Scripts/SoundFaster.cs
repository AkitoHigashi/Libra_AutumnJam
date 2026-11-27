using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundFaster : MonoBehaviour
{
    [Header("参照設定")]
    [SerializeField] private InGameData _inGameData;

    [Header("BGM加速設定")]
    [SerializeField, Min(0.1f)]
    private float _accelerationTime = 5f;

    [SerializeField, Range(1f, 3f)]
    private float _maxPitch = 2f;

    private const float DefaultPitch = 1f;

    private AudioSource _audioSource;

    /// <summary>
    /// BGMの再生に使用するAudioSourceを取得する。
    /// </summary>
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// ゲームの進行状態に応じてBGMの再生速度を更新する。
    /// </summary>
    private void Update()
    {
        if (_inGameData == null)
        {
            return;
        }

        if (InGameData.gameState == GameState.Search)
        {
            UpdatePitch();
        }
        else
        {
            ResetPitch();
        }
    }

    /// <summary>
    /// 探索の残り時間に応じてBGMを二次関数的に加速する。
    /// </summary>
    private void UpdatePitch()
    {
        if (_accelerationTime <= 0f)
        {
            return;
        }

        float remainingTime = Mathf.Max(
            0f,
            _inGameData.SearchTime - _inGameData.nowSearchTime);

        float progress = Mathf.Clamp01(
            1f - remainingTime / _accelerationTime);

        float acceleration = progress * progress;

        _audioSource.pitch = Mathf.Lerp(
            DefaultPitch,
            _maxPitch,
            acceleration);
    }

    /// <summary>
    /// BGMの再生速度を通常速度に戻す。
    /// </summary>
    private void ResetPitch()
    {
        _audioSource.pitch = DefaultPitch;
    }

    /// <summary>
    /// コンポーネントの無効化時に再生速度を初期化する。
    /// </summary>
    private void OnDisable()
    {
        if (_audioSource != null)
        {
            ResetPitch();
        }
    }
}