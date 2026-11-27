using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class ItemJudge : MonoBehaviour
{
    // ゲーム中の情報を管理するデータ
    [SerializeField] private InGameData _inGameData;

    // スポーンしたアイテムを管理するスクリプト
    [SerializeField] private ItemSpawner _itemSpawner;

    // お題のアイテムを抽選するスクリプト
    [SerializeField] private TargetItemSelector _targetItemSelector;

    // アイテムを削除するまでの待ち時間（秒）
    [SerializeField] private float _itemDestroyDelay = 5f;

    // 真偽判定後の処理
    [SerializeField] private ScoreCalculator _scoreCalculator;

    // アイテムの見た目を変換するスクリプト
    [SerializeField] private ItemTransformer _itemTransformer;

    // 正解したときに呼び出すイベント
    public UnityEvent onCorrect;

    // 不正解だったときに呼び出すイベント
    public UnityEvent onIncorrect;

    // 判定処理中かどうかを管理する
    private bool _isJudging;

    // アイテムの正誤判定を行うメソッド
    public void JudgeItem()
    {
        // 必要なデータがない場合や、すでに判定中の場合は処理を終了する
        if (_isJudging ||
            _inGameData == null ||
            _inGameData.picUpItem == null ||
            _inGameData.playerTakeItem == null)
        {
            return;
        }

        // プレイヤーが掴んだアイテムを取得
        ItemData selectedItem = _inGameData.playerTakeItem;

        // 掴んだアイテムとお題のアイテムが同じか比較
        bool isCorrect = selectedItem == _inGameData.picUpItem;

        // 判定中に同じ処理が重複して実行されないようにする
        _isJudging = true;

        // 正誤判定時に元のメッシュを表示する
        if (_itemTransformer != null)
        {
            _itemTransformer.RestoreItemMesh(selectedItem);
        }

        // 判定結果に応じてイベントを呼び出す
        if (isCorrect)
        {
            onCorrect?.Invoke();
        }
        else
        {
            onIncorrect?.Invoke();
        }

        // 判定結果をスコア計算に渡す
        if (_scoreCalculator != null)
        {
            _scoreCalculator.CalculateScore(
                isCorrect,
                _inGameData.picUpItem,
                _inGameData.nowSearchTime
            );
        }
    }
}
