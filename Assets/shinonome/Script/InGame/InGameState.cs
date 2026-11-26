using UnityEngine;

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
        switch (InGameData.gameState)
        {
            case GameState.CountDown:
                StartCountDown();
                break;

            case GameState.Walk:
                WarkTime();
                break;

            case GameState.Search:
                SearchTime();
                break;

            case GameState.Instructions:
                Instruction();
                break;
        }
    }
    void StartCountDown()
    {

    }

    void WarkTime()
    {

    }

    void SearchTime()
    {

    }

    void Instruction()
    {

    }
}
