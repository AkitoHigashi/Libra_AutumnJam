using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    //ボタンが押されたときにタイトル画面とゲーム画面を遷移する
    [SerializeField] string _gameSceneName;
    [SerializeField] GameObject _resultPanel;
    InGameData _gameData;

    public void GameSceneLoad()
    {
        SceneManager.LoadScene(_gameSceneName);
    }
    //ゲームオーバー時(ライフが0)になった時にリザルト画面へ遷移する
    public void ResultSceneLoad()
    {
        if(_gameData.life <= 0)
        {
           _resultPanel.SetActive(true);
        }
    }
}
