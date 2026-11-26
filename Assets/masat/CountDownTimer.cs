using UnityEngine;
using UnityEngine.UI;
public class CountDownTimer : MonoBehaviour
{
    /// <summary>10秒カウントダウンの初期値</summary>
    [SerializeField] float _timeLimit = 10f;
    /// <summary>時間を表示するテキスト</summary>
    [SerializeField] Text _timerText;
    /// <summary>残り時間</summary>
    float remainingTime;
    /// <summary>時間切れかどうかを判断する</summary>
    public bool isFinished = false;
    
    
    public void CountStart()
    {
        remainingTime = _timeLimit;
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
