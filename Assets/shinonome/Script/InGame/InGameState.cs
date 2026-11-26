using System;
using Unity.VisualScripting;
using UnityEngine;

public class InGameState : MonoBehaviour
{
    GameObject _mainGameManager;
    InGameData _gameData;
    ScoreCalculator _scoreCalculator;
    bool nowAction = false;
    float _nowTime;
    private void OnEnable()
    {
        _mainGameManager = GameObject.FindWithTag("MainGameManager");
        _gameData = _mainGameManager.GetComponent<InGameData>();
        _scoreCalculator = _mainGameManager.GetComponent<ScoreCalculator>();
    }
    private void Update()//各ゲームステートでそれぞれの処理を行う
    {
        if (nowAction) return;
        nowAction = true;
        switch (InGameData.gameState)
        {
            case GameState.CountDown:
                {
                    StartCountDown(() =>
                    {
                        InGameData.gameState = GameState.Walk;
                        nowAction = false;
                    });
                }
                break;

            case GameState.Walk:
                WarkTime(() => 
                {
                    InGameData.gameState = GameState.lottery;
                    nowAction = false;
                });
                break;

            case GameState.lottery:
                lottery(() =>
                {
                    InGameData.gameState = GameState.lottery;
                    nowAction = false;
                });
                break;

            case GameState.Search:
                SearchTime(() =>
                {
                    InGameData.gameState = GameState.Instructions;
                    nowAction = false;
                });
                break;

            case GameState.Instructions:
                Instruction(() =>
                {
                    InGameData.gameState = GameState.CountDown;
                    nowAction = false;
                });
                break;
        }
    }
    void StartCountDown(Action onComplete)
    {

    }

    void WarkTime(Action onComplete)
    {

    }

    void lottery(Action onComplete)
    {

    }

    void SearchTime(Action onComplete)
    {
        while(_gameData.SearchTime > _gameData.nowSearchTime)
        {
            _gameData.nowSearchTime += Time.deltaTime;
        }
    }

    void Instruction(Action onComplete)
    {
        bool isClear = true;
        _scoreCalculator.CalculateScore(isClear, _gameData.picUpItem, _gameData.nowSearchTime);
    }
}
