using System;
using UnityEngine;
using System.Collections;

/// <summary>
/// インゲーム全体の進行フェーズ（ステート）を管理し、各フェーズに応じた処理を順次実行するクラス
/// </summary>
public class InGameState : MonoBehaviour
{
    GameObject _mainGameManager;
    GameObject _itemManager;
    InGameData _gameData;
    ScoreCalculator _scoreCalculator;
    TargetItemSelector _targetItemSelector;
    ItemJudge _itemJudge;
    ItemSpawner _spawner;

    // 現在のステート処理が実行中かどうか（多重起動防止用フラグ）
    bool nowAction = false;

    /// <summary>
    /// オブジェクト有効化時にマネージャー類および各機能コンポーネントの参照を取得する
    /// </summary>
    private void OnEnable()
    {
        _mainGameManager = GameObject.FindWithTag("InGameManager");
        _itemManager = GameObject.FindWithTag("ItemManager");

        _gameData = _mainGameManager.GetComponent<InGameData>();
        _targetItemSelector = _itemManager.GetComponent<TargetItemSelector>();
        _itemJudge = _itemManager.GetComponent<ItemJudge>();
        _spawner = _itemManager.GetComponent<ItemSpawner>();
        _scoreCalculator = _itemManager.GetComponent<ScoreCalculator>();
    }

    /// <summary>
    /// 現在のゲームステートを監視し、未実行のステートであれば対応するコルーチンを開始する
    /// </summary>
    private void Update()
    {
        // すでに現在のステートのコルーチンが進行中なら何もしない
        if (nowAction) return;
        nowAction = true;

        switch (InGameData.gameState)
        {
            // ① ラウンド開始前のカウントダウン（アイテム配置・お題決定）
            case GameState.CountDown:
                StartCoroutine(StartCountDown(() =>
                {
                    InGameData.gameState = GameState.Walk;
                    nowAction = false;
                    Debug.Log("endCountDown");
                }));
                break;

            // ② フィールド内を自由に練り歩いてアイテムの位置を覚えるフェーズ
            case GameState.Walk:
                StartCoroutine(WarkTime(() =>
                {
                    InGameData.gameState = GameState.lottery;
                    nowAction = false;
                    Debug.Log("EndWarkTime");
                }));
                break;

            // ③ 探すべきお題アイテムの発表（抽選結果表示）フェーズ
            case GameState.lottery:
                StartCoroutine(lottery(() =>
                {
                    InGameData.gameState = GameState.Search;
                    nowAction = false;
                    Debug.Log("Endlottery");
                }));
                break;

            // ④ 制限時間内にお題アイテムを探して取得するフェーズ
            case GameState.Search:
                StartCoroutine(SearchTime(() =>
                {
                    InGameData.gameState = GameState.Instructions;
                    nowAction = false;
                    Debug.Log("EndSearch");
                }));
                break;

            // ⑤ 正解発表・スコア計算・ライフ判定フェーズ
            case GameState.Instructions:
                StartCoroutine(Judge(() =>
                {
                    // ライフが0以下ならリザルトへ移行、残っていれば次のラウンド（カウントダウン）へ
                    if (_gameData.life <= 0) InGameData.gameState = GameState.StartResult;
                    else InGameData.gameState = GameState.CountDown;
                    nowAction = false;
                    Debug.Log("EndJuge");
                }));
                break;

            // ⑥ ゲーム終了後のリザルト移行フェーズ
            case GameState.StartResult:
                StartCoroutine(StartResult(() =>
                {
                    // リザルトシーンへの遷移処理などを記述
                    Debug.Log("StartResult");
                }));
                break;
        }
    }

