using UnityEngine;
using UnityEngine.InputSystem;

public class ScoreTest : MonoBehaviour
{
    [Header("Score Settings")]
    [SerializeField] private ScoreCalculator _scoreCalculator;

    [Header("Item Settings")]
    [SerializeField] private ItemData _itemData;

    [Header("Timer Settings")]
    [SerializeField] private GameTimerTest _gameTimer;

    private void Update()
    {
        // キーボードが接続されていない場合は処理しない
        if (Keyboard.current == null)
        {
            return;
        }

        // Fキー：正解判定のテスト
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            _scoreCalculator.CalculateScore(
                true,
                _itemData,
                _gameTimer.GetRemainingTime()
            );

            Debug.Log("正解判定のテスト");
        }

        // Jキー：不正解判定のテスト
        if (Keyboard.current.jKey.wasPressedThisFrame)
        {
            _scoreCalculator.CalculateScore(
                false,
                _itemData,
                _gameTimer.GetRemainingTime()
            );

            Debug.Log("不正解判定のテスト");
        }
    }
}