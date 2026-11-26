using UnityEngine;
using UnityEngine.UI;
public class GameStart : MonoBehaviour
{
    [SerializeField] GameObject _titleCanvas;
    [SerializeField] GameObject _nameSelectCanvas;
    void Start()
    {
        _titleCanvas.SetActive(true);
        _nameSelectCanvas.SetActive(false);
    }
    public void GameStartButton()
    {
        _titleCanvas.SetActive(false);
        _nameSelectCanvas.SetActive(true);
    }
}
