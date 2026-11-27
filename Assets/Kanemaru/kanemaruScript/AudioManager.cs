using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer _audioMixer;

    private const string MASTER_KEY = "MasterVolume";
    private const string BGM_KEY = "BGMVolume";
    private const string SE_KEY = "SEVolume";

    private void Awake()
    {
        // 重複生成を防止
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // 保存した音量を読み込む
        LoadVolume();
    }

    // 全体音量
    public void SetMasterVolume(float volume)
    {
        SetVolume("MasterVolume", MASTER_KEY, volume);
    }

    // BGM音量
    public void SetBGMVolume(float volume)
    {
        SetVolume("BGMVolume", BGM_KEY, volume);
    }

    // SE音量
    public void SetSEVolume(float volume)
    {
        SetVolume("SEVolume", SE_KEY, volume);
    }

    // 音量変更の共通処理
    private void SetVolume(string mixerName, string key, float volume)
    {
        volume = Mathf.Clamp01(volume);

        // 0～1をデシベルに変換
        float db = volume > 0f
            ? Mathf.Log10(volume) * 20f
            : -80f;

        _audioMixer.SetFloat(mixerName, db);

        // 音量を保存
        PlayerPrefs.SetFloat(key, volume);
        PlayerPrefs.Save();
    }

    // 音量設定を読み込む
    private void LoadVolume()
    {
        float master = PlayerPrefs.GetFloat(MASTER_KEY, 1f);
        float bgm = PlayerPrefs.GetFloat(BGM_KEY, 0.5f);
        float se = PlayerPrefs.GetFloat(SE_KEY, 0.5f);

        SetMasterVolume(master);
        SetBGMVolume(bgm);
        SetSEVolume(se);
    }

    // 保存された音量を取得
    public float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_KEY, 1f);
    }

    public float GetBGMVolume()
    {
        return PlayerPrefs.GetFloat(BGM_KEY, 0.5f);
    }

    public float GetSEVolume()
    {
        return PlayerPrefs.GetFloat(SE_KEY, 0.5f);
    }
}