using System;
using UnityEngine;
using System.Collections;

public class InGameState : MonoBehaviour
{
    GameObject _mainGameManager;
    GameObject _itemManager;
    InGameData _gameData;
    ScoreCalculator _scoreCalculator;
    TargetItemSelector _targetItemSelector;
    ItemJudge _itemJudge;
    ItemSpawner _spawner;
    bool nowAction = false;
    private void OnEnable()
    {
        _mainGameManager = GameObject.FindWithTag("MainGameManager");
        _itemManager = GameObject.FindWithTag("ItemManager");
        _gameData = _mainGameManager.GetComponent<InGameData>();
        _targetItemSelector = _itemManager.GetComponent<TargetItemSelector>();
        _itemJudge = _itemManager.GetComponent<ItemJudge>();
        _spawner = _itemManager.GetComponent<ItemSpawner>();
        _scoreCalculator = _itemManager.GetComponent<ScoreCalculator>();
    }
    private void Update()//各ゲームステートでそれぞれの処理を行う
    {
        if (nowAction) return;
        nowAction = true;
        switch (InGameData.gameState)
        {
            case GameState.CountDown:
                {
                    StartCoroutine(StartCountDown(() =>
                    {
                        InGameData.gameState = GameState.Walk;
                        nowAction = false;
                    }));
                }
                break;

            case GameState.Walk:
                StartCoroutine(WarkTime(() =>
                {
                    InGameData.gameState = GameState.lottery;
                    nowAction = false;
                }));
                break;

            case GameState.lottery:
                StartCoroutine(lottery(() =>
                {
                    InGameData.gameState = GameState.Search;
                    nowAction = false;
                }));
                break;

            case GameState.Search:
                StartCoroutine(SearchTime(() =>
                {
                    InGameData.gameState = GameState.Instructions;
                    nowAction = false;
                }));
                break;

            case GameState.Instructions:
                StartCoroutine(Judge(() =>
                {
                    InGameData.gameState = GameState.CountDown;
                    nowAction = false;
                }));
                break;
        }
    }
    IEnumerator StartCountDown(Action onComplete)
    {
        _gameData.isTake = false;
        // ゲーム開始時にアイテムを生成する
        _spawner.SpawnItems();
        //ゲーム開始時にターゲットを決定
        _targetItemSelector.SelectTargetItem();
        //カウントダウンの処理
        yield return new WaitForSeconds(1f);

        onComplete?.Invoke();
    }

    IEnumerator WarkTime(Action onComplete)
    {
        //一定時間たつまで歩けるようにする
        _gameData.nowSearchTime = 0;
        while(_gameData.WalkTime > _gameData.nowSearchTime)
        {
            _gameData.nowSearchTime += Time.deltaTime;
            yield return null;
        }
        onComplete?.Invoke();
    }

    IEnumerator lottery(Action onComplete)
    {
        //選ばれたアイテムを表示する処理

        yield return new WaitForSeconds(1f);
        onComplete?.Invoke();
    }

    IEnumerator SearchTime(Action onComplete)
    { 
        //アイテムを選択するか時間がたつまで待つ
        _gameData.nowSearchTime = 0;
        while(_gameData.SearchTime > _gameData.nowSearchTime && !_gameData.isTake)
        {
            _gameData.nowSearchTime += Time.deltaTime;
            yield return null;
        }
        onComplete?.Invoke();
    }

    IEnumerator Judge(Action onComplete)
    {
        //判定後アイテムを全削除する
        if (!_gameData.isTake)
        {
            _scoreCalculator.CalculateScore(false, _gameData.picUpItem, _gameData.nowSearchTime);
            //アイテムの全削除

            yield return new WaitForSeconds(1f);
        }
        if (_gameData.isTake)
        {
            _itemJudge.JudgeItem();
        }
        onComplete?.Invoke();
    }
}