    /// <summary>
    /// ラウンド開始時の初期セットアップとカウントダウンを行う
    /// </summary>
    /// <param name="onComplete">処理完了時にステートを次へ進めるコールバック</param>
    /// <returns>コルーチン用の列挙子</returns>
    IEnumerator StartCountDown(Action onComplete)
    {
        // アイテム未所持状態にリセット
        _gameData.isTake = false;

        // 各エリアの出現位置にアイテムをランダムに配置する
        _spawner.SpawnItems();

        // 今回プレイヤーが探すべきお題アイテムを決定する
        _targetItemSelector.SelectTargetItem();

        // カウントダウン演出の待機
        yield return new WaitForSeconds(1f);

        onComplete?.Invoke();
    }

    /// <summary>
    /// プレイヤーがエリア内を練り歩いてアイテム配置を記憶できる時間（WalkTime）を計測する
    /// </summary>
    /// <param name="onComplete">制限時間経過時にステートを次へ進めるコールバック</param>
    /// <returns>コルーチン用の列挙子</returns>
    IEnumerator WarkTime(Action onComplete)
    {
        _gameData.nowSearchTime = 0;

        // 設定された練り歩き時間（WalkTime）に達するまで毎フレーム時間を加算
        while (_gameData.WalkTime > _gameData.nowSearchTime)
        {
            _gameData.nowSearchTime += Time.deltaTime;
            yield return null;
        }

        onComplete?.Invoke();
    }

    /// <summary>
    /// 抽選されたお題アイテムをプレイヤーへ表示・提示する演出を行う
    /// </summary>
    /// <param name="onComplete">表示完了時にステートを次へ進めるコールバック</param>
    /// <returns>コルーチン用の列挙子</returns>
    IEnumerator lottery(Action onComplete)
    {
        // 選ばれたお題アイテム（_gameData.picUpItem）をUI等に表示する処理

        yield return new WaitForSeconds(1f);
        onComplete?.Invoke();
    }

    /// <summary>
    /// お題アイテムを探す探索フェーズの制限時間を計測する。
    /// 制限時間が経過するか、プレイヤーがアイテムを取得した時点で終了する。
    /// </summary>
    /// <param name="onComplete">探索終了時にステートを次へ進めるコールバック</param>
    /// <returns>コルーチン用の列挙子</returns>
    IEnumerator SearchTime(Action onComplete)
    {
        _gameData.nowSearchTime = 0;

        // 「制限時間内」かつ「まだアイテムを取っていない（!isTake）」間だけ待機する
        while (_gameData.SearchTime > _gameData.nowSearchTime && !_gameData.isTake)
        {
            _gameData.nowSearchTime += Time.deltaTime;
            yield return null;
        }

        onComplete?.Invoke();
    }

    /// <summary>
    /// 正解発表フェーズの処理。アイテム取得の有無に応じて正誤判定・スコア計算を行い、次ラウンドへの待機を行う
    /// </summary>
    /// <param name="onComplete">判定と演出完了時にステートを次へ進めるコールバック</param>
    /// <returns>コルーチン用の列挙子</returns>
    IEnumerator Judge(Action onComplete)
    {
        if (!_gameData.isTake)
        {
            // 時間切れでアイテムをつかめなかった場合：不正解扱いとしてお題アイテムのポイント分を減点する
            _scoreCalculator.CalculateScore(false, _gameData.picUpItem, _gameData.nowSearchTime);
            // フィールド上のアイテムを全削除する処理
        }
        else
        {
            // アイテムを取得していた場合：お題と一致しているか判定し、正解ならスコア加算・不正解ならライフ減少を行う
            _itemJudge.JudgeItem();
            // フィールド上のアイテムを全削除する処理
        }

        // 正解発表の表示時間待機
        yield return new WaitForSeconds(1f);
        onComplete?.Invoke();
    }

    /// <summary>
    /// ライフが0になった際のリザルト画面への移行処理を行う
    /// </summary>
    /// <param name="onComplete">移行処理完了時のコールバック</param>
    /// <returns>コルーチン用の列挙子</returns>
    IEnumerator StartResult(Action onComplete)
    {
        // リザルト画面へ移行する処理

        yield return null;
        onComplete?.Invoke();
    }
}