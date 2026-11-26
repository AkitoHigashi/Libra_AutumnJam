using UnityEngine;
using UnityEngine.UI;

public class NameInputSystem : MonoBehaviour
{

    [SerializeField]InputField _nameInputField;
    [SerializeField] Text _displayNameText;
    [SerializeField] Text _errorText;

    
    void Start()
    {
        
    }
    public void OnConfirmName()
    {
        string playerName = _nameInputField.text.Trim();
        if(string.IsNullOrEmpty(playerName))
        {
            _errorText.text = "名前を入力してください";
            return;
        }
        
        _displayNameText.text = playerName;
    }
    
}
