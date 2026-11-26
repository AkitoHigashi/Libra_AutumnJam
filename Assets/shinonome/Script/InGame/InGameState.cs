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
    private void Update()
    {
        if (nowAction) return;
        switch (InGameData.gameState)
        {
            case GameState.CountDown:
                {
                  nowAction = true;
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

                });
                break;

            case GameState.Search:
                SearchTime();
                break;

            case GameState.Instructions:
                Instruction();
                break;
        }
    }
    void StartCountDown(Action onComplete)
    {

    }

    void WarkTime(Action onComplete)
    {

    }

    void SearchTime(Action onComplete)
    {

    }

    void Instruction(Action onComplete)
    {

    }
}
