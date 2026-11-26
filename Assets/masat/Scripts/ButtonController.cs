using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class ButtonController : MonoBehaviour
{
    [SerializeField] GameObject _titleCanvas;
    [SerializeField] GameObject _nameInputCanvas;
    //[SerializeField] GameObject _playingCanvas;
    [SerializeField] GameObject _resultCanvas;

    
    void Start()
    {
        _titleCanvas.SetActive(true);
        _nameInputCanvas.SetActive(false);
       //_playingCanvas.SetActive(false);
        _resultCanvas.SetActive(false);
    }
    public void GameStartButton()
    {
        _titleCanvas.SetActive(false);
        _nameInputCanvas.SetActive(true);
       //_playingCanvas.SetActive(false);
        _resultCanvas.SetActive(false);
    }
    public void PlayingButton()
    {
        _titleCanvas.SetActive(false);
        _nameInputCanvas.SetActive(false);
       //_playingCanvas.SetActive(true);
        _resultCanvas.SetActive(false);
    }
    public void TitleButton()
    {
        _titleCanvas.SetActive(true);
        _nameInputCanvas.SetActive(false);
        //_playingCanvas.SetActive(false);
        _resultCanvas.SetActive(false);
    }
    
}
