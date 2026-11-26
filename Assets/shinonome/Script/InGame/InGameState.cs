using UnityEngine;
using System;

public class InGameState : MonoBehaviour
{
    GameObject _mainGameManager;
    InGameData _gameData;
    bool nowAction = false;
    private void OnEnable()
    {
        _mainGameManager = GameObject.FindWithTag("MainGameManager");
        _gameData = _mainGameManager.GetComponent<InGameData>();
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

    }

    void Instruction(Action onComplete)
    {

    }
}
