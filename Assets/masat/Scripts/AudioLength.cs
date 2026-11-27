using UnityEngine;

public class AudioLength : MonoBehaviour
{
    //unityエディタ上で指定されたAudioClipの秒数を取得する
    [SerializeField] AudioClip _audioClip;

    float _audioLength;
    void Start()
    {
        _audioLength = _audioClip.length;
    }
}
