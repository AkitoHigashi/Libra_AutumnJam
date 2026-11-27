using TMPro;
using UnityEngine;

public class ScoreCalculator : MonoBehaviour
{
    GameObject _mainGameManager;
    InGameData _gameData;
    [Header("Score")]
    [SerializeField] private int _score = 0;
    [SerializeField] private int _totalPoint = 0;
    [SerializeField] private int _baseScore = 10;
    [SerializeField] private int _penaltyScore = 20;
    [SerializeField] private TMP_Text _scoreText;

    [Header("Time Bonus")]
    [SerializeField] private int _timeBonus = 10;

    private void Start()
    {
        _mainGameManager = GameObject.FindWithTag("InGameManager");
        _gameData = _mainGameManager.GetComponent<InGameData>();
        UpdateScoreUI();
    }
    // ターン開始時にスコアをリセット
    //ターンの管理をするスクリプトで
    public void ResetScore()
    {
        _totalPoint = 0;

        UpdateScoreUI();

        Debug.Log("スコアをリセットしました");
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
            Debug.LogError("必要なデータが設定されていません");
            return;
        }

        // お題のアイテムとプレイヤーが掴んだアイテムを比較
        bool correct = itemData == _gameData.playerTakeItem;

        if (correct)
        {
            // 正解時の基本得点
            _totalPoint = _baseScore + itemData.item.point;

            // 時間ボーナス
            AddTimeBonus(remainingTime, _totalPoint);

            Debug.Log("正解！");
        }
        else
        {
            // 不正解時の減点とライフ減少
            Penalty();

            Debug.Log("不正解！");
        }

        UpdateScoreUI();
    }

    // 残り時間によるボーナス
    private void AddTimeBonus(float remainingTime , int totalPoint)
    {
        int bonus = Mathf.FloorToInt(remainingTime) * _timeBonus;

        _gameData.sumPoint +=(totalPoint + bonus);
        _totalPoint += bonus;

        Debug.Log("時間ボーナス：+" + bonus + "点");
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