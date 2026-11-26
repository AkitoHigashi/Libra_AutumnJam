
using UnityEngine;
using UnityEngine.Events;

public class ItemJudge : MonoBehaviour
{
    [SerializeField] private InGameData _inGameData;

    public UnityEvent onCorrect;
    public UnityEvent onIncorrect;

    public void JudgeItem()
    {
        if (_inGameData == null ||
            _inGameData.picUpItem == null ||
            _inGameData.playerTakeItem == null)
        {
            return;
        }

        if (_inGameData.playerTakeItem == _inGameData.picUpItem)
        {
            onCorrect?.Invoke();
        }
        else
        {
            onIncorrect?.Invoke();
        }
    }
}

