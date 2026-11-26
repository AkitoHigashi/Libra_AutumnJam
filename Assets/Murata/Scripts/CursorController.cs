using UnityEngine;

public class CursorController : MonoBehaviour
{
    /// <summary>
    /// ゲーム開始時にカーソルを固定する。
    /// </summary>
    private void Start()
    {
        LockCursor();
    }

    /// <summary>
    /// カーソルを画面中央に固定して非表示にする。
    /// </summary>
    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    /// <summary>
    /// カーソルの固定を解除して表示する。
    /// </summary>
    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}