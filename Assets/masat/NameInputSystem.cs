using UnityEngine;
using UnityEngine.UI;

public class NameInputSystem : MonoBehaviour
{

    [SerializeField]InputField _nameInputField;
    [SerializeField] Text _displayNameText; 

    
    void Start()
    {
        
    }
    public void OnConfirmName()
    {
        string playerName = _nameInputField.text.Trim();
        if(playerName.Length == 0 )
        {
            _displayNameText.text = "名前を入力してください";
            return;
        }
        
        _displayNameText.text = playerName;
    }
    
}
