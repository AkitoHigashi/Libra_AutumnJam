using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    //ボタンが押されたときにタイトル画面とゲーム画面を遷移する
    [SerializeField] string _gameSceneName;

    public void GameSceneLoad()
    {
        SceneManager.LoadScene(_gameSceneName);
    }
}
