using TMPro;
using UnityEngine;

public class ScoreCalculator : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] private int _score = 0;
    [SerializeField] private int _baseScore = 10;
    [SerializeField] private int _penaltyScore = 20;
    [SerializeField] private TMP_Text _scoreText;

    [Header("Time Bonus")]
    [SerializeField] private int _timeBonus = 10;

    private void Start()
    {
        UpdateScoreUI();
    }

    // 正誤判定を受け取って得点を計算
    // TODO: 正誤判定の参照先を設定する（ItemPickupなど）
    // TODO: ItemData完成後、引数とデータ構造を確認する
    public void CalculateScore(bool isCorrect, ItemData itemData, float remainingTime)
    {
        if (isCorrect)
        {
            // アイテムの基本得点
            // TODO: アイテム追加得点の参照先を確認する
            int totalScore = _baseScore + itemData.item.point;

            _score += totalScore;

            Debug.Log("正解！ +" + totalScore + "点");

            // 正解アイテム取得時のみ時間ボーナスを追加
            AddTimeBonus(remainingTime);
        }
        else
        {
            _score -= _penaltyScore;

            if (_score < 0)
            {
                _score = 0;
            }

            Debug.Log("不正解！ -" + _penaltyScore + "点");
        }

        UpdateScoreUI();
    }

    // 残り時間によるボーナス
    private void AddTimeBonus(float remainingTime)
    {
        int bonus = Mathf.FloorToInt(remainingTime) * _timeBonus;

        _score += bonus;

        Debug.Log("時間ボーナス：+" + bonus + "点");
    }

    // 得点表示
    private void UpdateScoreUI()
    {
        _scoreText.text = "Score: " + _score;
    }

    // 現在の得点を取得
    public int GetScore()
    {
        return _score;
    }
}