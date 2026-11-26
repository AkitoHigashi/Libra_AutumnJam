using UnityEngine;
using UnityEngine.UI;
using System;
public class CountDownTimer : MonoBehaviour
{
    [SerializeField] InGameData _inGameData;
    
    /// <summary>時間を表示するテキスト</summary>
    [SerializeField] Text _timerText;
    /// <summary>残り時間</summary>
    float _remainingTime;
    /// <summary>時間切れかどうかを判断する</summary>
    public bool _isFinished = false;
    /// <summary>練り歩きカウントダウンの初期値（20秒）</summary>
    float _walkTimeLimit;
    /// <summary>探す時間のカウントダウンの初期値（10秒）</summary>
    float _seachTimeLimit;
    /// <summary>今が「探す時間」かどうか判断する</summary>
    bool _isSearchPhase = false;

    bool _isActive = false;
   
    void Start()
    {
        _walkTimeLimit = _inGameData.WalkTime;
        _seachTimeLimit = _inGameData.SearchTime;
        _isFinished = false;
        _isSearchPhase = false;
        _remainingTime = _walkTimeLimit;

    }
    

    void Update()
    {
        if(!_isActive) TimerCount(() => { });

    }
    public void TimerCount(Action onComplete)
    {
        _isActive = true;
        if (_isFinished)
        {

            return;
        }

        _remainingTime -= Time.deltaTime;
        if (_remainingTime <= 0f)
        {
            /*タイムアップした時間が「練り歩き時間」だったら、
            「探す時間」に移行する*/
            if (!_isSearchPhase)
            {
                _isSearchPhase = true;
                _remainingTime = _seachTimeLimit;
            }
            else
            {
                //「探す時間」がタイムアップしたら停止する

                _remainingTime = 0f;
                _isFinished = true;
            }
        }
        _timerText.text = _remainingTime.ToString("00.00");
        onComplete?.Invoke();
    }
}
