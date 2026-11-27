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

    //真偽判定後の処理
    [SerializeField] private ScoreCalculator _scoreCalculator;

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

        // プレイヤーが掴んだアイテムを取得する
        ItemData selectedItem = _inGameData.playerTakeItem;

        // 掴んだアイテムとお題のアイテムが同じか比較する
        bool isCorrect = selectedItem == _inGameData.picUpItem;

        // 判定中に同じ処理が重複して実行されないようにする
        _isJudging = true;

        // 判定結果に応じてイベントを呼び出す
        _scoreCalculator.CalculateScore(_isJudging, _inGameData.picUpItem, _inGameData.nowSearchTime);

<<<<<<< HEAD
        // 掴んだアイテムの子にあるCommonItemModelを探す
        Transform commonModel =
            selectedItem.transform.Find("CommonItemModel");

        // CommonItemModelが見つかった場合は削除する
        if (commonModel != null)
        {
            Destroy(commonModel.gameObject);
        }

        // 一定時間後にアイテムを削除して次のお題を選ぶ処理を開始する
        StartCoroutine(RemoveItemAndSelectNext(selectedItem));
    }

    // アイテムの削除と次のお題の抽選を行うコルーチン
    private IEnumerator RemoveItemAndSelectNext(ItemData selectedItem)
    {
        // 削除するアイテムをお題の抽選対象から外す
        _itemSpawner.RemoveSpawnedItem(selectedItem);

        // Inspectorで設定した秒数だけ待つ
        yield return new WaitForSeconds(_itemDestroyDelay);

        // 対象のアイテムが存在している場合は削除する
        if (selectedItem != null)
        {
            Destroy(selectedItem.gameObject);
        }

        // プレイヤーが掴んだアイテムの情報をリセットする
        _inGameData.playerTakeItem = null;

        // ステージに残っているアイテムから次のお題を抽選する
        _targetItemSelector.SelectTargetItem();

        // 判定処理中の状態を解除する
        _isJudging = false;
=======
>>>>>>> 6f628dc196cb6d25a7a75c75efdd7f041745aa83
    }
}
