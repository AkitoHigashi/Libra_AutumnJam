using UnityEngine;
using UnityEngine.UI;
public class CountDownTimer : MonoBehaviour
{
    [SerializeField] InGameData _inGameData;
    
    /// <summary>時間を表示するテキスト</summary>
    [SerializeField] Text _timerText;
    /// <summary>残り時間</summary>
    float remainingTime;
    /// <summary>時間切れかどうかを判断する</summary>
    public bool isFinished = false;
    /// <summary>練り歩きカウントダウンの初期値（20秒）</summary>
    float _walkTimeLimit;
    /// <summary>探す時間のカウントダウンの初期値（10秒）</summary>
    float _seachTimeLimit;
    /// <summary>今が「探す時間」かどうか判定する</summary>
    bool _isSearchPhase = false;

   
    void Start()
    {
        _walkTimeLimit = _inGameData.WalkTime;
        _seachTimeLimit = _inGameData.SearchTime;
        
    }
    public void CountStart()
    {
        _isSearchPhase = false;
        isFinished = false;
        remainingTime = _walkTimeLimit;
    }

    void Update()
    {
        if (isFinished)
        {

            return;
        }

            remainingTime -= Time.deltaTime;
        if(remainingTime <= 0f)
        {
            remainingTime = 0f;
            isFinished = true;

        }
        _timerText.text = remainingTime.ToString("00.00");
    }
}
