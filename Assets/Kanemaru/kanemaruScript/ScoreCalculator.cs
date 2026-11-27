using TMPro;
using UnityEngine;

public class ScoreCalculator : MonoBehaviour
{
    GameObject _inGameManager;
    InGameData _gameData;
    [Header("Score")]
    [SerializeField] private int _score = 0;
    [SerializeField] private int _totalPoint = 0;
    [SerializeField] private int _baseScore = 10;
    [SerializeField] private int _timeBonus = 5;
    [SerializeField] private int _penaltyScore = 20;
    [SerializeField] private TMP_Text _scoreText;

    [Header("Timer")]
    [SerializeField] private CountDownTimer _countDownTimer;

    private void Start()
    {
        _inGameManager = GameObject.FindWithTag("InGameManager");
        _gameData = _inGameManager.GetComponent<InGameData>();
        //UpdateScoreUI();
    }
    // ターン開始時にスコアをリセット
    //ターンの管理をするスクリプトで
    public void FinishSet()
    {
        _gameData.sumPoint += _totalPoint;

        Debug.Log("今回のセットの得点：" + _totalPoint);
        Debug.Log("ゲーム全体の合計得点：" + _gameData.sumPoint);

        // 次のセットに向けてリセット
        _totalPoint = 0;

        UpdateScoreUI();
    }

    // 正誤判定を受け取って得点を計算
    // TODO: ItemData完成後、引数とデータ構造を確認する
    public void CalculateScore(
          bool isCorrect,
          ItemData itemData,
          float remainingTime)
    {
        if (_gameData == null || itemData == null)
        {
            Debug.LogError("得点計算に必要なデータがありません");
            return;
        }

        if (isCorrect)
        {
            // 基本得点 + アイテムの得点
            _totalPoint = _baseScore + itemData.item.point;

            // 残り時間ボーナス
            AddTimeBonus(_totalPoint);

            Debug.Log("正解！");
            Debug.Log("今回の獲得得点：" + _totalPoint);
        }
        else
        {
            // 不正解時の減点
            Penalty();

            Debug.Log("不正解！");
        }

        UpdateScoreUI();
    }

    // 残り時間ボーナス
    private void AddTimeBonus(int totalPoint)
    {
        int bonus = 0;

        if (_countDownTimer != null)
        {
            // CountDownTimerから残り時間を取得
            float time = _countDownTimer._remainingTime;

            bonus = Mathf.FloorToInt(time) * _timeBonus;
        }
        else
        {
            Debug.LogWarning("CountDownTimerが設定されていません");
        }

        // 基本得点 + 時間ボーナス
        _totalPoint = totalPoint + bonus;

        // 累計スコアに加算
        _gameData.sumPoint += _totalPoint;

        Debug.Log("時間ボーナス：" + bonus + "点");
    }


    // 得点表示
    private void UpdateScoreUI()
    {
        _scoreText.text = "Score: " + _totalPoint;
    }

    private void Penalty()
    {
        _gameData.sumPoint -= _penaltyScore;

        if (_gameData.sumPoint < 0)
        {
            _totalPoint = 0;
        }
    }
}