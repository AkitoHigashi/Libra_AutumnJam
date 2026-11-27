using UnityEngine;
using UnityEngine.UI;

public class NameInputSystem : MonoBehaviour
{

    [SerializeField]InputField _nameInputField;
    [SerializeField] Text _displayNameText;
    [SerializeField] Text _errorText;
    [SerializeField] SceneManagement _sceneManagement;

    public string _playerName;
    
    public void OnConfirmName()
    {
         _playerName = _nameInputField.text.Trim();
        if(string.IsNullOrEmpty(_playerName))
        {
            _errorText.text = "名前を入力してください";
            return;
        }
        
        _displayNameText.text = _playerName;
        _sceneManagement.GameSceneLoad();
    
    }
    
}
