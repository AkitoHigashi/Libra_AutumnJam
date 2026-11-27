using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    // ボタンが押されたときに呼ばれるメソッド
    public void PlayButtonSound()
    {
        if (_audioSource != null)
        {
            _audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSourceがアタッチされていません。");
        }
    }
}
