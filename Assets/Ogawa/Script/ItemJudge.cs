using UnityEngine;
using UnityEngine.Events;

public class ItemJudge : MonoBehaviour
{
    [SerializeField] private InGameData _inGameData;

    public UnityEvent onCorrect;
    public UnityEvent onIncorrect;

    public void JudgeItem(ItemData selectedItem)
    {
        if (_inGameData == null || _inGameData.picUpItem == null
            || selectedItem == null)
        {
            return;
        }

        if (selectedItem == _inGameData.picUpItem)
        {
            onCorrect?.Invoke();
        }
        else
        {
            onIncorrect?.Invoke();
        }
    }
}

