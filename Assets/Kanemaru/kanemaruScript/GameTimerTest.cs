using UnityEngine;

public class GameTimerTest : MonoBehaviour
{
    [Header("Timer Settings")]
    [SerializeField] private float _timeLimit = 10f;

    private float _currentTime;
    private bool _isPlaying = true;

    private void Start()
    {
        _currentTime = _timeLimit;
    }

    private void Update()
    {
        if (!_isPlaying)
        {
            return;
        }

        _currentTime -= Time.deltaTime;

        if (_currentTime <= 0f)
        {
            _currentTime = 0f;
            _isPlaying = false;

            Debug.Log("時間切れ！");
        }
    }

    // 残り時間を取得する
    public float GetRemainingTime()
    {
        return _currentTime;
    }
}