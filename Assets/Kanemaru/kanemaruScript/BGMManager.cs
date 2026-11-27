using UnityEngine;

public class BGMManager : MonoBehaviour
{
    [Header("BGM設定")]
    [SerializeField] private AudioSource _audioSource;

    // BGMを再生
    public void PlayBGM()
    {
        _audioSource.Play();
    }

    // BGMを停止
    public void StopBGM()
    {
        _audioSource.Stop();
    }

}
