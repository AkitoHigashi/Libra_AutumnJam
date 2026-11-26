using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>暗転、ロード画面、暗転解除を共通化するシーン遷移処理。</summary>
public class SceneManagement : MonoBehaviour
{
    [SerializeField] string _gameSceneName;

    [Header("ロード演出")]
    [SerializeField, Min(0f), Tooltip("暗転するまでの秒数")]
    private float _fadeOutDuration = 0.3f;
    [SerializeField, Min(0f), Tooltip("ロード画面の最低表示秒数")]
    private float _minimumLoadingDuration = 2f;
    [SerializeField, Min(0f), Tooltip("暗転解除にかける秒数")]
    private float _fadeInDuration = 0.3f;
    [SerializeField, Tooltip("任意のロード画面画像。未設定の場合は黒背景と文字を表示")]
    private Sprite _loadingImage;

    public static bool IsTransitioning { get; private set; }

    private bool _isRunner;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetTransitionFlag()
    {
        IsTransitioning = false;
    }

    // 既存のボタンや名前入力からの呼び出しを維持する。
    public void GameSceneLoad()
    {
        LoadSceneWithTransition(_gameSceneName, true);
    }

    /// <summary>他のシーンへの遷移にも利用可能。インゲーム以外は startGame=false。</summary>
    public void LoadSceneWithTransition(string sceneName, bool startGame = false)
    {
        if (IsTransitioning) return;
        if (string.IsNullOrWhiteSpace(sceneName) ||
            !Application.CanStreamedLevelBeLoaded(sceneName))
        {
            Debug.LogError($"ロード先のシーンがBuild Settingsにありません: {sceneName}", this);
            return;
        }

        IsTransitioning = true;

        // タイトルのCanvasやマネージャー本体は永続化せず、遷移専用のルートだけ残す。
        GameObject root = new GameObject("SceneTransition",
            typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler),
            typeof(GraphicRaycaster), typeof(CanvasGroup));
        DontDestroyOnLoad(root);
        SceneManagement runner = root.AddComponent<SceneManagement>();
        runner._isRunner = true;
        runner._fadeOutDuration = _fadeOutDuration;
        runner._minimumLoadingDuration = _minimumLoadingDuration;
        runner._fadeInDuration = _fadeInDuration;
        runner._loadingImage = _loadingImage;
        runner.StartCoroutine(runner.Transition(sceneName, startGame));
    }

    private IEnumerator Transition(string sceneName, bool startGame)
    {
        Canvas canvas = GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = short.MaxValue;
        CanvasScaler scaler = GetComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);

        CanvasGroup group = GetComponent<CanvasGroup>();
        group.alpha = 0f;
        group.blocksRaycasts = true;

        Image background = CreateImage("FadeBackground", transform);
        background.color = Color.black;
        background.raycastTarget = true;

        GameObject loading = new GameObject("LoadingContent", typeof(RectTransform));
        loading.transform.SetParent(transform, false);
        Stretch((RectTransform)loading.transform);

        if (_loadingImage != null)
        {
            Image image = CreateImage("LoadingImage", loading.transform);
            image.sprite = _loadingImage;
            image.preserveAspect = true;
            image.raycastTarget = false;
        }

        GameObject label = new GameObject("LoadingText", typeof(RectTransform), typeof(Text));
        label.transform.SetParent(loading.transform, false);
        RectTransform rect = (RectTransform)label.transform;
        rect.anchorMin = new Vector2(0f, 0f);
        rect.anchorMax = new Vector2(1f, 0.2f);
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
        Text text = label.GetComponent<Text>();
        text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        text.text = "NOW LOADING...";
        text.fontSize = 48;
        text.fontStyle = FontStyle.Bold;
        text.alignment = TextAnchor.MiddleCenter;
        text.color = Color.white;
        text.raycastTarget = false;
        loading.SetActive(false);

        // 操作を遮断し、黒くなってからロードを開始する。
        yield return Fade(group, 1f, _fadeOutDuration);
        loading.SetActive(true);
        float loadingStarted = Time.realtimeSinceStartup;
        yield return null; // ロード開始前に表示を1フレーム描画できるようにする。

        GameState previousState = InGameData.gameState;
        if (startGame) InGameData.gameState = GameState.None;

        AsyncOperation operation = null;
        try
        {
            operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        }
        catch (System.Exception exception)
        {
            Debug.LogException(exception, this);
        }

        if (operation != null)
        {
            while (!operation.isDone ||
                   Time.realtimeSinceStartup - loadingStarted < _minimumLoadingDuration)
                yield return null;
        }
        else if (startGame)
        {
            InGameData.gameState = previousState;
        }

        // 読み込みと最低表示時間の両方が完了した後に暗転解除する。
        loading.SetActive(false);
        yield return Fade(group, 0f, _fadeInDuration);

        // 暗転解除が完了したフレームで初めてゲーム進行を許可する。
        if (operation != null && startGame)
            InGameData.gameState = GameState.CountDown;

        IsTransitioning = false;
        _isRunner = false;
        Destroy(gameObject);
    }

    private static IEnumerator Fade(CanvasGroup group, float target, float duration)
    {
        float from = group.alpha;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            group.alpha = Mathf.Lerp(from, target, Mathf.Clamp01(elapsed / duration));
            yield return null;
        }
        group.alpha = target;
    }

    private static Image CreateImage(string objectName, Transform parent)
    {
        GameObject obj = new GameObject(objectName, typeof(RectTransform), typeof(Image));
        obj.transform.SetParent(parent, false);
        Stretch((RectTransform)obj.transform);
        return obj.GetComponent<Image>();
    }

    private static void Stretch(RectTransform rect)
    {
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
    }

    private void OnDestroy()
    {
        if (_isRunner) IsTransitioning = false;
    }
}
